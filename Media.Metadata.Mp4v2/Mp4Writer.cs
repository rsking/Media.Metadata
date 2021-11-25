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
    public void UpdateVideo(string fileName, Video video, IDictionary<int, string>? languages = default)
    {
        if (video is Episode episode)
        {
            this.UpdateEpisode(fileName, episode, languages);
        }
        else if (video is Movie movie)
        {
            this.UpdateMovie(fileName, movie, languages);
        }
        else
        {
            var file = Mp4File.Open(fileName);
            var mediaType = file.Tags is null
                ? MediaKind.NotSet
                : file.Tags.MediaType;
            Update(file, video, mediaType, languages);
            file.Save();
        }
    }

    /// <inheritdoc/>
    public void UpdateEpisode(string fileName, Episode episode, IDictionary<int, string>? languages = default)
    {
        var file = Mp4File.Open(fileName);
        Update(file, episode, MediaKind.TVShow, languages);
        if (file.Tags is not null)
        {
            // episode
            file.Tags.EpisodeNumber = ValueOrNull(episode.Number);
            file.Tags.SeasonNumber = ValueOrNull(episode.Season);
            file.Tags.TVShow = episode.Show;
            file.Tags.EpisodeId = episode.Id;
            file.Tags.TVNetwork = episode.Network;
        }

        file.Save();

        static int? ValueOrNull(int value)
        {
            return value < 0 ? null : value;
        }
    }

    /// <inheritdoc/>
    public void UpdateMovie(string fileName, Movie movie, IDictionary<int, string>? languages = default)
    {
        var file = Mp4File.Open(fileName);
        Update(file, movie, MediaKind.Movie, languages);
        file.Save();
    }

    private static void Update(Mp4File file, Video video, MediaKind mediaKind, IDictionary<int, string>? languages)
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

            if (languages is not null && file.Tracks is not null)
            {
                foreach (var language in languages)
                {
                    var tracks = language.Key == -1 ? file.Tracks : file.Tracks.Where(track => track.Id == language.Key);
                    foreach (var track in tracks)
                    {
                        track.Language = language.Value;
                    }
                }
            }

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