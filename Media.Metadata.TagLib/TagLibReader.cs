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
    /// <inheritdoc/>
    public Episode ReadEpisode(string path) => ReadVideo(path, (fileInfo, appleTag) => ReadEpisode(fileInfo, appleTag, CreatePList(appleTag)));

    /// <inheritdoc/>
    public Movie ReadMovie(string path) => ReadVideo(path, (fileInfo, appleTag) => ReadMovie(fileInfo, appleTag, CreatePList(appleTag)));

    /// <inheritdoc/>
    public Video ReadVideo(string path) => ReadVideo(path, ReadVideo);

    private static T ReadVideo<T>(string path, Func<FileInfo, TagLib.Mpeg4.AppleTag, T> func)
        where T : Video
    {
        var fileInfo = new FileInfo(path);
        using var tagLibFile = TagLib.File.Create(fileInfo.FullName);

        return tagLibFile.GetTag(TagLib.TagTypes.Apple) is TagLib.Mpeg4.AppleTag appleTag
            ? Update(fileInfo, func(fileInfo, appleTag), appleTag)
            : throw new ArgumentException(default, nameof(path));
    }

    private static Video ReadVideo(FileInfo fileInfo, TagLib.Mpeg4.AppleTag appleTag) => appleTag switch
    {
        var t when t.GetMediaType() == MediaType.Movie => ReadMovie(fileInfo, appleTag, CreatePList(appleTag)),
        var t when t.GetMediaType() == MediaType.TVShow => ReadEpisode(fileInfo, appleTag, CreatePList(appleTag)),
        _ => new LocalVideo(fileInfo, Path.GetFileNameWithoutExtension(fileInfo.Name)),
    };

    private static Movie ReadMovie(FileInfo fileInfo, TagLib.Mpeg4.AppleTag appleTag, PList plist) => new LocalMovie(
        fileInfo,
        appleTag.Title,
        appleTag.Description,
        GetPersonel(plist, "producers").ToArray(),
        GetPersonel(plist, "directors").ToArray(),
        plist.ContainsKey("studio") ? plist["studio"].ToString().Split(',').Select(studio => studio.Trim()).ToArray() : Enumerable.Empty<string>(),
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
            Show = appleTag.GetShowName(),
            Network = appleTag.GetNetwork(),
            Season = appleTag.GetSeasonNumber() ?? 0,
            Number = appleTag.GetEpisodeNumber() ?? 0,
            Id = appleTag.GetEpisodeId(),
            Part = appleTag.GetContentId() ?? 0,
        };
    }

    private static T Update<T>(FileInfo info, T video, TagLib.Mpeg4.AppleTag appleTag)
        where T : Video
    {
        if (appleTag.GetReleaseDate() is string day
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

        if (appleTag.Pictures is IList<TagLib.IPicture> { Count: > 0 } pictures)
        {
            video = video with
            {
                Image = SixLabors.ImageSharp.Image.Load(pictures[0].Data.Data, out var imageFormat),
                ImageFormat = imageFormat,
            };
        }

        // extract chapters and tracks
        if (info.GetTracks().ToArray() is { Length: > 0 } tracks)
        {
            video = video with { Tracks = tracks };
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