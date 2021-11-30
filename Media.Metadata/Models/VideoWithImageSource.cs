// -----------------------------------------------------------------------
// <copyright file="VideoWithImageSource.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

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
    private bool disposedValue;

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

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing && !this.disposedValue)
        {
            if (this.ImageSource is System.IDisposable imageSourceDisposable)
            {
                imageSourceDisposable.Dispose();
            }

            if (this.SoftwareBitmap is System.IDisposable softwareBitmapDisposable)
            {
                softwareBitmapDisposable.Dispose();
            }
        }
    }

    /// <inheritdoc/>
    protected override async ValueTask DisposeAsyncCore()
    {
        await base.DisposeAsyncCore().ConfigureAwait(false);

        await DisposeAsync(this.ImageSource).ConfigureAwait(false);
        await DisposeAsync(this.SoftwareBitmap).ConfigureAwait(false);
        this.disposedValue = true;

        static async ValueTask DisposeAsync(object? value)
        {
            if (value is System.IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync().ConfigureAwait(false);
            }
            else if (value is System.IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}