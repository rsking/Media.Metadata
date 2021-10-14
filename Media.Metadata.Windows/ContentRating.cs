// -----------------------------------------------------------------------
// <copyright file="ContentRating.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Windows;

/// <summary>
/// Specifies the value for the content rating of an MP4 file.
/// </summary>
internal enum ContentRating : byte
{
    /// <summary>
    /// Indicates the value has been set, but there is no rating for the content of this file.
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates a value of "clean" has been set for the content of this file.
    /// </summary>
    Clean = 2,

    /// <summary>
    /// Indicates a value of "explicit" has been set for the content of this file.
    /// </summary>
    Explicit = 4,

    /// <summary>
    /// Indicates that the value is not set in the file.
    /// </summary>
    NotSet = byte.MaxValue,
}