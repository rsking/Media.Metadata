// -----------------------------------------------------------------------
// <copyright file="LocalEpisode.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Represents a local episode.
/// </summary>
/// <inheritdoc />
public record class LocalEpisode(
    FileInfo FileInfo,
    string? Name,
    string? Description,
    IEnumerable<string>? Producers,
    IEnumerable<string>? Directors,
    IEnumerable<string>? Studios,
    IEnumerable<string>? Genre,
    IEnumerable<string>? ScreenWriters,
    IEnumerable<string>? Cast,
    IEnumerable<string>? Composers) : Episode(Name, Description, Producers, Directors, Studios, Genre, ScreenWriters, Cast, Composers), ILocalVideo;