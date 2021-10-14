// -----------------------------------------------------------------------
// <copyright file="RatingInfo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Windows;

/// <summary>
/// The <see cref="RatingInfo"/> class is represents all of the information contained
/// in the "iTunEXTC" atom. This information includes information about the parental
/// advisory ratings of the content.
/// </summary>
internal class RatingInfo : Atom
{
    /// <summary>
    /// Gets or sets the string that represents the rating (e.g., 'PG').
    /// </summary>
    public string Rating { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the string that represents the source for the rating (e.g., 'mpaa').
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

    /// <summary>
    /// Gets the meaning of the atom.
    /// </summary>
    public override string Meaning => "com.apple.iTunes";

    /// <summary>
    /// Gets the name of the atom.
    /// </summary>
    public override string Name => "iTunEXTC";

    /// <summary>
    /// Returns the string representation of the rating.
    /// </summary>
    /// <returns>The string representation of the rating.</returns>
    public override string ToString() => FormattableString.Invariant($"{this.RatingSource}|{this.Rating}|{this.SortValue}|{this.RatingAnnotation ?? string.Empty}");

    /// <summary>
    /// Returns the hash code for this <see cref="RatingInfo"/>.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(this.ToString());

    /// <summary>
    /// Determines whether two <see cref="RatingInfo"/> objects have the same value.
    /// </summary>
    /// <param name="obj">Determines whether this instance and a specified object, which
    /// must also be a <see cref="RatingInfo"/> object, have the same value.</param>
    /// <returns><see langword="true"/> if obj is a <see cref="RatingInfo"/> and its value
    /// is the same as this instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object obj) => obj is RatingInfo other && string.Equals(this.ToString(), other.ToString(), StringComparison.Ordinal);

    /// <summary>
    /// Populates this <see cref="RatingInfo"/> with the specific data stored in it in the referenced file.
    /// </summary>
    /// <param name="dataBuffer">A byte array containing the iTunes Metadata Format data
    /// used to populate this <see cref="RatingInfo"/>.</param>
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

    /// <summary>
    /// Returns the data to be stored in this <see cref="RatingInfo"/> as a byte array.
    /// </summary>
    /// <returns>The byte array containing the data to be stored in the atom.</returns>
    public override byte[] ToByteArray() => System.Text.Encoding.UTF8.GetBytes(this.ToString());
}