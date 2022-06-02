// -----------------------------------------------------------------------
// <copyright file="ImageExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using SixLabors.ImageSharp;

/// <summary>
/// <see cref="ImageSource"/> helpers.
/// </summary>
internal static class ImageExtensions
{
    /// <summary>
    /// Creates an image source.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image source.</returns>
    public static async Task<Windows.Graphics.Imaging.SoftwareBitmap?> CreateSoftwareBitmapAsync(this IHasImage video, CancellationToken cancellationToken = default)
    {
        return video.Image switch
        {
            Image image => await image.CreateSoftwareBitmapAsync(video.ImageFormat, cancellationToken).ConfigureAwait(false),
            _ => default,
        };
    }

    /// <summary>
    /// Creates an image source.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="imageFormat">The image format.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image source.</returns>
    public static async Task<Windows.Graphics.Imaging.SoftwareBitmap?> CreateSoftwareBitmapAsync(this Image image, SixLabors.ImageSharp.Formats.IImageFormat? imageFormat, CancellationToken cancellationToken = default)
    {
        if (image is null)
        {
            return default;
        }

        using var stream = new MemoryStream();
        await image.SaveAsync(stream, imageFormat, cancellationToken).ConfigureAwait(false);
        var bitmapDecoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
        return await bitmapDecoder.GetSoftwareBitmapAsync();
    }

    /// <summary>
    /// Creates an image source.
    /// </summary>
    /// <param name="video">The image.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image source.</returns>
    public static async Task<ImageSource?> CreateImageSource(this IHasImage video, CancellationToken cancellationToken = default)
    {
        return video.Image switch
        {
            Image image => await image.CreateImageSource(video.ImageFormat, cancellationToken).ConfigureAwait(true),
            _ => default,
        };
    }

    /// <summary>
    /// Creates an image source.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="imageFormat">The image format.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image source.</returns>
    public static async Task<ImageSource?> CreateImageSource(this Image image, SixLabors.ImageSharp.Formats.IImageFormat? imageFormat, CancellationToken cancellationToken = default)
    {
        using var softwareBitmap = await image.CreateSoftwareBitmapAsync(imageFormat, cancellationToken).ConfigureAwait(true);
        if (softwareBitmap is not null)
        {
            var displayable = Windows.Graphics.Imaging.SoftwareBitmap.Convert(
                softwareBitmap,
                Windows.Graphics.Imaging.BitmapPixelFormat.Bgra8,
                Windows.Graphics.Imaging.BitmapAlphaMode.Premultiplied);

            var source = new Microsoft.UI.Xaml.Media.Imaging.SoftwareBitmapSource();
            await source.SetBitmapAsync(displayable);
            return source;
        }

        return default;
    }
}