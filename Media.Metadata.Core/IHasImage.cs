// -----------------------------------------------------------------------
// <copyright file="IHasImage.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// A resource that has an image.
/// </summary>
public interface IHasImage
{
    /// <summary>
    /// Gets the image.
    /// </summary>
    Image? Image { get; }

    /// <summary>
    /// Gets the image format.
    /// </summary>
    SixLabors.ImageSharp.Formats.IImageFormat? ImageFormat { get; }
}