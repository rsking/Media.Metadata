// -----------------------------------------------------------------------
// <copyright file="Mp4Writer.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Windows;

/// <summary>
/// The MP4v2 writer.
/// </summary>
public class Mp4Writer : IUpdater
{
    /// <inheritdoc/>
    public void UpdateEpisode(string fileName, Episode episode) => throw new NotImplementedException();

    /// <inheritdoc/>
    public void UpdateMovie(string fileName, Movie movie)
    {
        var file = Mp4File.Open(fileName);
        if (file.Tags is not null)
        {
            file.Tags.MediaType = MediaKind.Movie;
            file.Tags.Title = movie.Name;
            file.Tags.Description = movie.Description;
            file.Tags.LongDescription = movie.Description;
            file.Tags.Genre = ToString(movie.Genre);
            file.Tags.Composer = ToString(movie.Composers);
            file.Tags.AlbumArtist = ToString(movie.Cast);
            file.Tags.ReleaseDate = movie.Release?.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            file.Tags.Artwork = movie.Image;

            file.Tags.RatingInfo = default;
            if (movie.Rating is not null)
            {
                var rating = movie.Rating.Value;
                var ratingInfo = new RatingInfo
                {
                    RatingSource = rating.Standard,
                    Rating = rating.ContentRating,
                    SortValue = rating.Score,
                };

                file.Tags.RatingInfo = ratingInfo;
            }

            file.Tags.MovieInfo ??= new MovieInfo();
            file.Tags.MovieInfo.RemoveProducers();
            if (movie.Producers is not null)
            {
                foreach (var producer in movie.Producers)
                {
                    file.Tags.MovieInfo.Producers.Add(producer);
                }
            }

            file.Tags.MovieInfo.RemoveDirectors();
            if (movie.Directors is not null)
            {
                foreach (var director in movie.Directors)
                {
                    file.Tags.MovieInfo.Directors.Add(director);
                }
            }

            file.Tags.MovieInfo.RemoveCast();
            if (movie.Cast is not null)
            {
                foreach (var cast in movie.Cast)
                {
                    file.Tags.MovieInfo.Cast.Add(cast);
                }
            }

            file.Tags.MovieInfo.RemoveScreenwriters();
            if (movie.ScreenWriters is not null)
            {
                foreach (var screenWriter in movie.ScreenWriters)
                {
                    file.Tags.MovieInfo.Screenwriters.Add(screenWriter);
                }
            }

            file.Tags.MovieInfo.Studio = ToString(movie.Studios);

            static string? ToString(IEnumerable<string>? value)
            {
                if (value is null)
                {
                    return default;
                }

                return string.Join(", ", value);
            }
        }

        file.Save();
    }
}