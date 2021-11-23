// -----------------------------------------------------------------------
// <copyright file="RemoteSeries.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <inheritdoc/>
public record class RemoteSeries(string Name, IEnumerable<RemoteSeason> RemoteSeasons) : Series(Name, RemoteSeasons), IRemoteImage
{
    /// <inheritdoc/>
    public Uri? ImageUri { get; init; }

    /// <inheritdoc/>
    protected override ValueTask<System.Drawing.Image?> GetImageAsync() => this.DownloadImageAsync();
}