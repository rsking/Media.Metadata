// -----------------------------------------------------------------------
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
internal static partial class NativeMethods
{
    /// <summary>
    /// Invalid track ID.
    /// </summary>
    public const int MP4InvalidTrackId = 0;

    /// <summary>
    /// Od track type.
    /// </summary>
    public const string MP4OdTrackType = "odsm";

    /// <summary>
    /// Scene track type.
    /// </summary>
    public const string MP4SceneTrackType = "sdsm";

    /// <summary>
    /// Audio track type.
    /// </summary>
    public const string MP4AudioTrackType = "soun";

    /// <summary>
    /// Video track type.
    /// </summary>
    public const string MP4VideoTrackType = "vide";

    /// <summary>
    /// Hint track type.
    /// </summary>
    public const string MP4HintTrackType = "hint";

    /// <summary>
    /// Control track type.
    /// </summary>
    public const string MP4ControlTrackType = "cntl";

    /// <summary>
    /// Text track type.
    /// </summary>
    public const string MP4TextTrackType = "text";

    /// <summary>
    /// Subtitle track type.
    /// </summary>
    public const string MP4SubtitleTrackType = "sbtl";

    /// <summary>
    /// Sub-picture track type.
    /// </summary>
    public const string MP4SubpictureTrackType = "subp";

    /// <summary>
    /// Values representing the time scale for a track.
    /// </summary>
    public enum MP4TimeScale
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
    /// Frees the pointer.
    /// </summary>
    /// <param name="pointer">The pointer.</param>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MP4Free(IntPtr pointer);

    /// <summary>
    /// Convert duration from track time scale to an arbitrary time scale.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <param name="trackId">id of track for operation.</param>
    /// <param name="duration">value to be converted.</param>
    /// <param name="timeScale">time scale in ticks per second.</param>
    /// <returns>On success, the duration in arbitrary time scale units. On error, <b>0</b>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern long MP4ConvertFromTrackDuration(IntPtr hFile, int trackId, long duration, MP4TimeScale timeScale);
}