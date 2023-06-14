// -----------------------------------------------------------------------
// <copyright file="ChapterExtractor.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Tracks;

/// <summary>
/// The Chapter extractor.
/// </summary>
internal sealed class ChapterExtractor
{
    private static readonly byte[] Tref = "tref"u8.ToArray();

    private static readonly byte[] Chap = "chap"u8.ToArray();

    private static readonly byte[] Mdat = "mdat"u8.ToArray();

    private static readonly byte[] Moov = "moov"u8.ToArray();

    private static readonly byte[] Mvhd = "mvhd"u8.ToArray();

    private static readonly byte[] Trak = "trak"u8.ToArray();

    private static readonly byte[] Tkhd = "tkhd"u8.ToArray();

    private static readonly byte[] Mdia = "mdia"u8.ToArray();

    private static readonly byte[] Mdhd = "mdhd"u8.ToArray();

    private static readonly byte[] Hdlr = "hdlr"u8.ToArray();

    private static readonly byte[] Minf = "minf"u8.ToArray();

    private static readonly byte[] Stbl = "stbl"u8.ToArray();

    private static readonly byte[] Stco = "stco"u8.ToArray();

    private static readonly byte[] Stts = "stts"u8.ToArray();

    private static readonly byte[] Ftyp = "ftyp"u8.ToArray();

    private readonly Stream stream;

    /// <summary>
    /// Initialises a new instance of the <see cref="ChapterExtractor"/> class.
    /// </summary>
    /// <param name="stream">The abstract stream.</param>
    public ChapterExtractor(Stream stream) => this.stream = stream;

    /// <summary>
    /// Gets the chapters.
    /// </summary>
    public ChapterInfo[]? Chapters { get; private set; }

    /// <summary>
    /// Gets the tracks.
    /// </summary>
    public TrakInfo[]? Tracks { get; private set; }

    /// <summary>
    /// Runs the chapter extractor.
    /// </summary>
    public void Run()
    {
        this.Chapters = null;
        this.stream.Seek(0L, SeekOrigin.Begin);
        if (this.ReadMoovInfo() is MoovInfo moovInfo)
        {
            this.Tracks = moovInfo.Tracks;
            this.ReadChapters(moovInfo);
        }
    }

    /// <summary>
    /// Gets a value indicating whether this stream is an MP4A file.
    /// </summary>
    /// <returns><see langword="true" /> if this instance is an MP4A file; otherwise <see langword="false" />.</returns>
    public bool IsMp4a()
    {
        if (this.stream.Length < 8)
        {
            return false;
        }

        this.stream.Seek(4L, SeekOrigin.Begin);
        var type = this.ReadType();
        return type.Check(Ftyp);
    }

    private static T? FirstOrDefaultNullable<T>(IEnumerable<T> source)
        where T : struct
    {
        var enumerator = source.GetEnumerator();
        return enumerator.MoveNext()
            ? enumerator.Current
            : default(T?);
    }

    private void ReadChapters(MoovInfo moovBox)
    {
        var soundBox = moovBox.Tracks.Where(b => string.Equals(b.Type, "soun", StringComparison.Ordinal)).ToArray();
        if (soundBox.Length == 0)
        {
            return;
        }

        if (soundBox[0].Chaps?.Length > 0)
        {
            var chapterIds = new HashSet<uint>(soundBox[0].Chaps);
            var textBox = moovBox.Tracks.Where(b => string.Equals(b.Type, "text", StringComparison.Ordinal) && chapterIds.Contains(b.Id)).ToArray();
            if (textBox.Length == 0)
            {
                return;
            }

            this.ReadChaptersText(textBox[0]);
        }
    }

    private void ReadChaptersText(TrakInfo textBox)
    {
        if (textBox.Durations is not null)
        {
            var length = Math.Min(textBox.Durations.Length, textBox.Samples?.Length ?? int.MaxValue);
            if (length > 0)
            {
                this.Chapters = new ChapterInfo[length];
                var position = TimeSpan.FromSeconds(0);
                var timeUnitPerSecond = (double)textBox.TimeUnitPerSecond;
                if (timeUnitPerSecond <= 0.1)
                {
                    timeUnitPerSecond = 600;
                }

                for (var i = 0; i < length; i++)
                {
                    var chapterInfo = new ChapterInfo { Time = position };
                    var duration = (double)textBox.Durations[i];
                    position += TimeSpan.FromSeconds(duration / timeUnitPerSecond);
                    if (textBox.Samples is not null)
                    {
                        this.stream.Seek(textBox.Samples[i], SeekOrigin.Begin);
                        chapterInfo.Name = this.ReadPascalString(System.Text.Encoding.UTF8);
                    }

                    this.Chapters[i] = chapterInfo;
                }
            }
        }
    }

    private BoxInfo? FindBox(byte[] type)
    {
        return FirstOrDefaultNullable(this.EnumerateBoxes().Where(box => box.Type.Check(type)));
    }

    private IEnumerable<BoxInfo> EnumerateBoxes(long? maximumLength = null)
    {
        bool last;
        do
        {
            last = true;
            if (this.NextBox(maximumLength) is BoxInfo box)
            {
                yield return box;

                this.SeekNext(box);

                last = box.Last;
            }
        }
        while (!last);
    }

    private MoovInfo? ReadMoovInfo()
    {
        if (this.FindBox(Moov) is BoxInfo moovBox)
        {
            var moovData = default(MoovInfo);
            var tracks = new List<TrakInfo>();
            var maximumLength = moovBox.BoxOffset + moovBox.Offset;
            foreach (var box in this.EnumerateBoxes(maximumLength))
            {
                if (box.Type.Check(Mvhd))
                {
                    this.ReadMvhd(ref moovData);
                }

                if (box.Type.Check(Trak))
                {
                    tracks.Add(this.ReadTrak(box));
                }
            }

            moovData.Tracks = tracks.ToArray();
            return moovData;
        }

        return null;
    }

    private TrakInfo ReadTrak(BoxInfo trakBox)
    {
        var maximumLength = trakBox.BoxOffset + trakBox.Offset;
        var trakData = default(TrakInfo);
        foreach (var box in this.EnumerateBoxes(maximumLength))
        {
            if (box.Type.Check(Tkhd))
            {
                this.ReadTkhd(ref trakData);
            }

            if (box.Type.Check(Mdia))
            {
                this.ReadMdia(ref trakData, box);
            }

            if (box.Type.Check(Tref))
            {
                this.ReadTref(ref trakData, box);
            }
        }

        return trakData;
    }

    private void ReadTref(ref TrakInfo trakData, BoxInfo box2)
    {
        foreach (var box in this
            .EnumerateBoxes(box2.BoxOffset + box2.Offset)
            .Where(box => box.Type.Check(Chap)))
        {
            this.ReadChap(ref trakData, box);
        }
    }

    private void ReadChap(ref TrakInfo trakData, BoxInfo box2)
    {
        var length = (box2.Offset - 8) / 4;
        if (length is > 0 and < 1024)
        {
            trakData.Chaps = new uint[length];
            for (var i = 0; i < length; i++)
            {
                trakData.Chaps[i] = this.ReadUInt32();
            }
        }
    }

    private void ReadMdia(ref TrakInfo trackData, BoxInfo mdiaBox)
    {
        foreach (var box in this.EnumerateBoxes(mdiaBox.BoxOffset + mdiaBox.Offset))
        {
            if (box.Type.Check(Mdhd))
            {
                this.ReadMdhd(ref trackData);
            }

            if (box.Type.Check(Hdlr))
            {
                this.ReadHdlr(ref trackData);
            }

            if (box.Type.Check(Minf))
            {
                this.ReadMinf(ref trackData, box);
            }
        }
    }

    private void ReadMinf(ref TrakInfo trakData, BoxInfo box2)
    {
        foreach (var box in this
            .EnumerateBoxes(box2.BoxOffset + box2.Offset)
            .Where(box => box.Type.Check(Stbl)))
        {
            this.ReadStbl(ref trakData, box);
        }
    }

    private void ReadStbl(ref TrakInfo trakData, BoxInfo box2)
    {
        foreach (var boxType in this
            .EnumerateBoxes(box2.BoxOffset + box2.Offset)
            .Select(box => box.Type))
        {
            if (boxType.Check(Stco))
            {
                this.ReadStco(ref trakData);
            }

            if (boxType.Check(Stts))
            {
                this.ReadStts(ref trakData);
            }
        }
    }

    private void ReadStts(ref TrakInfo trakData)
    {
        this.stream.Seek(4L, SeekOrigin.Current);
        var len = this.ReadUInt32();
        if (len > 1024)
        {
            len = 0;
        }

        trakData.Durations = new uint[len];
        trakData.FrameCount = new uint[len];
        for (uint i = 0; i < len; i++)
        {
            trakData.FrameCount[i] = this.ReadUInt32();
            trakData.Durations[i] = this.ReadUInt32();
        }
    }

    private void ReadStco(ref TrakInfo trakData)
    {
        this.stream.Seek(4L, SeekOrigin.Current);
        var len = this.ReadUInt32();
        if (len > 1024)
        {
            len = 0;
        }

        trakData.Samples = new long[len];
        for (uint i = 0; i < len; i++)
        {
            trakData.Samples[i] = this.ReadUInt32();
        }
    }

    private void ReadHdlr(ref TrakInfo trakData)
    {
        this.stream.Seek(8, SeekOrigin.Current);
        var b = new byte[4];
        _ = this.stream.Read(b, 0, 4);
        var bc = new char[4];
        _ = System.Text.Encoding.ASCII.GetDecoder().GetChars(b, 0, 4, bc, 0);
        trakData.Type = new string(bc);
    }

    private void ReadMdhd(ref TrakInfo trakData)
    {
        var v = new byte[1];
        _ = this.stream.Read(v, 0, 1);
        var isv8 = v[0] == 1;

        this.stream.Seek(3L + (isv8 ? 16L : 8L), SeekOrigin.Current);
        trakData.TimeUnitPerSecond = this.ReadUInt32();

        // get the duration
        trakData.Duration = isv8
            ? this.ReadUInt64()
            : this.ReadUInt32();

        var data = this.ReadUInt16();
        var code = new char[]
        {
            (char)(((data & 0x7c00) >> 10) + 0x60),
            (char)(((data & 0x03e0) >> 5) + 0x60),
            (char)((data & 0x001f) + 0x60),
        };

        trakData.Language = new string(code);
    }

    private void ReadTkhd(ref TrakInfo trakData)
    {
        var v = new byte[1];
        _ = this.stream.Read(v, 0, 1);
        var isv8 = v[0] == 1;
        this.stream.Seek(3L + (isv8 ? 16L : 8L), SeekOrigin.Current);
        trakData.Id = this.ReadUInt32();
    }

    private void ReadMvhd(ref MoovInfo moovData)
    {
        var v = new byte[1];
        _ = this.stream.Read(v, 0, 1);
        var isv8 = v[0] == 1;
        this.stream.Seek(3L + (isv8 ? 16L : 8L), SeekOrigin.Current);
        moovData.TimeUnitPerSecond = this.ReadUInt32();
    }

    private void SeekNext(BoxInfo box)
    {
        this.stream.Seek(box.BoxOffset, SeekOrigin.Begin);
        this.stream.Seek(box.Offset, SeekOrigin.Current);
    }

    private BoxInfo? NextBox(long? maxLen = null)
    {
        var currentPosition = this.stream.Position;
        var length = maxLen ?? this.stream.Length;
        if (length - this.stream.Position < 8)
        {
            return null;
        }

        long ofs = this.ReadUInt32();
        var at = this.ReadType();
        if (!at.Check(Mdat))
        {
            return new BoxInfo()
            {
                BoxOffset = currentPosition,
                Offset = ofs,
                Last = ofs == 0,
                Type = at,
            };
        }

        if (length - this.stream.Position < 8)
        {
            return null;
        }

        if (ofs == 1)
        {
            ofs = (long)this.ReadUInt64();
        }
        else
        {
            this.stream.Seek(8L, SeekOrigin.Current);
        }

        return new BoxInfo()
        {
            BoxOffset = currentPosition,
            Offset = ofs,
            Last = ofs == 0,
            Type = at,
        };
    }

    private AsciiType ReadType()
    {
        var b = new byte[4];
        _ = this.stream.Read(b, 0, 4);
        return new AsciiType { Type = b };
    }

    private ushort ReadUInt16()
    {
        var bytes = new byte[2];
        if (this.stream.Read(bytes, 0, 2) != 2)
        {
            return 0;
        }

        if (BitConverter.IsLittleEndian)
        {
            var swap = new byte[2];
            swap[0] = bytes[1];
            swap[1] = bytes[0];
            return BitConverter.ToUInt16(swap, 0);
        }

        return BitConverter.ToUInt16(bytes, 0);
    }

    private uint ReadUInt32()
    {
        var bytes = new byte[4];
        if (this.stream.Read(bytes, 0, 4) != 4)
        {
            return 0;
        }

        if (BitConverter.IsLittleEndian)
        {
            var swap = new byte[4];
            swap[0] = bytes[3];
            swap[1] = bytes[2];
            swap[2] = bytes[1];
            swap[3] = bytes[0];
            return BitConverter.ToUInt32(swap, 0);
        }

        return BitConverter.ToUInt32(bytes, 0);
    }

    private ulong ReadUInt64()
    {
        var bytes = new byte[8];
        if (this.stream.Read(bytes, 0, 8) != 8)
        {
            return 0;
        }

        if (BitConverter.IsLittleEndian)
        {
            var swap = new byte[8];
            swap[0] = bytes[7];
            swap[1] = bytes[6];
            swap[2] = bytes[5];
            swap[3] = bytes[4];
            swap[4] = bytes[3];
            swap[5] = bytes[2];
            swap[6] = bytes[1];
            swap[7] = bytes[0];
            return BitConverter.ToUInt64(swap, 0);
        }

        return BitConverter.ToUInt64(bytes, 0);
    }

    private string ReadPascalString(System.Text.Encoding encoding)
    {
        var @ushort = this.ReadUInt16();
        if (@ushort == 0)
        {
            return string.Empty;
        }

        var b = new byte[@ushort];
        _ = this.stream.Read(b, 0, @ushort);
        return new string(encoding.GetChars(b));
    }
}