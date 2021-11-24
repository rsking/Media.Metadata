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
    /// <param name="languages">The languages.</param>
    void UpdateMovie(string fileName, Movie movie, IDictionary<int, string>? languages = default);

    /// <summary>
    /// Updates the episode.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="episode">The episode.</param>
    /// <param name="languages">The languages.</param>
    void UpdateEpisode(string fileName, Episode episode, IDictionary<int, string>? languages = default);
}