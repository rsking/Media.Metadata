// -----------------------------------------------------------------------
// <copyright file="Country.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Country.
/// </summary>
public record class Country : IEquatable<Country>
{
    /// <summary>
    /// Australia.
    /// </summary>
    public static readonly Country Australia = new("AU");

    /// <summary>
    /// Canada.
    /// </summary>
    public static readonly Country Canada = new("CA");

    /// <summary>
    /// France.
    /// </summary>
    public static readonly Country France = new("FR");

    /// <summary>
    /// Germany.
    /// </summary>
    public static readonly Country Germany = new("DE");

    /// <summary>
    /// Greate Britan.
    /// </summary>
    public static readonly Country GreatBritan = new("GB");

    /// <summary>
    /// New Zealand.
    /// </summary>
    public static readonly Country NewZealand = new("NZ");

    /// <summary>
    /// United States.
    /// </summary>
    public static readonly Country UnitedStates = new("US");

    private Country(string abbreviation)
    {
        this.Name = Properties.Resources.ResourceManager.GetString(abbreviation, Properties.Resources.Culture);
        this.Abbreviation = abbreviation;
    }

    /// <summary>
    /// Gets all the countries.
    /// </summary>
    public static IEnumerable<Country> All
    {
        get
        {
            yield return Australia;
            yield return Canada;
            yield return France;
            yield return Germany;
            yield return GreatBritan;
            yield return NewZealand;
            yield return UnitedStates;
        }
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    public string Abbreviation { get; }

    /// <summary>
    /// Converts a string value into an abbrevation.
    /// </summary>
    /// <param name="abbreviation">The abbreviation.</param>
    public static implicit operator Country(string abbreviation) => All.FirstOrDefault(country => string.Equals(country.Abbreviation, abbreviation, StringComparison.Ordinal));

    /// <inheritdoc/>
    public override string ToString() => this.Name;
}