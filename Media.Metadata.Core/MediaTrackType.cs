// -----------------------------------------------------------------------
// <copyright file="MediaTrackType.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The media track type.
/// </summary>
public enum MediaTrackType
{
    /// <summary>
    /// All tracks.
    /// </summary>
    All = int.MinValue,

    /// <summary>
    /// Text.
    /// </summary>
    Text = -4,

    /// <summary>
    /// Audio.
    /// </summary>
    Audio = -3,

    /// <summary>
    /// Video.
    /// </summary>
    Video = -2,

    /// <summary>
    /// Unknown.
    /// </summary>
    Unknown = -1,
}