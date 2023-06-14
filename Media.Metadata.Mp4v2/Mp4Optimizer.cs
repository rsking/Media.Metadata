// -----------------------------------------------------------------------
// <copyright file="Mp4Optimizer.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The MP4v2 optimizer.
/// </summary>
public class Mp4Optimizer : IOptimizer
{
    /// <inheritdoc/>
    public bool Opimize(string path) => NativeMethods.MP4Optimize(System.Text.Encoding.UTF8.GetBytes(path), newName: null);
}