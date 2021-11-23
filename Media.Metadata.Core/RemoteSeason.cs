// -----------------------------------------------------------------------
// <copyright file="RemoteSeason.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <inheritdoc/>
public record class RemoteSeason(int Number, IEnumerable<RemoteEpisode> RemoteEpisodes) : Season(Number, RemoteEpisodes), IRemoteImage
{
    /// <inheritdoc/>
    public Uri? ImageUri { get; init; }

    /// <inheritdoc/>
    protected override ValueTask<System.Drawing.Image?> GetImageAsync() => this.DownloadImageAsync();
}