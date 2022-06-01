// -----------------------------------------------------------------------
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
        if (remoteVideo.ImageUri is null)
        {
            return default;
        }

        using var client = new HttpClient();
        using var stream = await client.GetStreamAsync(remoteVideo.ImageUri).ConfigureAwait(false);
        return await Image.LoadWithFormatAsync(stream, cancellationToken).ConfigureAwait(false);
    }
}