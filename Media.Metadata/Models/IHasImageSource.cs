// -----------------------------------------------------------------------
// <copyright file="IHasImageSource.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

/// <summary>
/// Interface for having an image source.
/// </summary>
internal interface IHasImageSource
{
    /// <summary>
    /// Gets the image source.
    /// </summary>
    public ImageSource? ImageSource { get; }
}