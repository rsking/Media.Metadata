// -----------------------------------------------------------------------
// <copyright file="IRenamer.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Renames the video.
/// </summary>
public interface IRenamer
{
    /// <summary>
    /// Renames the specified video.
    /// </summary>
    /// <param name="current">The current file name.</param>
    /// <param name="video">The video.</param>
    /// <returns>Gets the file name.</returns>
    public string? GetFileName(string current, Video video);
}