// -----------------------------------------------------------------------
// <copyright file="SingleValueToleranceMap.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Single value tolerance map.
/// </summary>
internal class SingleValueToleranceMap : Snapshot
{
    private System.Drawing.Color toleraceColor;

    /// <summary>
    /// Initialises a new instance of the <see cref="SingleValueToleranceMap"/> class.
    /// </summary>
    /// <param name="color">The single color.</param>
    internal SingleValueToleranceMap(System.Drawing.Color color) => this.toleraceColor = color;

    /// <inheritdoc/>
    internal override System.Drawing.Color this[int row, int column]
    {
        get => this.toleraceColor;

        set => throw new NotSupportedException();
    }
}