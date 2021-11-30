// -----------------------------------------------------------------------
// <copyright file="ImageExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System;
using System.Runtime.InteropServices.WindowsRuntime;

/// <summary>
/// <see cref="Microsoft.UI.Xaml.Media.ImageSource"/> helpers.
/// </summary>
internal static class ImageExtensions
{
    /// <summary>
    /// Creates an image source.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <returns>The image source.</returns>
    public static async Task<Windows.Graphics.Imaging.SoftwareBitmap?> CreateSoftwareBitmapAsync(this Video video)
    {
        if (video.Image is System.Drawing.Image image)
        {
            using var stream = new MemoryStream();
            using (var bitmap = new System.Drawing.Bitmap(image))
            {
                bitmap.Save(stream, image.RawFormat);
            }

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
    /// <returns>The image.</returns>
    public static async Task<System.Drawing.Image?> CreateImageAsync(this Models.IHasSoftwareBitmap source)
    {
        if (source.SoftwareBitmap is Windows.Graphics.Imaging.SoftwareBitmap softwareBitmap)
        {
            var bytes = await EncodedBytes(softwareBitmap, Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId).ConfigureAwait(false);
            if (bytes is not null)
            {
                using var stream = new MemoryStream(bytes);
                stream.Position = 0;
                return System.Drawing.Image.FromStream(stream);
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