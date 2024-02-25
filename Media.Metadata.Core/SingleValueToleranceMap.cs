// -----------------------------------------------------------------------
// <copyright file="SingleValueToleranceMap.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Single value tolerance map.
/// </summary>
internal sealed class SingleValueToleranceMap : Snapshot
{
    private readonly Color toleranceColor;

    /// <summary>
    /// Initialises a new instance of the <see cref="SingleValueToleranceMap"/> class.
    /// </summary>
    /// <param name="color">The single color.</param>
    internal SingleValueToleranceMap(Color color) => this.toleranceColor = color;

    /// <inheritdoc/>
    internal override Color this[int row, int column]
    {
        get => this.toleranceColor;
        set => throw new NotSupportedException();
    }
}