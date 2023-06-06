// -----------------------------------------------------------------------
// <copyright file="NativeMethods.Track.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <content>
/// Methods from <c>track.h</c>.
/// </content>
internal static partial class NativeMethods
{
    /// <summary>
    /// Gets the number of tracks.
    /// </summary>
    /// <param name="hFile">handle of file to operate on.</param>
    /// <param name="type">type of track.</param>
    /// <param name="subType">sub type of track.</param>
    /// <returns>number of tracks.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern int MP4GetNumberOfTracks(IntPtr hFile, [MarshalAs(UnmanagedType.LPStr)] string? type, byte subType);

    /// <summary>
    /// Finds the track ID.
    /// </summary>
    /// <param name="hFile">handle of file to operate on.</param>
    /// <param name="index">index to find.</param>
    /// <param name="type">type of track.</param>
    /// <param name="subType">sub type of track.</param>
    /// <returns>track ID.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern int MP4FindTrackId(IntPtr hFile, short index, [MarshalAs(UnmanagedType.LPStr)] string? type, byte subType);
}