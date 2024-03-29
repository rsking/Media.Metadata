﻿// -----------------------------------------------------------------------
// <copyright file="Video.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// A video file.
/// </summary>
/// <param name="Name">Gets the name.</param>
/// <param name="Description">Gets the description.</param>
/// <param name="Producers">Gets the producers.</param>
/// <param name="Directors">Gets the directors.</param>
/// <param name="Studios">Gets the studios.</param>
/// <param name="Genre">Gets the genres.</param>
/// <param name="ScreenWriters">Gets the screen writers.</param>
/// <param name="Cast">Gets the cast.</param>
/// <param name="Composers">Gets the composers.</param>
public abstract record class Video(
    string? Name,
    string? Description,
    IEnumerable<string>? Producers,
    IEnumerable<string>? Directors,
    IEnumerable<string>? Studios,
    IEnumerable<string>? Genre,
    IEnumerable<string>? ScreenWriters,
    IEnumerable<string>? Cast,
    IEnumerable<string>? Composers) : HasImage
{
    private const string Separator = ", ";

    /// <summary>
    /// Gets the release date.
    /// </summary>
    public DateTime? Release { get; init; }

    /// <summary>
    /// Gets the rating.
    /// </summary>
    public Rating? Rating { get; init; }

    /// <summary>
    /// Gets the work.
    /// </summary>
    public string? Work { get; init; }

    /// <summary>
    /// Gets the tracks.
    /// </summary>
    public IReadOnlyList<MediaTrack> Tracks { get; init; } = [];

    /// <inheritdoc/>
    public override string ToString()
    {
        var list = new Formatters.PList.PList();
        list.AddIfNotNullOrEmpty("studio", Separator, this.Studios);
        list.AddIfNotNull("producers", this.Producers);
        list.AddIfNotNull("directors", this.Directors);
        list.AddIfNotNull("cast", this.Cast);
        list.AddIfNotNull("screenwriters", this.ScreenWriters);
        return list.Serialize();
    }
}