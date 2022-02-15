// -----------------------------------------------------------------------
// <copyright file="NativeMethods.Itmf.Tags.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System;
using System.Runtime.InteropServices;

/// <content>
/// Methods from <c>itmf_tags.h</c>.
/// </content>
internal static partial class NativeMethods
{
    /// <summary>
    /// Represents the type of image used for artwork.
    /// </summary>
    /// <remarks>
    /// These values are taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef enum MP4TagArtworkType_e
    /// {
    ///      MP4_ART_UNDEFINED = 0,
    ///      MP4_ART_BMP       = 1,
    ///      MP4_ART_GIF       = 2,
    ///      MP4_ART_JPEG      = 3,
    ///      MP4_ART_PNG       = 4
    ///  } MP4TagArtworkType;
    /// </code>
    /// </para>
    /// </remarks>
    public enum MP4TagArtworkType
    {
        /// <summary>
        /// Undefined image type.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// A bitmap image.
        /// </summary>
        Bmp = 1,

        /// <summary>
        /// A GIF image.
        /// </summary>
        Gif = 2,

        /// <summary>
        /// A JPEG image.
        /// </summary>
        Jpeg = 3,

        /// <summary>
        /// A PNG image.
        /// </summary>
        Png = 4,
    }

    /// <summary>
    /// Allocate tags convenience structure for reading and settings tags.
    /// </summary>
    /// <remarks>This function allocates a new structure which represents a snapshot of all the tags therein, tracking if the tag is missing, or present and with value.It is the caller's responsibility to free the structure with <see cref="MP4TagsFree"/>.</remarks>
    /// <returns>Structure with all tags missing.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr MP4TagsAlloc();

    /// <summary>
    /// Fetch data from mp4 file and populate structure.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="file">handle of file to fetch data from.</param>
    /// <remarks>The tags structure and its hidden data-cache is updated to reflect the actual tags values found in <paramref name="file"/>.</remarks>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsFetch(IntPtr tags, IntPtr file);

    /// <summary>
    /// Store data to mp4 file from structure.
    /// </summary>
    /// <param name="tags">tags structure to store (read) from.</param>
    /// <param name="file">handle of file to store data to.</param>
    /// <remarks>The tags structure is pushed out to the mp4 file, adding tags if needed, removing tags if needed, and updating the values to modified tags.</remarks>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsStore(IntPtr tags, IntPtr file);

    /// <summary>
    /// Free tags convenience structure.
    /// </summary>
    /// <param name="tags">tags structure to destroy.</param>
    /// <remarks>This function frees memory associated with the structure.</remarks>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MP4TagsFree(IntPtr tags);

    /// <summary>
    /// Sets the name tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="name">name to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetName(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? name);

    /// <summary>
    /// Sets the artist tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="artist">artist to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? artist);

    /// <summary>
    /// Sets the album artist tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="albumArtist">album artist to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetAlbumArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? albumArtist);

    /// <summary>
    /// Sets the album tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="album">album to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetAlbum(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? album);

    /// <summary>
    /// Sets the grouping tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="grouping">grouping to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetGrouping(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? grouping);

    /// <summary>
    /// Sets the composer tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="composer">composer to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetComposer(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? composer);

    /// <summary>
    /// Sets the comments tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="comments">comments to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetComments(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? comments);

    /// <summary>
    /// Sets the genre tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="genre">genre to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetGenre(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? genre);

    /// <summary>
    /// Sets the genre type tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="genreType">gente type to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetGenreType(IntPtr tags, IntPtr genreType);

    /// <summary>
    /// Sets the release date tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="releaseDate">release date to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetReleaseDate(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? releaseDate);

    /// <summary>
    /// Sets the track tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="trackInfo">track info to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetTrack(IntPtr tags, IntPtr trackInfo);

    /// <summary>
    /// Sets the disk tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="discInfo">disc info to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetDisk(IntPtr tags, IntPtr discInfo);

    /// <summary>
    /// Sets the tempo tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="tempo">tempo to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetTempo(IntPtr tags, IntPtr tempo);

    /// <summary>
    /// Sets the compilation tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="isCompilation">whether this is a compilation.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetCompilation(IntPtr tags, IntPtr isCompilation);

    /// <summary>
    /// Sets the TV show tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="tvShow">TV show to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetTVShow(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? tvShow);

    /// <summary>
    /// Sets the TV network tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="tvNetwork">TV network to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetTVNetwork(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? tvNetwork);

    /// <summary>
    /// Sets the TV episode ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="tvEpisodeId">TV episode ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetTVEpisodeID(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? tvEpisodeId);

    /// <summary>
    /// Sets the TV season tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="seasonNumber">season number to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetTVSeason(IntPtr tags, IntPtr seasonNumber);

    /// <summary>
    /// Sets the TV episode tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="episodeNumber">episode number to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetTVEpisode(IntPtr tags, IntPtr episodeNumber);

    /// <summary>
    /// Sets the description tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="description">desciption to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetDescription(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? description);

    /// <summary>
    /// Sets the long description tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="longDescription">long description to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetLongDescription(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? longDescription);

    /// <summary>
    /// Sets the comments tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="lyrics">lyrics to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetLyrics(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? lyrics);

    /// <summary>
    /// Sets the sort name tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortName">sort name to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetSortName(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortName);

    /// <summary>
    /// Sets the sort artist tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortArtist">sort artist to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetSortArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortArtist);

    /// <summary>
    /// Sets the sort album artist tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortAlbumArtist">sort album artist to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetSortAlbumArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortAlbumArtist);

    /// <summary>
    /// Sets the sort album tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortAlbum">sort album to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetSortAlbum(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortAlbum);

    /// <summary>
    /// Sets the sort composer tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortComposer">sort composer to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetSortComposer(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortComposer);

    /// <summary>
    /// Sets the sort TV show tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortTVShow">sort TV show to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetSortTVShow(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortTVShow);

    /// <summary>
    /// Adds artwork.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="artwork">artwork to add.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsAddArtwork(IntPtr tags, IntPtr artwork);

    /// <summary>
    /// Sets the artwork tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="index">index to set arwork at.</param>
    /// <param name="artwork">artwork to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetArtwork(IntPtr tags, int index, IntPtr artwork);

    /// <summary>
    /// Removes the artwork at the specified index.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="index">index to remove at.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsRemoveArtwork(IntPtr tags, int index);

    /// <summary>
    /// Sets the copyright tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="copyright">copyright to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetCopyright(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? copyright);

    /// <summary>
    /// Sets the encoding tool tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="encodingTool">encoding tool to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetEncodingTool(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? encodingTool);

    /// <summary>
    /// Sets the encoded by tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="encodedBy">encoded by to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetEncodedBy(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? encodedBy);

    /// <summary>
    /// Sets the purchased date tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="purchaseDate">purchase date to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetPurchaseDate(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? purchaseDate);

    /// <summary>
    /// Sets the podcast tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="isPodcast">whether this is a podcast.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetPodcast(IntPtr tags, IntPtr isPodcast);

    /// <summary>
    /// Sets the keywords tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="podcastKeywords">podcast keywords to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetKeywords(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? podcastKeywords);

    /// <summary>
    /// Sets the category tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="podcastCategory">podcast category to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetCategory(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? podcastCategory);

    /// <summary>
    /// Sets the HD video tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="isHDVideo">whether this is a HD video.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetHDVideo(IntPtr tags, IntPtr isHDVideo);

    /// <summary>
    /// Sets the mediat type tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="mediaType">media type to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetMediaType(IntPtr tags, IntPtr mediaType);

    /// <summary>
    /// Sets the content rating tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="contentRating">content rating to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetContentRating(IntPtr tags, IntPtr contentRating);

    /// <summary>
    /// Sets the gapless tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="isGapless">is gapless to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetGapless(IntPtr tags, IntPtr isGapless);

    /// <summary>
    /// Sets the iTunes account tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="iTunesAccount">iTunes account to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetITunesAccount(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? iTunesAccount);

    /// <summary>
    /// Sets the iTunes account type tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="iTunesAccountType">iTunes account type to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetITunesAccountType(IntPtr tags, IntPtr iTunesAccountType);

    /// <summary>
    /// Sets the iTunes country tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="iTunesAccountCountry">iTunes account country to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetITunesCountry(IntPtr tags, IntPtr iTunesAccountCountry);

    /// <summary>
    /// Sets the content ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="contentId">content ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetContentID(IntPtr tags, IntPtr contentId);

    /// <summary>
    /// Sets the artist ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="artistId">artist ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetArtistID(IntPtr tags, IntPtr artistId);

    /// <summary>
    /// Sets the playlist ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="playlistId">playlist ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetPlaylistID(IntPtr tags, IntPtr playlistId);

    /// <summary>
    /// Sets the genre ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="genreId">genre ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetGenreID(IntPtr tags, IntPtr genreId);

    /// <summary>
    /// Sets the composer ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="composerId">composer ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetComposerID(IntPtr tags, IntPtr composerId);

    /// <summary>
    /// Sets the XID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="xid">XID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4TagsSetXID(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? xid);

    /// <summary>
    /// Data object representing a single piece of artwork.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef struct MP4TagArtwork_s
    /// {   void*             data; /** raw picture data */
    ///     uint32_t          size; /** data size in bytes */
    ///     MP4TagArtworkType type; /** data type */
    /// } MP4TagArtwork;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MP4TagArtwork
    {
        /// <summary>
        /// raw picture data.
        /// </summary>
        public IntPtr data;

        /// <summary>
        /// data size in bytes.
        /// </summary>
        public int size;

        /// <summary>
        /// data type.
        /// </summary>
        public MP4TagArtworkType type;
    }

    /// <summary>
    /// Represents information about the tracks for this file.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef struct MP4TagTrack_s
    /// {
    ///     uint16_t index;
    ///     uint16_t total;
    /// } MP4TagTrack;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MP4TagTrack
    {
        /// <summary>
        /// Track number.
        /// </summary>
        public short index;

        /// <summary>
        /// Total number of tracks.
        /// </summary>
        public short total;
    }

    /// <summary>
    /// Represents information about the discs for this file.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef struct MP4TagDisk_s
    /// {
    ///     uint16_t index;
    ///     uint16_t total;
    /// } MP4TagDisk;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MP4TagDisk
    {
        /// <summary>
        /// Disc number.
        /// </summary>
        public short index;

        /// <summary>
        /// Total number of discs.
        /// </summary>
        public short total;
    }

    /// <summary>
    /// The main structure containing all of the tags for the file.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef struct MP4Tags_s
    /// {
    ///     void* __handle; /* internal use only */
    ///
    ///     const char*        name;
    ///     const char*        artist;
    ///     const char*        albumArtist;
    ///     const char*        album;
    ///     const char*        grouping;
    ///     const char*        composer;
    ///     const char*        comments;
    ///     const char*        genre;
    ///     const uint16_t*    genreType;
    ///     const char*        releaseDate;
    ///     const MP4TagTrack* track;
    ///     const MP4TagDisk*  disk;
    ///     const uint16_t*    tempo;
    ///     const uint8_t*     compilation;
    ///
    ///     const char*     tvShow;
    ///     const char*     tvNetwork;
    ///     const char*     tvEpisodeID;
    ///     const uint32_t* tvSeason;
    ///     const uint32_t* tvEpisode;
    ///
    ///     const char* description;
    ///     const char* longDescription;
    ///     const char* lyrics;
    ///
    ///     const char* sortName;
    ///     const char* sortArtist;
    ///     const char* sortAlbumArtist;
    ///     const char* sortAlbum;
    ///     const char* sortComposer;
    ///     const char* sortTVShow;
    ///
    ///     const MP4TagArtwork* artwork;
    ///     uint32_t             artworkCount;
    ///
    ///     const char* copyright;
    ///     const char* encodingTool;
    ///     const char* encodedBy;
    ///     const char* purchaseDate;
    ///
    ///     const uint8_t* podcast;
    ///     const char*    keywords;
    ///     const char*    category;
    ///
    ///     const uint8_t* hdVideo;
    ///     const uint8_t* mediaType;
    ///     const uint8_t* contentRating;
    ///     const uint8_t* gapless;
    ///
    ///     const char*     iTunesAccount;
    ///     const uint8_t*  iTunesAccountType;
    ///     const uint32_t* iTunesCountry;
    ///     const uint32_t* contentID;
    ///     const uint32_t* artistID;
    ///     const uint64_t* playlistID;
    ///     const uint32_t* genreID;
    ///     const uint32_t* composerID;
    ///     const char*     xid;
    /// } MP4Tags;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MP4Tags
    {
        /// <summary>
        /// Internal handle.
        /// </summary>
        public IntPtr handle;

        /// <summary>
        /// Name of the file.
        /// </summary>
        public string? name;

        /// <summary>
        /// Artist for the file.
        /// </summary>
        public string? artist;

        /// <summary>
        /// Album artist for the file.
        /// </summary>
        public string? albumArtist;

        /// <summary>
        /// Album for the file.
        /// </summary>
        public string? album;

        /// <summary>
        /// Grouping for the file.
        /// </summary>
        public string? grouping;

        /// <summary>
        /// Composer for the file.
        /// </summary>
        public string? composer;

        /// <summary>
        /// Comment for the file.
        /// </summary>
        public string? comment;

        /// <summary>
        /// Genre for the file.
        /// </summary>
        public string? genre;

        /// <summary>
        /// Pointer to the genre type for the file.
        /// </summary>
        public IntPtr genreType;

        /// <summary>
        /// Release data for the file.
        /// </summary>
        public string? releaseDate;

        /// <summary>
        /// Pointer to the track information about the file.
        /// </summary>
        public IntPtr track;

        /// <summary>
        /// Pointer to the disc information about the file.
        /// </summary>
        public IntPtr disk;

        /// <summary>
        /// Pointer to the tempo.
        /// </summary>
        public IntPtr tempo;

        /// <summary>
        /// Pointer to the "isCompilation" value.
        /// </summary>
        public IntPtr compilation;

        /// <summary>
        /// Pointer to the TV show name.
        /// </summary>
        public string? tvShow;

        /// <summary>
        /// Pointer to the TV network.
        /// </summary>
        public string? tvNetwork;

        /// <summary>
        /// Pointer to the TV episode ID.
        /// </summary>
        public string? tvEpisodeID;

        /// <summary>
        /// Pointer to the season number.
        /// </summary>
        public IntPtr tvSeason;

        /// <summary>
        /// Pointer to the episode number.
        /// </summary>
        public IntPtr tvEpisode;

        /// <summary>
        /// Description of the file.
        /// </summary>
        public string? description;

        /// <summary>
        /// Long description of the file.
        /// </summary>
        public string? longDescription;

        /// <summary>
        /// Lyrics of the file.
        /// </summary>
        public string? lyrics;

        /// <summary>
        /// Sort name of the file.
        /// </summary>
        public string? sortName;

        /// <summary>
        /// Sort artist of the file.
        /// </summary>
        public string? sortArtist;

        /// <summary>
        /// Sort album artist of the file.
        /// </summary>
        public string? sortAlbumArtist;

        /// <summary>
        /// Sort album of the file.
        /// </summary>
        public string? sortAlbum;

        /// <summary>
        /// Sort composer of the file.
        /// </summary>
        public string? sortComposer;

        /// <summary>
        /// Sort TV show of the file.
        /// </summary>
        public string? sortTVShow;

        /// <summary>
        /// Pointer to the artwork in the file.
        /// </summary>
        public IntPtr artwork;

        /// <summary>
        /// The artwork count in the file.
        /// </summary>
        public int artworkCount;

        /// <summary>
        /// Copyright in the file.
        /// </summary>
        public string? copyright;

        /// <summary>
        /// Encoding tool used for the file.
        /// </summary>
        public string? encodingTool;

        /// <summary>
        /// Encoded by information for the file.
        /// </summary>
        public string? encodedBy;

        /// <summary>
        /// Purchase date for the file.
        /// </summary>
        public string? purchasedDate;

        /// <summary>
        /// Pointer to the "isPodcast" value for the file.
        /// </summary>
        public IntPtr podcast;

        /// <summary>
        /// Podcast keywords for the file.
        /// </summary>
        public string? keywords;

        /// <summary>
        /// Podcast category for the file.
        /// </summary>
        public string? category;

        /// <summary>
        /// Pointer to the "isHDVideo" value for the file.
        /// </summary>
        public IntPtr hdVideo;

        /// <summary>
        /// Pointer to the media type for the file.
        /// </summary>
        public IntPtr mediaType;

        /// <summary>
        /// Pointer to the content rating for the file.
        /// </summary>
        public IntPtr contentRating;

        /// <summary>
        /// Pointer to the "isGapless" value for the file.
        /// </summary>
        public IntPtr gapless;

        /// <summary>
        /// iTunes account used to purchase the file.
        /// </summary>
        public string? itunesAccount;

        /// <summary>
        /// Pointer to the type of iTunes account used to purchase the file.
        /// </summary>
        public IntPtr iTunesAccountType;

        /// <summary>
        /// Pointer to the country for the iTunes account used to purchase the file.
        /// </summary>
        public IntPtr iTunesCountry;

        /// <summary>
        /// Pointer to the content ID of the file.
        /// </summary>
        public IntPtr contentID;

        /// <summary>
        /// Pointer to the artist ID of the file.
        /// </summary>
        public IntPtr artistID;

        /// <summary>
        /// Pointer to the playlist ID of the file.
        /// </summary>
        public IntPtr playlistID;

        /// <summary>
        /// Pointer to the genre ID of the file.
        /// </summary>
        public IntPtr genreID;

        /// <summary>
        /// Pointer to the composer ID of the file.
        /// </summary>
        public IntPtr composerID;

        /// <summary>
        /// Auxiliary ID of the file.
        /// </summary>
        public string? xid;
    }
}