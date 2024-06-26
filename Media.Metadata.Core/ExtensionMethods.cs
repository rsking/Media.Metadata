﻿// -----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The extension methods.
/// </summary>
internal static class ExtensionMethods
{
    /// <summary>
    /// Gets the image asynchronously.
    /// </summary>
    /// <param name="remoteVideo">The remote video.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image.</returns>
    public static async ValueTask<(Image Image, SixLabors.ImageSharp.Formats.IImageFormat ImageFormat)> DownloadImageAsync(this IRemoteImage remoteVideo, CancellationToken cancellationToken = default)
    {
        if (remoteVideo.ImageUri is { } imageUri)
        {
            using var client = new HttpClient();
            using var stream = await client.GetStreamAsync(imageUri).ConfigureAwait(false);
            return await Image.LoadWithFormatAsync(stream, cancellationToken).ConfigureAwait(false);
        }

        return default;
    }

    /// <summary>
    /// Gets the file name.
    /// </summary>
    /// <param name="renamer">The file name.</param>
    /// <param name="localVideo">The local video.</param>
    /// <returns>The renamed file name.</returns>
    public static string? GetFileName(this IRenamer renamer, LocalVideo localVideo) => renamer.GetFileName(localVideo.FileInfo.FullName, localVideo);
}