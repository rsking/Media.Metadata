// -----------------------------------------------------------------------
// <copyright file="MediaType.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Indicates the kind of media contained in this file.
/// </summary>
public enum MediaType : byte
{
    /// <summary>
    /// Indicates the media type is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Indicates the media type is a music file.
    /// </summary>
    Music = 1,

    /// <summary>
    /// Indicates the media type is an audiobook.
    /// </summary>
    Audiobook = 2,

    /// <summary>
    /// Indicates the media type is a music video.
    /// </summary>
    MusicVideo = 6,

    /// <summary>
    /// Indicates the media type is a movie.
    /// </summary>
    Movie = 9,

    /// <summary>
    /// Indicates the media type is an episode of a TV show.
    /// </summary>
    TVShow = 10,

    /// <summary>
    /// Indicates the media type is a digital booklet.
    /// </summary>
    Booklet = 11,

    /// <summary>
    /// Indicates the media type is a ringtone.
    /// </summary>
    Ringtone = 14,

    /// <summary>
    /// Indicates the media type is an episode of a podcast.
    /// </summary>
    Podcast = 21,

    /// <summary>
    /// Indicates the media type is an iTunesU file.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "iTunes is a proper name and is spelled correctly.")]
    iTunesU = 23,

    /// <summary>
    /// Indicates the media type is not set in this file.
    /// </summary>
    NotSet = byte.MaxValue,
}