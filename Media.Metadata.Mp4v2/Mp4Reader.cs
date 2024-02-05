// -----------------------------------------------------------------------
// <copyright file="Mp4Reader.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The MP4v2 reader.
/// </summary>
public class Mp4Reader : IReader
{
    /// <inheritdoc/>
    public Video ReadVideo(string path)
    {
        var file = Mp4File.Open(path);
        var tags = file.Tags ?? throw new ArgumentException(default, nameof(path));
        Video video = tags.MediaType switch
        {
            MediaKind.Movie => ReadMovie(path, tags),
            MediaKind.TVShow => ReadEpisode(path, tags),
            _ => ReadVideo(path, tags),
        };

        return Update(video, tags, file.Tracks);
    }

    /// <inheritdoc/>
    public Movie ReadMovie(string path)
    {
        var file = Mp4File.Open(path);
        var tags = file.Tags ?? throw new ArgumentException(default, nameof(path));
        return Update(ReadMovie(path, tags), tags, file.Tracks);
    }

    /// <inheritdoc/>
    public Episode ReadEpisode(string path)
    {
        var file = Mp4File.Open(path);
        var tags = file.Tags ?? throw new ArgumentException(default, nameof(path));
        return Update(ReadEpisode(path, tags), tags, file.Tracks);
    }

    private static LocalVideo ReadVideo(string path, MetadataTags tags) => new(
        new FileInfo(path),
        tags.Title ?? Path.GetFileNameWithoutExtension(path),
        tags.Description,
        tags.MovieInfo?.Producers,
        tags.MovieInfo?.Directors,
        Split(tags.MovieInfo?.Studio),
        Split(tags.Genre),
        tags.MovieInfo?.Screenwriters,
        tags.MovieInfo?.Cast,
        Split(tags.Composer))
    {
        Work = tags.Work,
    };

    private static LocalMovie ReadMovie(string path, MetadataTags tags) => new(
        new FileInfo(path),
        tags.Title ?? Path.GetFileNameWithoutExtension(path),
        tags.Description,
        tags.MovieInfo?.Producers,
        tags.MovieInfo?.Directors,
        Split(tags.MovieInfo?.Studio),
        Split(tags.Genre),
        tags.MovieInfo?.Screenwriters,
        tags.MovieInfo?.Cast,
        Split(tags.Composer))
    {
        Work = tags.Work,
        Edition = tags.Category,
    };

    private static LocalEpisode ReadEpisode(string path, MetadataTags tags) => new(
        new FileInfo(path),
        tags.Title ?? Path.GetFileNameWithoutExtension(path),
        tags.Description,
        tags.MovieInfo?.Producers,
        tags.MovieInfo?.Directors,
        Split(tags.MovieInfo?.Studio),
        Split(tags.Genre),
        tags.MovieInfo?.Screenwriters,
        tags.MovieInfo?.Cast,
        Split(tags.Composer))
    {
        Work = tags.Work,
        Show = tags.TVShow,
        Season = tags.SeasonNumber,
        Network = tags.TVNetwork,
        Number = tags.EpisodeNumber,
        Id = tags.EpisodeId,
        Part = tags.ContentId,
    };

    private static T Update<T>(T video, MetadataTags tags, TrackList? trackList)
        where T : Video
    {
        if (tags.ReleaseDate is not null
            && TryParseDate(tags.ReleaseDate, out var releaseDate))
        {
            video = video with { Release = releaseDate };
        }

        if (tags.RatingInfo is not null
            && Rating.TryParse(tags.RatingInfo.ToString(), out var rating))
        {
            video = video with { Rating = rating };
        }

        if (tags.ArtworkCount > 0)
        {
            video = video with { Image = tags.Artwork, ImageFormat = tags.ArtworkFormat };
        }

        if (trackList?.Count > 0)
        {
            video = video with { Tracks = trackList.Select(track => new MediaTrack(track.Id, GetMediaTrackType(track.Type), track.Language)).ToList() };
        }

        return video;

        static bool TryParseDate(string? date, out DateTime result)
        {
            return DateTime.TryParse(date, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result)
                || DateTime.TryParse(date, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out result);
        }

        static MediaTrackType GetMediaTrackType(string? type)
        {
            return type switch
            {
                "vide" => MediaTrackType.Video,
                "soun" => MediaTrackType.Audio,
                "text" => MediaTrackType.Text,
                _ => MediaTrackType.Unknown,
            };
        }
    }

    private static IEnumerable<string>? Split(string? value) => value?.Split(',').Select(v => v.Trim());
}