// -----------------------------------------------------------------------
// <copyright file="ToleranceRectangle.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The tolerance rectangle.
/// </summary>
public struct ToleranceRectangle
{
    /// <summary>
    /// Gets or sets the rectangle.
    /// </summary>
    public System.Drawing.Rectangle Rectangle { get; set; }

    /// <summary>
    /// Gets or sets the difference.
    /// </summary>
    public ColorDifference Difference { get; set; }

    /// <summary>
    /// Implements not-equals.
    /// </summary>
    /// <param name="left">The lhs.</param>
    /// <param name="right">The rhs.</param>
    /// <returns>The result.</returns>
    public static bool operator !=(ToleranceRectangle left, ToleranceRectangle right) => !(left == right);

    /// <summary>
    /// Implements equals.
    /// </summary>
    /// <param name="left">The lhs.</param>
    /// <param name="right">The rhs.</param>
    /// <returns>The result.</returns>
    public static bool operator ==(ToleranceRectangle left, ToleranceRectangle right) => left.Rectangle == right.Rectangle && left.Difference == right.Difference;

    /// <inheritdoc/>
    public override readonly bool Equals(object obj) => obj is ToleranceRectangle toleranceRectangle && this == toleranceRectangle;

    /// <inheritdoc/>
    public override readonly string ToString() => string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}{1}", this.Rectangle, this.Difference);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => StringComparer.Ordinal.GetHashCode(this.ToString());
}