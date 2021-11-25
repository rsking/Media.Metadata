// -----------------------------------------------------------------------
// <copyright file="IReader.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Reads the movie.
/// </summary>
public interface IReader
{
    /// <summary>
    /// Reads the video.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The video.</returns>
    Video ReadVideo(string path);

    /// <summary>
    /// Reads the movie.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The movie.</returns>
    Movie ReadMovie(string path);

    /// <summary>
    /// Reads the episode.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The episode.</returns>
    Episode ReadEpisode(string path);
}