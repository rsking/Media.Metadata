// -----------------------------------------------------------------------
// <copyright file="IHasSoftwareBitmap.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

/// <summary>
/// Interface for have a software bitmap.
/// </summary>
internal interface IHasSoftwareBitmap
{
    /// <summary>
    /// Gets the softare bitmap.
    /// </summary>
    Windows.Graphics.Imaging.SoftwareBitmap? SoftwareBitmap { get; }
}