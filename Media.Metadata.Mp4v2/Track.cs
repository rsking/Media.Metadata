// -----------------------------------------------------------------------
// <copyright file="Track.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Represents a media track.
/// </summary>
/// <remarks>
/// Initialises a new instance of the <see cref="Track"/> class.
/// </remarks>
/// <param name="Id">Gets the ID.</param>
/// <param name="Type">Gets the type.</param>
internal record class Track(int Id, string? Type)
{
    /// <summary>
    /// Gets or sets the language.
    /// </summary>
    public string? Language { get; set; }
}