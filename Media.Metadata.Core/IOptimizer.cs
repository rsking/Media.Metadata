// -----------------------------------------------------------------------
// <copyright file="IOptimizer.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The optimizer.
/// </summary>
public interface IOptimizer
{
    /// <summary>
    /// Optimizes the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>Whether the file was optimized or not.</returns>
    public bool Optimize(string path);
}