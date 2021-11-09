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
    IEnumerable<string>? Composers) : Video(Name, Description, Producers, Directors, Studios, Genre, ScreenWriters, Cast, Composers)
{
    /// <summary>
    /// Gets the image source.
    /// </summary>
    public Microsoft.UI.Xaml.Media.ImageSource? ImageSource { get; init; }

    /// <summary>
    /// Creates a <see cref="VideoWithImageSource"/> from a <see cref="Video"/>.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <returns>The video with image source.</returns>
    public static async Task<VideoWithImageSource> CreateAsync(Video video)
    {
        Microsoft.UI.Xaml.Media.ImageSource? imageSource = default;
        if (video.Image is System.Drawing.Image image)
        {
            using var stream = new MemoryStream();
            using (var bitmap = new System.Drawing.Bitmap(image))
            {
                bitmap.Save(stream, image.RawFormat);
            }

            var bitmapDecoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
            var softwareBitmap = await bitmapDecoder.GetSoftwareBitmapAsync();
            if (softwareBitmap.BitmapPixelFormat != Windows.Graphics.Imaging.BitmapPixelFormat.Bgra8 ||
                softwareBitmap.BitmapAlphaMode == Windows.Graphics.Imaging.BitmapAlphaMode.Straight)
            {
                softwareBitmap = Windows.Graphics.Imaging.SoftwareBitmap.Convert(
                    softwareBitmap,
                    Windows.Graphics.Imaging.BitmapPixelFormat.Bgra8,
                    Windows.Graphics.Imaging.BitmapAlphaMode.Premultiplied);
            }

            var softwareBitmapSource = new Microsoft.UI.Xaml.Media.Imaging.SoftwareBitmapSource();
            await softwareBitmapSource.SetBitmapAsync(softwareBitmap);
            imageSource = softwareBitmapSource;
        }

        return new VideoWithImageSource(video.Name, video.Description, video.Producers, video.Directors, video.Studios, video.Genre, video.ScreenWriters, video.Cast, video.Composers)
        {
            ImageSource = imageSource,
        };
    }
}