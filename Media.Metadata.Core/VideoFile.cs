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

    private static readonly TagLib.ReadOnlyByteVector PodCastDescriptionAtom = "ldes";

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
    public static Movie? ReadMovieMetadata(string fileName)
    {
        using var tagLibFile = TagLib.File.Create(fileName);

        if (tagLibFile.GetTag(TagLib.TagTypes.Apple) is TagLib.Mpeg4.AppleTag appleTag && appleTag.IsMovie())
        {
            var plist = PList.Create(appleTag.GetDashBox("com.apple.iTunes", "iTunMOVI"));

            var movie = new Movie(
                appleTag.Title,
                appleTag.Description,
                GetPersonel(plist, "producers").ToArray(),
                GetPersonel(plist, "directors").ToArray(),
                plist["studio"]?.ToString().Split(',').Select(studio => studio.Trim()).ToArray() ?? Enumerable.Empty<string>(),
                appleTag.Genres,
                GetPersonel(plist, "screenwriters").ToArray(),
                GetPersonel(plist, "cast").ToArray(),
                Array.Empty<string>());

            if (appleTag.GetText(DayAtom).FirstOrDefault() is string day
                && DateTime.TryParse(day, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var release))
            {
                movie = movie with { Release = release };
            }

            if (appleTag.GetDashBox("com.apple.iTunes", "iTunEXTC") is string ratingString
                && Rating.TryParse(ratingString, out var rating))
            {
                movie = movie with { Rating = rating };
            }

            return movie;
        }

        return default;
    }

    /// <summary>
    /// Reads the movie.
    /// </summary>
    /// <returns>The movie.</returns>
    public Movie ReadMovieMetadata() => ReadMovieMetadata(this.fileName) ?? throw new InvalidOperationException();

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
}