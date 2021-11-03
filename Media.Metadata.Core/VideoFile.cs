// -----------------------------------------------------------------------
// <copyright file="VideoFile.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The video file.
/// </summary>
public class VideoFile
{
    private static readonly TagLib.ReadOnlyByteVector DayAtom = new(169, 100, 97, 121);

    private readonly string fileName;

    /// <summary>
    /// Initialises a new instance of the <see cref="VideoFile"/> class.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    public VideoFile(string fileName) => this.fileName = fileName;

    /// <summary>
    /// Reads the movie.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <returns>The movie.</returns>
    public static Video? ReadMetadata(string fileName)
    {
        var fileInfo = new FileInfo(fileName);
        using var tagLibFile = TagLib.File.Create(fileName);

        if (tagLibFile.GetTag(TagLib.TagTypes.Apple) is TagLib.Mpeg4.AppleTag appleTag)
        {
            if (appleTag.IsMovie())
            {
                var plist = PList.Create(appleTag.GetDashBox("com.apple.iTunes", "iTunMOVI"));
                return Update(ReadMovieMetadata(fileInfo, appleTag, plist), appleTag);
            }

            if (appleTag.IsTvShow())
            {
                var plist = PList.Create(appleTag.GetDashBox("com.apple.iTunes", "iTunMOVI"));
                return Update(ReadEpisodeMetadata(fileInfo, appleTag, plist), appleTag);
            }

            return new LocalVideo(fileInfo, Path.GetFileNameWithoutExtension(fileName));
        }

        return default;

        static Movie ReadMovieMetadata(FileInfo fileInfo, TagLib.Mpeg4.AppleTag appleTag, PList plist)
        {
            return new LocalMovie(
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
        }

        static Episode ReadEpisodeMetadata(FileInfo fileInfo, TagLib.Mpeg4.AppleTag appleTag, PList plist)
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

        static Video Update(Video video, TagLib.Mpeg4.AppleTag appleTag)
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
    }

    /// <summary>
    /// Reads the video.
    /// </summary>
    /// <returns>The video.</returns>
    public Video ReadMetadata() => ReadMetadata(this.fileName) ?? throw new InvalidOperationException();

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

    private static IEnumerable<string> SplitArray(IEnumerable<string> values)
    {
        return values
            .SelectMany(value => value.Split(','))
            .Select(value => value.Trim());
    }
}