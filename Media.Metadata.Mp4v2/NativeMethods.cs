﻿// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System.Runtime.InteropServices;

/// <summary>
/// Contains methods used for interfacing with the native code MP4V2 library.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA2101:Specify marshaling for P/Invoke string arguments", Justification = "The marshalling is specified")]
internal static class NativeMethods
{
    /// <summary>
    /// Invalid track ID.
    /// </summary>
    internal const int MP4InvalidTrackId = 0;

    /// <summary>
    /// Od track type.
    /// </summary>
    internal const string MP4OdTrackType = "odsm";

    /// <summary>
    /// Scene track type.
    /// </summary>
    internal const string MP4SceneTrackType = "sdsm";

    /// <summary>
    /// Audio track type.
    /// </summary>
    internal const string MP4AudioTrackType = "soun";

    /// <summary>
    /// Video track type.
    /// </summary>
    internal const string MP4VideoTrackType = "vide";

    /// <summary>
    /// Hint track type.
    /// </summary>
    internal const string MP4HintTrackType = "hint";

    /// <summary>
    /// Control track type.
    /// </summary>
    internal const string MP4ControlTrackType = "cntl";

    /// <summary>
    /// Text track type.
    /// </summary>
    internal const string MP4TextTrackType = "text";

    /// <summary>
    /// Subtitle track type.
    /// </summary>
    internal const string MP4SubtitleTrackType = "sbtl";

    /// <summary>
    /// Sub-picture track type.
    /// </summary>
    internal const string MP4SubpictureTrackType = "subp";

    /// <summary>
    /// The should parse atom call back.
    /// </summary>
    /// <param name="atom">The atom.</param>
    /// <returns><see langword="true"/> to parse the atom.</returns>
    public delegate bool ShouldParseAtomCallback(uint atom);

    /// <summary>
    /// Represents the iTunes Metadata Format basic types.
    /// </summary>
    /// <remarks>
    /// These values are taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// Basic types of value data as enumerated in spec. */
    /// typedef enum MP4ItmfBasicType_e
    /// {
    ///     MP4_ITMF_BT_IMPLICIT  = 0,   /** for use with tags for which no type needs to be indicated */
    ///     MP4_ITMF_BT_UTF8      = 1,   /** without any count or null terminator */
    ///     MP4_ITMF_BT_UTF16     = 2,   /** also known as UTF-16BE */
    ///     MP4_ITMF_BT_SJIS      = 3,   /** deprecated unless it is needed for special Japanese characters */
    ///     MP4_ITMF_BT_HTML      = 6,   /** the HTML file header specifies which HTML version */
    ///     MP4_ITMF_BT_XML       = 7,   /** the XML header must identify the DTD or schemas */
    ///     MP4_ITMF_BT_UUID      = 8,   /** also known as GUID; stored as 16 bytes in binary (valid as an ID) */
    ///     MP4_ITMF_BT_ISRC      = 9,   /** stored as UTF-8 text (valid as an ID) */
    ///     MP4_ITMF_BT_MI3P      = 10,  /** stored as UTF-8 text (valid as an ID) */
    ///     MP4_ITMF_BT_GIF       = 12,  /** (deprecated) a GIF image */
    ///     MP4_ITMF_BT_JPEG      = 13,  /** a JPEG image */
    ///     MP4_ITMF_BT_PNG       = 14,  /** a PNG image */
    ///     MP4_ITMF_BT_URL       = 15,  /** absolute, in UTF-8 characters */
    ///     MP4_ITMF_BT_DURATION  = 16,  /** in milliseconds, 32-bit integer */
    ///     MP4_ITMF_BT_DATETIME  = 17,  /** in UTC, counting seconds since midnight, January 1, 1904; 32 or 64-bits */
    ///     MP4_ITMF_BT_GENRES    = 18,  /** a list of enumerated values */
    ///     MP4_ITMF_BT_INTEGER   = 21,  /** a signed big-endian integer with length one of { 1,2,3,4,8 } bytes */
    ///     MP4_ITMF_BT_RIAA_PA   = 24,  /** RIAA parental advisory; { -1=no, 1=yes, 0=unspecified }, 8-bit ingteger */
    ///     MP4_ITMF_BT_UPC       = 25,  /** Universal Product Code, in text UTF-8 format (valid as an ID) */
    ///     MP4_ITMF_BT_BMP       = 27,  /** Windows bitmap image */
    ///     MP4_ITMF_BT_UNDEFINED = 255  /** undefined */
    /// } MP4ItmfBasicType;
    /// </code>
    /// </para>
    /// </remarks>
    internal enum MP4ItmfBasicType
    {
        /// <summary>
        /// For use with tags for which no type needs to be indicated.
        /// </summary>
        Implicit = 0,

        /// <summary>
        /// Without any count or null terminator.
        /// </summary>
        Utf8 = 1,

        /// <summary>
        /// Also known as UTF-16BE.
        /// </summary>
        Utf16 = 2,

        /// <summary>
        /// Deprecated unless it is needed for special Japanese characters.
        /// </summary>
        Sjis = 3,

        /// <summary>
        /// The HTML file header specifies which HTML version.
        /// </summary>
        Html = 6,

        /// <summary>
        /// The XML header must identify the DTD or schemas.
        /// </summary>
        Xml = 7,

        /// <summary>
        /// Also known as GUID; stored as 16 bytes in binary (valid as an ID).
        /// </summary>
        Uuid = 8,

        /// <summary>
        /// stored as UTF-8 text (valid as an ID).
        /// </summary>
        Isrc = 9,

        /// <summary>
        /// stored as UTF-8 text (valid as an ID).
        /// </summary>
        Mi3p = 10,

        /// <summary>
        /// (deprecated) a GIF image.
        /// </summary>
        Gif = 12,

        /// <summary>
        /// a JPEG image.
        /// </summary>
        Jpeg = 13,

        /// <summary>
        /// A PNG image.
        /// </summary>
        Png = 14,

        /// <summary>
        /// absolute, in UTF-8 characters.
        /// </summary>
        Url = 15,

        /// <summary>
        /// in milliseconds, 32-bit integer.
        /// </summary>
        Duration = 16,

        /// <summary>
        /// in UTC, counting seconds since midnight, January 1, 1904; 32 or 64-bits.
        /// </summary>
        DateTime = 17,

        /// <summary>
        /// a list of enumerated values.
        /// </summary>
        Genres = 18,

        /// <summary>
        /// a signed big-endian integer with length one of { 1,2,3,4,8 } bytes.
        /// </summary>
        Integer = 21,

        /// <summary>
        /// RIAA parental advisory; { -1=no, 1=yes, 0=unspecified }, 8-bit integer.
        /// </summary>
        Riaa_pa = 24,

        /// <summary>
        /// Universal Product Code, in text UTF-8 format (valid as an ID).
        /// </summary>
        Upc = 25,

        /// <summary>
        /// A Windows bitmap image.
        /// </summary>
        Bmp = 27,

        /// <summary>
        /// An undefined value.
        /// </summary>
        Undefined = 255,
    }

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
    internal enum ArtworkType
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
    /// Represents the known types used for chapters.
    /// </summary>
    /// <remarks>
    /// These values are taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef enum {
    ///     MP4ChapterTypeNone = 0,
    ///     MP4ChapterTypeAny  = 1,
    ///     MP4ChapterTypeQt   = 2,
    ///     MP4ChapterTypeNero = 4,
    /// } MP4ChapterType;
    /// </code>
    /// </para>
    /// </remarks>
    internal enum MP4ChapterType
    {
        /// <summary>
        /// No chapters found return value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Any or all known chapter types.
        /// </summary>
        Any = 1,

        /// <summary>
        /// QuickTime chapter type.
        /// </summary>
        Qt = 2,

        /// <summary>
        /// Nero chapter type.
        /// </summary>
        Nero = 4,
    }

    /// <summary>
    /// Values representing the time scale for a track.
    /// </summary>
    internal enum MP4TimeScale
    {
        /// <summary>
        /// Track duration is measured in seconds.
        /// </summary>
        Seconds = 1,

        /// <summary>
        /// Track duration is measured in milliseconds.
        /// </summary>
        Milliseconds = 1000,

        /// <summary>
        /// Track duration is measured in microseconds.
        /// </summary>
        Microseconds = 1000000,

        /// <summary>
        /// Track duration is measured in nanoseconds.
        /// </summary>
        Nanoseconds = 100000000,
    }

    /// <summary>
    /// Allocate tags convenience structure for reading and settings tags.
    /// </summary>
    /// <remarks>This function allocates a new structure which represents a snapshot of all the tags therein, tracking if the tag is missing, or present and with value.It is the caller's responsibility to free the structure with <see cref="MP4TagsFree"/>.</remarks>
    /// <returns>Structure with all tags missing.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr MP4TagsAlloc();

    /// <summary>
    /// Fetch data from mp4 file and populate structure.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="file">handle of file to fetch data from.</param>
    /// <remarks>The tags structure and its hidden data-cache is updated to reflect the actual tags values found in <paramref name="file"/>.</remarks>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsFetch(IntPtr tags, IntPtr file);

    /// <summary>
    /// Store data to mp4 file from structure.
    /// </summary>
    /// <param name="tags">tags structure to store (read) from.</param>
    /// <param name="file">handle of file to store data to.</param>
    /// <remarks>The tags structure is pushed out to the mp4 file, adding tags if needed, removing tags if needed, and updating the values to modified tags.</remarks>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsStore(IntPtr tags, IntPtr file);

    /// <summary>
    /// Free tags convenience structure.
    /// </summary>
    /// <param name="tags">tags structure to destroy.</param>
    /// <remarks>This function frees memory associated with the structure.</remarks>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void MP4TagsFree(IntPtr tags);

    /// <summary>
    /// Read an existing mp4 file.
    /// </summary>
    /// <param name="fileName">pathname of the file to be read. On Windows, this should be a UTF-8 encoded string. On other platforms, it should be an 8-bit encoding that is appropriate for the platform, locale, file system, etc. (prefer to use UTF-8 when possible).</param>
    /// <param name="cb">The call back.</param>
    /// <remarks>MP4Read is the first call that should be used when you want to just read an existing mp4 file.It is equivalent to opening a file for reading, but in addition the mp4 file is parsed and the controlinformation is loaded into memory.Note that actual track samples are notread into memory until MP4ReadSample() is called.</remarks>
    /// <returns>On success a handle of the file for use in subsequent calls to the library. On error, #MP4_INVALID_FILE_HANDLE.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr MP4Read([MarshalAs(UnmanagedType.LPStr)] string fileName, IntPtr cb);

    /// <summary>
    /// Modify an existing mp4 file.
    /// </summary>
    /// <param name="fileName">pathname of the file to be modified. On Windows, this should be a UTF-8 encoded string. On other platforms, it should be an 8-bit encoding that is appropriate for the platform, locale, file system, etc. prefer to use UTF-8 when possible).</param>
    /// <param name="flags">flags currently ignored.</param>
    /// <remarks>
    /// <para>MP4Modify is the first call that should be used when you want to modify an existing mp4 file.It is roughly equivalent to opening a file in read/write mode.</para>
    /// <para>Since modifications to an existing mp4 file can result in a sub-optimal file layout, you may want to use MP4Optimize() after you have modified and closed the mp4 file.</para>
    /// </remarks>
    /// <returns>On success a handle of the target file for use in subsequent calls to the library. On error, #MP4_INVALID_FILE_HANDLE.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr MP4Modify([MarshalAs(UnmanagedType.LPStr)] string fileName, int flags);

    //// Commenting this API declaration. It isn't called yet, but may be in the future.
    //// [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    //// [return: MarshalAs(UnmanagedType.U1)]
    //// internal static extern bool MP4Optimize([MarshalAs(UnmanagedType.LPStr)]string fileName, [MarshalAs(UnmanagedType.LPStr)]string newName);

    /// <summary>
    /// Close an mp4 file.
    /// </summary>
    /// <param name="file">handle of file to close.</param>
    /// <remarks>MP4Close closes a previously opened mp4 file. If the file was opened writable with <see cref="MP4Modify(string, int)"/>, then <see cref="MP4Close(IntPtr)"/> will write out all pending information to disk.</remarks>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void MP4Close(IntPtr file);

    /// <summary>
    /// Frees the pointer.
    /// </summary>
    /// <param name="pointer">The pointer.</param>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void MP4Free(IntPtr pointer);

    /// <summary>
    /// Sets the name tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="name">name to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetName(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? name);

    /// <summary>
    /// Sets the artist tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="artist">artist to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? artist);

    /// <summary>
    /// Sets the album artist tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="albumArtist">album artist to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetAlbumArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? albumArtist);

    /// <summary>
    /// Sets the album tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="album">album to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetAlbum(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? album);

    /// <summary>
    /// Sets the grouping tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="grouping">grouping to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetGrouping(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? grouping);

    /// <summary>
    /// Sets the composer tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="composer">composer to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetComposer(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? composer);

    /// <summary>
    /// Sets the comments tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="comments">comments to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetComments(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? comments);

    /// <summary>
    /// Sets the genre tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="genre">genre to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetGenre(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? genre);

    /// <summary>
    /// Sets the genre type tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="genreType">gente type to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetGenreType(IntPtr tags, IntPtr genreType);

    /// <summary>
    /// Sets the release date tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="releaseDate">release date to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetReleaseDate(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? releaseDate);

    /// <summary>
    /// Sets the track tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="trackInfo">track info to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetTrack(IntPtr tags, IntPtr trackInfo);

    /// <summary>
    /// Sets the disk tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="discInfo">disc info to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetDisk(IntPtr tags, IntPtr discInfo);

    /// <summary>
    /// Sets the tempo tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="tempo">tempo to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetTempo(IntPtr tags, IntPtr tempo);

    /// <summary>
    /// Sets the compilation tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="isCompilation">whether this is a compilation.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetCompilation(IntPtr tags, IntPtr isCompilation);

    /// <summary>
    /// Sets the TV show tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="tvShow">TV show to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetTVShow(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? tvShow);

    /// <summary>
    /// Sets the TV network tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="tvNetwork">TV network to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetTVNetwork(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? tvNetwork);

    /// <summary>
    /// Sets the TV episode ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="tvEpisodeId">TV episode ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetTVEpisodeID(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? tvEpisodeId);

    /// <summary>
    /// Sets the TV season tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="seasonNumber">season number to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetTVSeason(IntPtr tags, IntPtr seasonNumber);

    /// <summary>
    /// Sets the TV episode tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="episodeNumber">episode number to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetTVEpisode(IntPtr tags, IntPtr episodeNumber);

    /// <summary>
    /// Sets the description tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="description">desciption to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetDescription(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? description);

    /// <summary>
    /// Sets the long description tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="longDescription">long description to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetLongDescription(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? longDescription);

    /// <summary>
    /// Sets the comments tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="lyrics">lyrics to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetLyrics(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? lyrics);

    /// <summary>
    /// Sets the sort name tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortName">sort name to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetSortName(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortName);

    /// <summary>
    /// Sets the sort artist tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortArtist">sort artist to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetSortArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortArtist);

    /// <summary>
    /// Sets the sort album artist tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortAlbumArtist">sort album artist to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetSortAlbumArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortAlbumArtist);

    /// <summary>
    /// Sets the sort album tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortAlbum">sort album to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetSortAlbum(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortAlbum);

    /// <summary>
    /// Sets the sort composer tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortComposer">sort composer to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetSortComposer(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortComposer);

    /// <summary>
    /// Sets the sort TV show tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="sortTVShow">sort TV show to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetSortTVShow(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? sortTVShow);

    /// <summary>
    /// Adds artwork.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="artwork">artwork to add.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsAddArtwork(IntPtr tags, IntPtr artwork);

    /// <summary>
    /// Sets the artwork tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="index">index to set arwork at.</param>
    /// <param name="artwork">artwork to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetArtwork(IntPtr tags, int index, IntPtr artwork);

    /// <summary>
    /// Removes the artwork at the specified index.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="index">index to remove at.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsRemoveArtwork(IntPtr tags, int index);

    /// <summary>
    /// Sets the copyright tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="copyright">copyright to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetCopyright(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? copyright);

    /// <summary>
    /// Sets the encoding tool tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="encodingTool">encoding tool to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetEncodingTool(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? encodingTool);

    /// <summary>
    /// Sets the encoded by tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="encodedBy">encoded by to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetEncodedBy(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? encodedBy);

    /// <summary>
    /// Sets the purchased date tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="purchaseDate">purchase date to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetPurchaseDate(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? purchaseDate);

    /// <summary>
    /// Sets the podcast tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="isPodcast">whether this is a podcast.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetPodcast(IntPtr tags, IntPtr isPodcast);

    /// <summary>
    /// Sets the keywords tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="podcastKeywords">podcast keywords to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetKeywords(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? podcastKeywords);

    /// <summary>
    /// Sets the category tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="podcastCategory">podcast category to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetCategory(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? podcastCategory);

    /// <summary>
    /// Sets the HD video tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="isHDVideo">whether this is a HD video.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetHDVideo(IntPtr tags, IntPtr isHDVideo);

    /// <summary>
    /// Sets the mediat type tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="mediaType">media type to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetMediaType(IntPtr tags, IntPtr mediaType);

    /// <summary>
    /// Sets the content rating tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="contentRating">content rating to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetContentRating(IntPtr tags, IntPtr contentRating);

    /// <summary>
    /// Sets the gapless tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="isGapless">is gapless to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetGapless(IntPtr tags, IntPtr isGapless);

    /// <summary>
    /// Sets the iTunes account tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="iTunesAccount">iTunes account to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetITunesAccount(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? iTunesAccount);

    /// <summary>
    /// Sets the iTunes account type tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="iTunesAccountType">iTunes account type to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetITunesAccountType(IntPtr tags, IntPtr iTunesAccountType);

    /// <summary>
    /// Sets the iTunes country tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="iTunesAccountCountry">iTunes account country to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetITunesCountry(IntPtr tags, IntPtr iTunesAccountCountry);

    /// <summary>
    /// Sets the content ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="contentId">content ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetContentID(IntPtr tags, IntPtr contentId);

    /// <summary>
    /// Sets the artist ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="artistId">artist ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetArtistID(IntPtr tags, IntPtr artistId);

    /// <summary>
    /// Sets the playlist ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="playlistId">playlist ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetPlaylistID(IntPtr tags, IntPtr playlistId);

    /// <summary>
    /// Sets the genre ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="genreId">genre ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetGenreID(IntPtr tags, IntPtr genreId);

    /// <summary>
    /// Sets the composer ID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="composerId">composer ID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetComposerID(IntPtr tags, IntPtr composerId);

    /// <summary>
    /// Sets the XID tag.
    /// </summary>
    /// <param name="tags">tags structure to fetch (write) into.</param>
    /// <param name="xid">XID to set.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4TagsSetXID(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string? xid);

    /// <summary>
    /// Get list of items by meaning from file.
    /// </summary>
    /// <param name="file">handle of file to operate on.</param>
    /// <param name="meaning">UTF-8 meaning. NULL-terminated.</param>
    /// <param name="name">may be NULL. UTF-8 name. NULL-terminated.</param>
    /// <returns>On succes, list of items, which must be free'd. On failure, <see cref="IntPtr.Zero"/>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr MP4ItmfGetItemsByMeaning(IntPtr file, [MarshalAs(UnmanagedType.LPStr)] string meaning, [MarshalAs(UnmanagedType.LPStr)] string? name);

    /// <summary>
    /// Free an item list (deep free).
    /// </summary>
    /// <param name="itemList">itemList to be free'd.</param>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void MP4ItmfItemListFree(IntPtr itemList);

    /// <summary>
    /// Allocate an item on the heap.
    /// </summary>
    /// <param name="code">four-char code identifying atom type. NULL-terminated.</param>
    /// <param name="numData">number of data elements to allocate. Must be >= 1.</param>
    /// <returns>newly allocated item.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr MP4ItmfItemAlloc([MarshalAs(UnmanagedType.LPStr)] string code, int numData);

    /// <summary>
    /// Add an item to file.
    /// </summary>
    /// <param name="hFile">handle of file to operate on.</param>
    /// <param name="item">object to add.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4ItmfAddItem(IntPtr hFile, IntPtr item);

    //// Commenting this API declaration. It isn't called yet, but may be in the future.
    //// [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    //// [return: MarshalAs(UnmanagedType.U1)]
    //// internal static extern bool MP4ItmfSetItem(IntPtr hFile, IntPtr item);

    /// <summary>
    /// Remove an existing item from file.
    /// </summary>
    /// <param name="hFile">handle of file to operate on.</param>
    /// <param name="item">object to remove. Must have a valid index obtained from prior get.</param>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4ItmfRemoveItem(IntPtr hFile, IntPtr item);

    /// <summary>
    /// Gets the number of tracks.
    /// </summary>
    /// <param name="hFile">handle of file to operate on.</param>
    /// <param name="type">type of track.</param>
    /// <param name="subType">sub type of track.</param>
    /// <returns>number of tracks.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int MP4GetNumberOfTracks(IntPtr hFile, [MarshalAs(UnmanagedType.LPStr)] string? type, byte subType);

    /// <summary>
    /// Finds the track ID.
    /// </summary>
    /// <param name="hFile">handle of file to operate on.</param>
    /// <param name="index">index to find.</param>
    /// <param name="type">type of track.</param>
    /// <param name="subType">sub type of track.</param>
    /// <returns>track ID.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int MP4FindTrackId(IntPtr hFile, short index, [MarshalAs(UnmanagedType.LPStr)] string? type, byte subType);

    /// <summary>
    /// Get the track type.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <param name="trackId">id of track for operation.</param>
    /// <returns>On success, a string indicating track type. On failure, <see langword="null"/>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern string MP4GetTrackType(IntPtr hFile, int trackId);

    /// <summary>
    /// Get the track media data name.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <param name="trackId">id of track for operation.</param>
    /// <returns>On success, a string indicating track type. On failure, <see langword="null"/>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern string MP4GetTrackMediaDataName(IntPtr hFile, int trackId);

    /// <summary>
    /// Get ISO-639-2/T language code of a track.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <param name="trackId">id of track for operation.</param>
    /// <param name="code">buffer to hold 3-char+null.</param>
    /// <remarks>The language code is a 3-char alpha code consisting of lower-case letters.</remarks>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4GetTrackLanguage(IntPtr hFile, int trackId, byte[] code);

    /// <summary>
    /// Set ISO-639-2/T language code of a track.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <param name="trackId">id of track for operation.</param>
    /// <param name="code">3-char language code.</param>
    /// <remarks>The language code is a 3-char alpha code consisting of lower-case letters.</remarks>
    /// <returns><b>true</b> on success, <b>false</b> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool MP4SetTrackLanguage(IntPtr hFile, int trackId, byte[] code);

    /// <summary>
    /// Convert duration from track time scale to an arbitrary time scale.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <param name="trackId">id of track for operation.</param>
    /// <param name="duration">value to be converted.</param>
    /// <param name="timeScale">time scale in ticks per second.</param>
    /// <returns>On success, the duration in arbitrary time scale units. On error, <b>0</b>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern long MP4ConvertFromTrackDuration(IntPtr hFile, int trackId, long duration, MP4TimeScale timeScale);

    /// <summary>
    /// Gets the track duration.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <param name="trackId">id of track for operation.</param>
    /// <returns>On success, the duration. On error, <b>0</b>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern long MP4GetTrackDuration(IntPtr hFile, int trackId);

    /// <summary>
    /// Get list of chapters.
    /// </summary>
    /// <param name="hFile">handle of file to read.</param>
    /// <param name="chapterList">address receiving array of chapter items. If a non-NULL is received the caller is responsible for freeing the memory with MP4Free().</param>
    /// <param name="chapterCount">address receiving count of items in array.</param>
    /// <param name="chapterType">the type of chapters to read.</param>
    /// <returns>the first type of chapters found.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static extern MP4ChapterType MP4GetChapters(IntPtr hFile, ref IntPtr chapterList, ref int chapterCount, MP4ChapterType chapterType);

    /// <summary>
    /// Set list of chapters.
    /// </summary>
    /// <param name="hFile">handle of file to modify.</param>
    /// <param name="chapterList">array of chapters items.</param>
    /// <param name="chapterCount">count of items in array.</param>
    /// <param name="chapterType">type of chapters to write.</param>
    /// <returns>the type of chapters written.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static extern MP4ChapterType MP4SetChapters(IntPtr hFile, [In, Out] MP4Chapter[] chapterList, int chapterCount, MP4ChapterType chapterType);

    /// <summary>
    /// Gets the duration.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <returns>On success, the duration. On error, <b>0</b>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern long MP4GetDuration(IntPtr hFile);

    /// <summary>
    /// Get the time scale of the movie (file).
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <returns>timescale (ticks per second) of the mp4 file.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int MP4GetTimeScale(IntPtr hFile);

    /// <summary>
    /// Models an iTunes Metadata Format data atom contained in an iTMF metadata item atom.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef struct MP4ItmfData_s
    /// {
    ///     uint8_t          typeSetIdentifier; /** always zero. */
    ///     MP4ItmfBasicType typeCode;          /** iTMF basic type. */
    ///     uint32_t         locale;            /** always zero. */
    ///     uint8_t*         value;             /** may be NULL. */
    ///     uint32_t         valueSize;         /** value size in bytes. */
    /// } MP4ItmfData;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MP4ItmfData
    {
        /// <summary>
        /// Always zero.
        /// </summary>
        internal byte typeSetIdentifier;

        /// <summary>
        /// Basic type of data.
        /// </summary>
        internal MP4ItmfBasicType typeCode;

        /// <summary>
        /// Always zero.
        /// </summary>
        internal int locale;

        /// <summary>
        /// Value of the data, may be NULL (<see cref="IntPtr.Zero"/>).
        /// </summary>
        internal IntPtr value;

        /// <summary>
        /// Value size in bytes.
        /// </summary>
        internal int valueSize;
    }

    /// <summary>
    /// Represents a list of data in an atom.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// List of data. */
    /// typedef struct MP4ItmfDataList_s
    /// {
    ///     MP4ItmfData* elements; /** flat array. NULL when size is zero. */
    ///     uint32_t     size;     /** number of elements. */
    /// } MP4ItmfDataList;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MP4ItmfDataList
    {
        /// <summary>
        /// flat array. NULL when size is zero.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray)]
        internal IntPtr[] elements;

        /// <summary>
        /// number of elements.
        /// </summary>
        internal int size;
    }

    /// <summary>
    /// Models an iTMF metadata item atom contained in an iTunes atom.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef struct MP4ItmfItem_s
    /// {
    ///     void* __handle; /** internal use only. */
    ///
    ///     char*           code;     /** four-char code identifing atom type. NULL-terminated. */
    ///     char*           mean;     /** may be NULL. UTF-8 meaning. NULL-terminated. */
    ///     char*           name;     /** may be NULL. UTF-8 name. NULL-terminated. */
    ///     MP4ItmfDataList dataList; /** list of data. can be zero length. */
    /// } MP4ItmfItem;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MP4ItmfItem
    {
        /// <summary>
        /// internal use only.
        /// </summary>
        internal IntPtr handle;

        /// <summary>
        /// four-char code identifying atom type. NULL-terminated.
        /// </summary>
        internal string code;

        /// <summary>
        /// may be NULL. UTF-8 meaning. NULL-terminated.
        /// </summary>
        internal string mean;

        /// <summary>
        /// may be NULL. UTF-8 name. NULL-terminated.
        /// </summary>
        internal string name;

        /// <summary>
        /// list of data. can be zero length.
        /// </summary>
        internal MP4ItmfDataList dataList;
    }

    /// <summary>
    /// List of items.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// typedef struct MP4ItmfItemList_s
    /// {
    ///     MP4ItmfItem* elements; /** flat array. NULL when size is zero. */
    ///     uint32_t     size;     /** number of elements. */
    /// } MP4ItmfItemList;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MP4ItmfItemList
    {
        /// <summary>
        /// flat array. NULL when size is zero.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray)]
        internal IntPtr[] elements;

        /// <summary>
        /// number of elements.
        /// </summary>
        internal int size;
    }

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
    internal struct MP4TagArtwork
    {
        /// <summary>
        /// raw picture data.
        /// </summary>
        internal IntPtr data;

        /// <summary>
        /// data size in bytes.
        /// </summary>
        internal int size;

        /// <summary>
        /// data type.
        /// </summary>
        internal ArtworkType type;
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
    internal struct MP4TagTrack
    {
        /// <summary>
        /// Track number.
        /// </summary>
        internal short index;

        /// <summary>
        /// Total number of tracks.
        /// </summary>
        internal short total;
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
    internal struct MP4TagDisk
    {
        /// <summary>
        /// Disc number.
        /// </summary>
        internal short index;

        /// <summary>
        /// Total number of discs.
        /// </summary>
        internal short total;
    }

    /// <summary>
    /// Represents information for a chapter in this file.
    /// </summary>
    /// <remarks>
    /// This structure definition is taken from the MP4V2 header files, documented thus:
    /// <para>
    /// <code>
    /// #define MP4V2_CHAPTER_TITLE_MAX 1023
    ///
    /// typedef struct MP4Chapter_s {
    ///     MP4Duration duration;
    ///     char title[MP4V2_CHAPTER_TITLE_MAX+1];
    /// } MP4Chapter_t;
    /// </code>
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MP4Chapter
    {
        /// <summary>
        /// Duration of chapter in milliseconds.
        /// </summary>
        internal long duration;

        /// <summary>
        /// Title of chapter.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        internal byte[] title;
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
    internal struct MP4Tags
    {
        /// <summary>
        /// Internal handle.
        /// </summary>
        internal IntPtr handle;

        /// <summary>
        /// Name of the file.
        /// </summary>
        internal string name;

        /// <summary>
        /// Artist for the file.
        /// </summary>
        internal string artist;

        /// <summary>
        /// Album artist for the file.
        /// </summary>
        internal string albumArtist;

        /// <summary>
        /// Album for the file.
        /// </summary>
        internal string album;

        /// <summary>
        /// Grouping for the file.
        /// </summary>
        internal string grouping;

        /// <summary>
        /// Composer for the file.
        /// </summary>
        internal string composer;

        /// <summary>
        /// Comment for the file.
        /// </summary>
        internal string comment;

        /// <summary>
        /// Genre for the file.
        /// </summary>
        internal string genre;

        /// <summary>
        /// Pointer to the genre type for the file.
        /// </summary>
        internal IntPtr genreType;

        /// <summary>
        /// Release data for the file.
        /// </summary>
        internal string releaseDate;

        /// <summary>
        /// Pointer to the track information about the file.
        /// </summary>
        internal IntPtr track;

        /// <summary>
        /// Pointer to the disc information about the file.
        /// </summary>
        internal IntPtr disk;

        /// <summary>
        /// Pointer to the tempo.
        /// </summary>
        internal IntPtr tempo;

        /// <summary>
        /// Pointer to the "isCompilation" value.
        /// </summary>
        internal IntPtr compilation;

        /// <summary>
        /// Pointer to the TV show name.
        /// </summary>
        internal string tvShow;

        /// <summary>
        /// Pointer to the TV network.
        /// </summary>
        internal string tvNetwork;

        /// <summary>
        /// Pointer to the TV episode ID.
        /// </summary>
        internal string tvEpisodeID;

        /// <summary>
        /// Pointer to the season number.
        /// </summary>
        internal IntPtr tvSeason;

        /// <summary>
        /// Pointer to the episode number.
        /// </summary>
        internal IntPtr tvEpisode;

        /// <summary>
        /// Description of the file.
        /// </summary>
        internal string description;

        /// <summary>
        /// Long description of the file.
        /// </summary>
        internal string longDescription;

        /// <summary>
        /// Lyrics of the file.
        /// </summary>
        internal string lyrics;

        /// <summary>
        /// Sort name of the file.
        /// </summary>
        internal string sortName;

        /// <summary>
        /// Sort artist of the file.
        /// </summary>
        internal string sortArtist;

        /// <summary>
        /// Sort album artist of the file.
        /// </summary>
        internal string sortAlbumArtist;

        /// <summary>
        /// Sort album of the file.
        /// </summary>
        internal string sortAlbum;

        /// <summary>
        /// Sort composer of the file.
        /// </summary>
        internal string sortComposer;

        /// <summary>
        /// Sort TV show of the file.
        /// </summary>
        internal string sortTVShow;

        /// <summary>
        /// Pointer to the artwork in the file.
        /// </summary>
        internal IntPtr artwork;

        /// <summary>
        /// The artwork count in the file.
        /// </summary>
        internal int artworkCount;

        /// <summary>
        /// Copyright in the file.
        /// </summary>
        internal string copyright;

        /// <summary>
        /// Encoding tool used for the file.
        /// </summary>
        internal string encodingTool;

        /// <summary>
        /// Encoded by information for the file.
        /// </summary>
        internal string encodedBy;

        /// <summary>
        /// Purchase date for the file.
        /// </summary>
        internal string purchasedDate;

        /// <summary>
        /// Pointer to the "isPodcast" value for the file.
        /// </summary>
        internal IntPtr podcast;

        /// <summary>
        /// Podcast keywords for the file.
        /// </summary>
        internal string keywords;

        /// <summary>
        /// Podcast category for the file.
        /// </summary>
        internal string category;

        /// <summary>
        /// Pointer to the "isHDVideo" value for the file.
        /// </summary>
        internal IntPtr hdVideo;

        /// <summary>
        /// Pointer to the media type for the file.
        /// </summary>
        internal IntPtr mediaType;

        /// <summary>
        /// Pointer to the content rating for the file.
        /// </summary>
        internal IntPtr contentRating;

        /// <summary>
        /// Pointer to the "isGapless" value for the file.
        /// </summary>
        internal IntPtr gapless;

        /// <summary>
        /// iTunes account used to purchase the file.
        /// </summary>
        internal string itunesAccount;

        /// <summary>
        /// Pointer to the type of iTunes account used to purchase the file.
        /// </summary>
        internal IntPtr iTunesAccountType;

        /// <summary>
        /// Pointer to the country for the iTunes account used to purchase the file.
        /// </summary>
        internal IntPtr iTunesCountry;

        /// <summary>
        /// Pointer to the content ID of the file.
        /// </summary>
        internal IntPtr contentID;

        /// <summary>
        /// Pointer to the artist ID of the file.
        /// </summary>
        internal IntPtr artistID;

        /// <summary>
        /// Pointer to the playlist ID of the file.
        /// </summary>
        internal IntPtr playlistID;

        /// <summary>
        /// Pointer to the genre ID of the file.
        /// </summary>
        internal IntPtr genreID;

        /// <summary>
        /// Pointer to the composer ID of the file.
        /// </summary>
        internal IntPtr composerID;

        /// <summary>
        /// Auxiliary ID of the file.
        /// </summary>
        internal string xid;
    }
}