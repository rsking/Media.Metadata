// -----------------------------------------------------------------------
// <copyright file="ColorDifference.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

public class ColorDifference : IEquatable<ColorDifference>
{
    /// <summary>
    /// Gets or sets the alpha.
    /// </summary>
    public byte Alpha { get; set; }

    /// <summary>
    /// Gets or sets the red.
    /// </summary>
    public byte Red { get; set; }

    /// <summary>
    /// Gets or sets the green.
    /// </summary>
    public byte Green { get; set; }

    /// <summary>
    /// Gets or sets the blue.
    /// </summary>
    public byte Blue { get; set; }

    /// <summary>
    /// Initialises a new instance of the <see cref="ColorDifference"/> class.
    /// </summary>
    public ColorDifference()
    {
        this.Alpha = byte.MaxValue;
        this.Red = 0;
        this.Green = 0;
        this.Blue = 0;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="ColorDifference"/> class.
    /// </summary>
    /// <param name="rgbTolerance">The tolerance.</param>
    public ColorDifference(byte rgbTolerance)
    {
        this.Alpha = byte.MaxValue;
        this.Red = rgbTolerance;
        this.Green = rgbTolerance;
        this.Blue = rgbTolerance;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="ColorDifference"/> class.
    /// </summary>
    /// <param name="red">The red tolerance.</param>
    /// <param name="green">The green tolerance.</param>
    /// <param name="blue">The blue tolerance.</param>
    public ColorDifference(byte red, byte green, byte blue)
    {
        this.Alpha = byte.MaxValue;
        this.Red = red;
        this.Green = green;
        this.Blue = blue;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="ColorDifference"/> class.
    /// </summary>
    /// <param name="alpha">The alpah tolerance.</param>
    /// <param name="red">The red tolerance.</param>
    /// <param name="green">The green tolerance.</param>
    /// <param name="blue">The blue tolerance.</param>
    public ColorDifference(byte alpha, byte red, byte green, byte blue)
    {
        this.Alpha = alpha;
        this.Red = red;
        this.Green = green;
        this.Blue = blue;
    }

    /// <summary>
    /// Implements not-equals.
    /// </summary>
    /// <param name="left">The lhs.</param>
    /// <param name="right">The rhs.</param>
    /// <returns>The result.</returns>
    public static bool operator !=(ColorDifference left, ColorDifference right) => !(left == right);

    /// <summary>
    /// Implements equals.
    /// </summary>
    /// <param name="left">The lhs.</param>
    /// <param name="right">The rhs.</param>
    /// <returns>The result.</returns>
    public static bool operator ==(ColorDifference left, ColorDifference right) =>
        ReferenceEquals(left, right)
            || (left is not null && right is not null && left.Alpha == right.Alpha && left.Red == right.Red && left.Green == right.Green && left.Blue == right.Blue);

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is ColorDifference colorDifference && this.Equals(colorDifference);

    /// <inheritdoc/>
    public bool Equals(ColorDifference other) => this == other;

    /// <inheritdoc/>
    public override string ToString() => string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ColorDifferenceToString, this.Alpha, this.Red, this.Green, this.Blue);

    /// <inheritdoc/>
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(this.ToString());

    /// <summary>
    /// Tests whether this instance meets tolerance.
    /// </summary>
    /// <param name="tolerance">The tolerance.</param>
    /// <returns>The results.</returns>
    internal bool MeetsTolerance(ColorDifference tolerance) => this.Alpha <= tolerance.Alpha && this.Red <= tolerance.Red && this.Green <= tolerance.Green && this.Blue <= tolerance.Blue;

    /// <summary>
    /// Calculates the margin.
    /// </summary>
    /// <param name="tolerance">The tolerance.</param>
    /// <returns>The margin.</returns>
    internal System.Drawing.Color CalculateMargin(ColorDifference tolerance) => System.Drawing.Color.FromArgb(Math.Abs(this.Alpha - tolerance.Alpha), Math.Abs(this.Red - tolerance.Red), Math.Abs(this.Green - tolerance.Green), Math.Abs(this.Blue - tolerance.Blue));
}