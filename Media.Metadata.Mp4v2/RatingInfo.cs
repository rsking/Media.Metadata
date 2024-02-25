// -----------------------------------------------------------------------
// <copyright file="RatingInfo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The <see cref="RatingInfo"/> class is represents all of the information contained
/// in the "iTunEXTC" atom. This information includes information about the parental
/// advisory ratings of the content.
/// </summary>
internal sealed class RatingInfo : Atom, IEquatable<RatingInfo>
{
    /// <summary>
    /// Gets or sets the string that represents the rating (e.g., 'PG').
    /// </summary>
    public string Rating { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the string that represents the source for the rating (e.g., <c>mpaa</c>).
    /// </summary>
    public string RatingSource { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sort value for the rating.
    /// </summary>
    public int SortValue { get; set; }

    /// <summary>
    /// Gets or sets the rating annotation.
    /// </summary>
    public string? RatingAnnotation { get; set; }

    /// <inheritdoc/>
    public override string Meaning => "com.apple.iTunes";

    /// <inheritdoc/>
    public override string Name => "iTunEXTC";

    /// <inheritdoc/>
    public override string ToString() => FormattableString.Invariant($"{this.RatingSource}|{this.Rating}|{this.SortValue}|{this.RatingAnnotation ?? string.Empty}");

    /// <inheritdoc/>
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(this.ToString());

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is RatingInfo other && this.Equals(other);

    /// <inheritdoc/>
    public bool Equals(RatingInfo other) => string.Equals(this.ToString(), other.ToString(), StringComparison.Ordinal);

    /// <inheritdoc/>
    public override void Populate(byte[] dataBuffer)
    {
        var ratingString = System.Text.Encoding.UTF8.GetString(dataBuffer);
        var parts = ratingString.Split('|');
        this.RatingSource = parts[0];
        this.Rating = parts[1];
        this.SortValue = int.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture);
        if (parts.Length > 3)
        {
            this.RatingAnnotation = parts[3];
        }
    }

    /// <inheritdoc/>
    public override byte[] ToByteArray() => System.Text.Encoding.UTF8.GetBytes(this.ToString());
}