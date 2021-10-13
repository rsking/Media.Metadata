// -----------------------------------------------------------------------
// <copyright file="Movie.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a movie.
/// </summary>
public record Movie(
    string Name,
    string Description,
    IEnumerable<string> Producers,
    IEnumerable<string> Directors,
    IEnumerable<string> Studios,
    IEnumerable<string> Genre,
    IEnumerable<string> ScreenWriters,
    IEnumerable<string> Cast,
    IEnumerable<string> Composers)
{
    private const string Separator = ", ";

    /// <summary>
    /// Gets the release date.
    /// </summary>
    public DateTime? Release { get; init; }

    /// <summary>
    /// Gets the image URI.
    /// </summary>
    public Uri? ImageUri { get; init; }

    /// <summary>
    /// Gets the rating.
    /// </summary>
    public Rating? Rating { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        var plist = new PList(new Dictionary<string, object>(StringComparer.Ordinal));
        plist.AddIfNotNullOrEmpty("studio", Separator, this.Studios);
        plist.AddAsArray("producers", this.Producers);
        plist.AddAsArray("directors", this.Directors);
        plist.AddAsArray("cast", this.Cast);
        plist.AddAsArray("screenwriters", this.ScreenWriters);
        return plist.ToString();
    }
}