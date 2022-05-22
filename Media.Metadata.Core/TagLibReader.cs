// -----------------------------------------------------------------------
// <copyright file="TagLibReader.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The <see cref="TagLib"/> <see cref="IReader"/>.
/// </summary>
public class TagLibReader : IReader
{
    private static readonly TagLib.ReadOnlyByteVector DayAtom = new(169, 100, 97, 121);

    /// <inheritdoc/>
    public Episode ReadEpisode(string path) => ReadVideo(path, (fileInfo, appleTag) => ReadEpisode(fileInfo, appleTag, CreatePList(appleTag)));

    /// <inheritdoc/>
    public Movie ReadMovie(string path) => ReadVideo(path, (fileInfo, appleTag) => ReadMovie(fileInfo, appleTag, CreatePList(appleTag)));

    /// <inheritdoc/>
    public Video ReadVideo(string path) => ReadVideo(path, ReadVideo);

    private static T ReadVideo<T>(string path, Func<FileInfo, TagLib.Mpeg4.AppleTag, T> func)
    {
        var fileInfo = new FileInfo(path);
        using var tagLibFile = TagLib.File.Create(fileInfo.FullName);

        return tagLibFile.GetTag(TagLib.TagTypes.Apple) is TagLib.Mpeg4.AppleTag appleTag
            ? func(fileInfo, appleTag)
            : throw new ArgumentException(default, nameof(path));
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "This would make it harder to read")]
    private static Video ReadVideo(FileInfo fileInfo, TagLib.Mpeg4.AppleTag appleTag)
    {
        if (appleTag.IsMovie())
        {
            return Update(ReadMovie(fileInfo, appleTag, CreatePList(appleTag)), appleTag);
        }

        if (appleTag.IsTvShow())
        {
            return Update(ReadEpisode(fileInfo, appleTag, CreatePList(appleTag)), appleTag);
        }

        return new LocalVideo(fileInfo, Path.GetFileNameWithoutExtension(fileInfo.Name));
    }

    private static Movie ReadMovie(FileInfo fileInfo, TagLib.Mpeg4.AppleTag appleTag, PList plist) => new LocalMovie(
        fileInfo,
        appleTag.Title,
        appleTag.Description,
        GetPersonel(plist, "producers").ToArray(),
        GetPersonel(plist, "directors").ToArray(),
        plist["studio"]?.ToString().Split(',').Select(studio => studio.Trim()).ToArray() ?? Enumerable.Empty<string>(),
        appleTag.Genres,
        GetPersonel(plist, "screenwriters").ToArray(),
        GetPersonel(plist, "cast").ToArray(),
        SplitArray(appleTag.Composers).ToArray());

    private static Episode ReadEpisode(FileInfo fileInfo, TagLib.Mpeg4.AppleTag appleTag, PList plist)
    {
        return new LocalEpisode(
            fileInfo,
            appleTag.Title,
            appleTag.Description,
            GetPersonel(plist, "producers").ToArray(),
            GetPersonel(plist, "directors").ToArray(),
            plist.ContainsKey("studio") ? plist["studio"].ToString().Split(',').Select(studio => studio.Trim()).ToArray() : Enumerable.Empty<string>(),
            appleTag.Genres,
            GetPersonel(plist, "screenwriters").ToArray(),
            GetPersonel(plist, "cast").ToArray(),
            SplitArray(appleTag.Composers).ToArray())
        {
            Show = appleTag.GetText("tvsh").SingleOrDefault(),
            Network = appleTag.GetText("tvnn").SingleOrDefault(),
            Season = GetInt32(appleTag, "tvsn").SingleOrDefault(),
            Number = GetInt32(appleTag, "tves").SingleOrDefault(),
            Id = appleTag.GetText("tven").SingleOrDefault(),
            Part = GetInt32(appleTag, "cnID").FirstOrDefault(),
        };

        static int[] GetInt32(TagLib.Mpeg4.AppleTag appleTag, TagLib.ByteVector byteVector)
        {
            return appleTag
                .DataBoxes(byteVector)
                .Select(box =>
                {
                    var bytes = box.Data.Data;
                    if (BitConverter.IsLittleEndian)
                    {
                        bytes = new[] { bytes[3], bytes[2], bytes[1], bytes[0] };
                    }

                    return BitConverter.ToInt32(bytes, 0);
                }).ToArray();
        }
    }

    private static Video Update(Video video, TagLib.Mpeg4.AppleTag appleTag)
    {
        if (appleTag.GetText(DayAtom).FirstOrDefault() is string day
            && (DateTime.TryParse(day, System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out var release)
            || DateTime.TryParse(day, System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None, out release)))
        {
            video = video with { Release = release.Date };
        }

        if (appleTag.GetDashBox("com.apple.iTunes", "iTunEXTC") is string ratingString
            && Rating.TryParse(ratingString, out var rating))
        {
            video = video with { Rating = rating };
        }

        if (appleTag.Pictures?.Length > 0)
        {
            var picture = appleTag.Pictures[0];
            using var stream = new MemoryStream(picture.Data.Data);
            video = video with { Image = System.Drawing.Image.FromStream(stream) };
        }

        return video;
    }

    private static PList CreatePList(TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetDashBox("com.apple.iTunes", "iTunMOVI") switch
    {
        string dashBox => PList.Create(dashBox),
        _ => new PList(),
    };

    private static IEnumerable<string> GetPersonel(PList plist, string key)
    {
        if (!plist.ContainsKey(key))
        {
            yield break;
        }

        var value = plist[key];
        if (value is object[] @array)
        {
            foreach (var item in array.OfType<IDictionary<string, object>>())
            {
                yield return (string)item["name"];
            }
        }
    }

    private static IEnumerable<string> SplitArray(IEnumerable<string> values) => values
        .SelectMany(value => value.Split(','))
        .Select(value => value.Trim());
}