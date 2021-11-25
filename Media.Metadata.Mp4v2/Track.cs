﻿// -----------------------------------------------------------------------
// <copyright file="Track.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Represents a media track.
/// </summary>
internal class Track
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Track"/> class.
    /// </summary>
    /// <param name="id">The track ID.</param>
    /// <param name="language">The track language.</param>
    public Track(int id, string? language)
    {
        this.Id = id;
        this.Language = language;
    }

    /// <summary>
    /// Gets the ID.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets or sets the language.
    /// </summary>
    public string? Language { get; set; }
}