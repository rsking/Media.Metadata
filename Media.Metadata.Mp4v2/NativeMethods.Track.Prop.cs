// -----------------------------------------------------------------------
// <copyright file="NativeMethods.Track.Prop.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <content>
/// Methods from <c>track_prop.h</c>.
/// </content>
internal static partial class NativeMethods
{
    /// <summary>
    /// Get the track type.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <param name="trackId">id of track for operation.</param>
    /// <returns>On success, a string indicating track type. On failure, <see langword="null"/>.</returns>
    public static string? MP4GetTrackType(IntPtr hFile, int trackId)
    {
        var ptr = MP4GetTrackTypeInterop(hFile, trackId);
        return Marshal.PtrToStringAnsi(ptr);
    }

    /// <summary>
    /// Get the track media data name.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <param name="trackId">id of track for operation.</param>
    /// <returns>On success, a string indicating track type. On failure, <see langword="null"/>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern string MP4GetTrackMediaDataName(IntPtr hFile, int trackId);

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
    public static extern bool MP4GetTrackLanguage(IntPtr hFile, int trackId, byte[] code);

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
    public static extern bool MP4SetTrackLanguage(IntPtr hFile, int trackId, byte[] code);

    /// <summary>
    /// Gets the track duration.
    /// </summary>
    /// <param name="hFile">handle of file for operation.</param>
    /// <param name="trackId">id of track for operation.</param>
    /// <returns>On success, the duration. On error, <b>0</b>.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern long MP4GetTrackDuration(IntPtr hFile, int trackId);

    [DllImport("libmp4v2.dll", EntryPoint = nameof(MP4GetTrackType), CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr MP4GetTrackTypeInterop(IntPtr hFile, int trackId);
}