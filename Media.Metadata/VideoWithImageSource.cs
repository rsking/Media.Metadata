// -----------------------------------------------------------------------
// <copyright file="VideoWithImageSource.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// A video with an <see cref="Microsoft.UI.Xaml.Media.ImageSource"/>.
/// </summary>
/// <inheritdoc />
internal record class VideoWithImageSource(
    string? Name,
    string? Description,
    IEnumerable<string>? Producers,
    IEnumerable<string>? Directors,
    IEnumerable<string>? Studios,
    IEnumerable<string>? Genre,
    IEnumerable<string>? ScreenWriters,
    IEnumerable<string>? Cast,
    IEnumerable<string>? Composers) : Video(Name, Description, Producers, Directors, Studios, Genre, ScreenWriters, Cast, Composers), IHasImageSource, IHasSoftwareBitmap
{
    /// <inheritdoc/>
    public Windows.Graphics.Imaging.SoftwareBitmap? SoftwareBitmap { get; init; }

    /// <inheritdoc/>
    public Microsoft.UI.Xaml.Media.ImageSource? ImageSource { get; init; }

    /// <summary>
    /// Creates a <see cref="VideoWithImageSource"/> from a <see cref="LocalVideo"/>.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <returns>The video with image source.</returns>
    public static async Task<VideoWithImageSource> CreateAsync(Video video)
    {
        var softwareBitmap = await video.CreateSoftwareBitmapAsync().ConfigureAwait(true);
        return new VideoWithImageSource(video.Name, video.Description, video.Producers, video.Directors, video.Studios, video.Genre, video.ScreenWriters, video.Cast, video.Composers)
        {
            Release = video.Release,
            Rating = video.Rating,
            SoftwareBitmap = softwareBitmap,
            ImageSource = await softwareBitmap.CreateImageSourceAsync().ConfigureAwait(true),
        };
    }
}