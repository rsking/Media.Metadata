// -----------------------------------------------------------------------
// <copyright file="IRemoteImage.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Represents a remote image resource.
/// </summary>
internal interface IRemoteImage
{
    /// <summary>
    /// Gets the image URI.
    /// </summary>
    Uri? ImageUri { get; }
}