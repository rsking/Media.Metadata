﻿// -----------------------------------------------------------------------
// <copyright file="IEpisodeSearch.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The episode search.
/// </summary>
public interface IEpisodeSearch
{
    /// <summary>
    /// Searches for a movie.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="year">The year.</param>
    /// <returns>The movies if found; otherwise <see langword="false"/>.</returns>
    IAsyncEnumerable<Movie> SearchAsync(string name, int year);
}