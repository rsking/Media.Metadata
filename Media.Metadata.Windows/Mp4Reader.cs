// -----------------------------------------------------------------------
// <copyright file="Mp4Reader.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Windows;

/// <summary>
/// The MP4v2 reader.
/// </summary>
public class Mp4Reader : IReader
{
    /// <inheritdoc/>
    public Movie ReadMovie(string path)
    {
        var file = Mp4File.Open(path);
        var tags = file.Tags;
        if (tags is null)
        {
            throw new ArgumentException(default, nameof(path));
        }

        var movie = new LocalMovie(
            tags.Title,
            tags.Description,
            tags.MovieInfo?.Producers,
            tags.MovieInfo?.Directors,
            Split(tags.MovieInfo?.Studio),
            Split(tags.Genre),
            tags.MovieInfo?.Screenwriters,
            tags.MovieInfo?.Cast,
            Split(tags.Composer));

        if (tags.ReleaseDate is not null
            && DateTime.TryParse(tags.ReleaseDate, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var releaseDate))
        {
            movie = movie with { Release = releaseDate };
        }

        if (tags.RatingInfo is not null
            && Rating.TryParse(tags.RatingInfo.ToString(), out var rating))
        {
            movie = movie with { Rating = rating };
        }

        if (tags.ArtworkCount > 0)
        {
            movie = movie with { Image = tags.Artwork };
        }

        return movie;

        static IEnumerable<string>? Split(string? value)
        {
            return value?.Split(',').Select(v => v.Trim());
        }
    }
}