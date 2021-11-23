// -----------------------------------------------------------------------
// <copyright file="RemoteEpisode.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// A remote <see cref="Episode"/>.
/// </summary>
/// <param name="Name">The name.</param>
/// <param name="Description">The description.</param>
public record class RemoteEpisode(
    string? Name,
    string? Description) : Episode(Name, Description, default, default, default, default, default, default, default), IRemoteImage
{
    /// <summary>
    /// Gets the image URI.
    /// </summary>
    public Uri? ImageUri { get; init; }

    /// <inheritdoc />
    protected override ValueTask<System.Drawing.Image?> GetImageAsync() => this.DownloadImageAsync();
}