// -----------------------------------------------------------------------
// <copyright file="IMovieSearch.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The movie search.
/// </summary>
public interface IMovieSearch
{
    /// <summary>
    /// Searches for a movie.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="year">The year.</param>
    /// <param name="country">The country.</param>
    /// <returns>The movies if found; otherwise <see langword="false"/>.</returns>
    IAsyncEnumerable<Movie> SearchAsync(string name, int year = 0, string country = "AU");
}