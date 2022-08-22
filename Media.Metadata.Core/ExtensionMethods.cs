// -----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The extension methods.
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// Gets the chapters from the file.
    /// </summary>
    /// <param name="info">The file information.</param>
    /// <returns>The chapter information.</returns>
    /// <exception cref="InvalidOperationException">No chapters could be extracted.</exception>
    public static IEnumerable<MediaChapter> GetChapters(this FileInfo info)
    {
        using var stream = info.OpenRead();
        var extractor = new Tracks.ChapterExtractor(new Tracks.StreamWrapper(stream));
        extractor.Run();

        if (extractor.Chapters is IEnumerable<Tracks.ChapterInfo> chapters)
        {
            return chapters.Select(chap => new MediaChapter(chap.Name, chap.Time));
        }

        // create a single chapter for the full length from the video track
        if (extractor.Tracks?.FirstOrDefault(t => string.Equals(t.Type, "vide", StringComparison.Ordinal)) is Tracks.TrakInfo videoTrack)
        {
            var duration = videoTrack.Duration;
            var timeScale = videoTrack.TimeUnitPerSecond;
            return Create(new MediaChapter("Chapter 1", TimeSpan.FromSeconds((double)duration / timeScale)));

            static IEnumerable<T> Create<T>(T value)
            {
                yield return value;
            }
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Gets the tracks from the file.
    /// </summary>
    /// <param name="info">The file information.</param>
    /// <returns>The track information.</returns>
    public static IEnumerable<MediaTrack> GetTracks(this FileInfo info)
    {
        using var stream = info.OpenRead();
        var extractor = new Tracks.ChapterExtractor(new Tracks.StreamWrapper(stream));
        extractor.Run();

        if (extractor.Tracks is IEnumerable<Tracks.TrakInfo> tracks)
        {
            return tracks.Select(MediaTrack);

            static MediaTrack MediaTrack(Tracks.TrakInfo trakInfo)
            {
                return new(
                    (int)trakInfo.Id,
                    GetTrackType(trakInfo.Type),
                    trakInfo.Language);

                static MediaTrackType GetTrackType(string type)
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
        }

        return Enumerable.Empty<MediaTrack>();
    }

    /// <summary>
    /// Gets the image asynchronously.
    /// </summary>
    /// <param name="remoteVideo">The remote video.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image.</returns>
    internal static async ValueTask<(Image Image, SixLabors.ImageSharp.Formats.IImageFormat ImageFormat)> DownloadImageAsync(this IRemoteImage remoteVideo, CancellationToken cancellationToken = default)
    {
        if (remoteVideo.ImageUri is null)
        {
            return default;
        }

        using var client = new HttpClient();
        using var stream = await client.GetStreamAsync(remoteVideo.ImageUri).ConfigureAwait(false);
        return await Image.LoadWithFormatAsync(stream, cancellationToken).ConfigureAwait(false);
    }
}