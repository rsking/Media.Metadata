// -----------------------------------------------------------------------
// <copyright file="Mp4Writer.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The MP4v2 writer.
/// </summary>
public class Mp4Writer : IUpdater
{
    /// <inheritdoc/>
    public void UpdateEpisode(string fileName, Episode episode)
    {
        var file = Mp4File.Open(fileName);
        Update(file, episode, MediaKind.TVShow);
        if (file.Tags is not null)
        {
            // episode
            file.Tags.EpisodeNumber = episode.Number;
            file.Tags.SeasonNumber = episode.Season;
            file.Tags.TVShow = episode.Show;
            file.Tags.EpisodeId = episode.Id;
            file.Tags.TVNetwork = episode.Network;
        }

        file.Save();
    }

    /// <inheritdoc/>
    public void UpdateMovie(string fileName, Movie movie)
    {
        var file = Mp4File.Open(fileName);
        Update(file, movie, MediaKind.Movie);
        file.Save();
    }

    private static void Update(Mp4File file, Video video, MediaKind mediaKind)
    {
        if (file.Tags is not null)
        {
            file.Tags.MediaType = mediaKind;
            file.Tags.Title = video.Name;
            file.Tags.Description = video.Description;
            file.Tags.LongDescription = video.Description;
            file.Tags.Genre = ToString(video.Genre);
            file.Tags.Composer = ToString(video.Composers);
            file.Tags.AlbumArtist = ToString(video.Cast);
            file.Tags.ReleaseDate = video.Release?.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            file.Tags.Artwork = video.Image;

            file.Tags.RatingInfo = default;
            if (video.Rating is not null)
            {
                var rating = video.Rating.Value;
                file.Tags.RatingInfo = new RatingInfo
                {
                    RatingSource = rating.Standard,
                    Rating = rating.ContentRating,
                    SortValue = rating.Score,
                };
            }

            file.Tags.MovieInfo ??= new MovieInfo();
            file.Tags.MovieInfo.RemoveProducers();
            if (video.Producers is not null)
            {
                foreach (var producer in video.Producers)
                {
                    file.Tags.MovieInfo.Producers.Add(producer);
                }
            }

            file.Tags.MovieInfo.RemoveDirectors();
            if (video.Directors is not null)
            {
                foreach (var director in video.Directors)
                {
                    file.Tags.MovieInfo.Directors.Add(director);
                }
            }

            file.Tags.MovieInfo.RemoveCast();
            if (video.Cast is not null)
            {
                foreach (var cast in video.Cast)
                {
                    file.Tags.MovieInfo.Cast.Add(cast);
                }
            }

            file.Tags.MovieInfo.RemoveScreenwriters();
            if (video.ScreenWriters is not null)
            {
                foreach (var screenWriter in video.ScreenWriters)
                {
                    file.Tags.MovieInfo.Screenwriters.Add(screenWriter);
                }
            }

            file.Tags.MovieInfo.Studio = ToString(video.Studios);

            static string? ToString(IEnumerable<string>? value)
            {
                return value switch
                {
                    null => default,
                    _ => string.Join(", ", value),
                };
            }
        }
    }
}