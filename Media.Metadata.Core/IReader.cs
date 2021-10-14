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
    /// Reads the movie.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The read movie.</returns>
    Movie ReadMovie(string path);
}