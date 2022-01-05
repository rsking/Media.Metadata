// -----------------------------------------------------------------------
// <copyright file="Track.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Initialises a new instance of the <see cref="MediaTrack"/> class.
/// </summary>
/// <param name="Id">The track ID.</param>
/// <param name="Type">The track type.</param>
/// <param name="Language">The track language.</param>
public record class MediaTrack(int Id, MediaTrackType Type, string? Language);