// -----------------------------------------------------------------------
// <copyright file="IRemoteVideo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Represents a remove video.
/// </summary>
internal interface IRemoteVideo
{
    /// <summary>
    /// Gets the image URI.
    /// </summary>
    Uri? ImageUri { get; }
}