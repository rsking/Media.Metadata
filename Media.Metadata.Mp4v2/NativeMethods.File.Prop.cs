// -----------------------------------------------------------------------
// <copyright file="NativeMethods.File.Prop.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <content>
/// Methods from <c>file_prop.h</c>.
/// </content>
internal static partial class NativeMethods
{
    /// <summary>
    /// Gets the duration.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <returns>On success, the duration. On error, <b>0</b>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern long MP4GetDuration(IntPtr hFile);

    /// <summary>
    /// Get the time scale of the movie (file).
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <returns>timescale (ticks per second) of the mp4 file.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern int MP4GetTimeScale(IntPtr hFile);
}