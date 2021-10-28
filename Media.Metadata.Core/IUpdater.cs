// -----------------------------------------------------------------------
// <copyright file="IUpdater.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The updater.
/// </summary>
public interface IUpdater
{
    /// <summary>
    /// Updates the movie.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="movie">The movie.</param>
    void UpdateMovie(string fileName, Movie movie);

    /// <summary>
    /// Updates the episode.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="episode">The episode.</param>
    void UpdateEpisode(string fileName, Episode episode);
}