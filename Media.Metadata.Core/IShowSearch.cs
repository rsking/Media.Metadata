// -----------------------------------------------------------------------
// <copyright file="IShowSearch.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The movie search.
/// </summary>
public interface IShowSearch
{
    /// <summary>
    /// Searches for a show.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="country">The country name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The shows if found; otherwise <see langword="false"/>.</returns>
    IAsyncEnumerable<Series> SearchAsync(string name, string country = "AU", CancellationToken cancellationToken = default);
}