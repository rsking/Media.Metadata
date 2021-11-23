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
    /// <returns>The image.</returns>
    public static async ValueTask<System.Drawing.Image?> DownloadImageAsync(this IRemoteImage remoteVideo)
    {
        if (remoteVideo.ImageUri is null)
        {
            return default;
        }

        using var client = new HttpClient();
        using var stream = await client.GetStreamAsync(remoteVideo.ImageUri).ConfigureAwait(false);
        return System.Drawing.Image.FromStream(stream);
    }
}