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
/// <see cref="Microsoft.UI.Xaml.Media.ImageSource"/> helpers.
/// </summary>
internal static class ImageExtensions
{
    /// <summary>
    /// Creates an image source.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image source.</returns>
    public static async Task<Windows.Graphics.Imaging.SoftwareBitmap?> CreateSoftwareBitmapAsync(this Video video, CancellationToken cancellationToken = default)
    {
        if (video.Image is Image image)
        {
            using var stream = new MemoryStream();
            await image.SaveAsync(stream, video.ImageFormat, cancellationToken).ConfigureAwait(false);
            var bitmapDecoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
            return await bitmapDecoder.GetSoftwareBitmapAsync();
        }

        return default;
    }

    /// <summary>
    /// Creates an image source.
    /// </summary>
    /// <param name="softwareBitmap">The software bitmap.</param>
    /// <returns>The image source.</returns>
    public static async Task<Microsoft.UI.Xaml.Media.ImageSource?> CreateImageSourceAsync(this Windows.Graphics.Imaging.SoftwareBitmap? softwareBitmap)
    {
        if (softwareBitmap is null)
        {
            return default;
        }

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
        return softwareBitmapSource;
    }

    /// <summary>
    /// Creates an image.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image.</returns>
    public static async Task<(Image? Image, SixLabors.ImageSharp.Formats.IImageFormat ImageFormat)> CreateImageAsync(this Models.IHasSoftwareBitmap source, CancellationToken cancellationToken = default)
    {
        if (source.SoftwareBitmap is Windows.Graphics.Imaging.SoftwareBitmap softwareBitmap)
        {
            var bytes = await EncodedBytes(softwareBitmap, Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId).ConfigureAwait(false);
            if (bytes is not null)
            {
                using var stream = new MemoryStream(bytes);
                stream.Position = 0;
                return await Image.LoadWithFormatAsync(stream, cancellationToken).ConfigureAwait(false);
            }
        }

        return default;
    }

    private static async Task<byte[]?> EncodedBytes(Windows.Graphics.Imaging.SoftwareBitmap soft, Guid encoderId)
    {
        byte[]? array = null;

        // First: Use an encoder to copy from SoftwareBitmap to an in-mem stream (FlushAsync)
        // Next:  Use ReadAsync on the in-mem stream to get byte[] array
        using (var ms = new Windows.Storage.Streams.InMemoryRandomAccessStream())
        {
            var encoder = await Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(encoderId, ms);
            encoder.SetSoftwareBitmap(soft);

            try
            {
                await encoder.FlushAsync();
            }
            catch (Exception)
            {
                return null;
            }

            array = new byte[ms.Size];
            await ms.ReadAsync(array.AsBuffer(), (uint)ms.Size, Windows.Storage.Streams.InputStreamOptions.None);
        }

        return array;
    }
}