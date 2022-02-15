// -----------------------------------------------------------------------
// <copyright file="NativeMethods.File.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System;
using System.Runtime.InteropServices;

/// <content>
/// Methods from <c>file.h</c>.
/// </content>
internal static partial class NativeMethods
{
    /// <summary>
    /// The should parse atom call back.
    /// </summary>
    /// <param name="atom">The atom.</param>
    /// <returns><see langword="true"/> to parse the atom.</returns>
    public delegate bool ShouldParseAtomCallback(uint atom);

    /// <summary>
    /// Close an mp4 file.
    /// </summary>
    /// <param name="file">handle of file to close.</param>
    /// <remarks>MP4Close closes a previously opened mp4 file. If the file was opened writable with <see cref="MP4Modify(byte[], int)"/>, then <see cref="MP4Close(IntPtr)"/> will write out all pending information to disk.</remarks>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MP4Close(IntPtr file);

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
    public static extern IntPtr MP4Modify(byte[] fileName, int flags);

    /// <summary>
    /// Optimize the layout of an mp4 file.
    /// </summary>
    /// <param name="fileName">pathname of (existing) file to be optimized. On Windows, this should be a UTF-8 encoded string. On other platforms, it should be an 8-bit encoding that is appropriate for the platform, locale, file system, etc. (prefer to use UTF-8 when possible).</param>
    /// <param name="newName">
    /// <para>pathname of the new optimized file. On Windows, this should be a UTF-8 encoded string. On other platforms, it should be an 8-bit encoding that is appropriate for the platform, locale, file system, etc. (prefer to use UTF-8 when possible).</para>
    /// <para>If <see langword="null"/> a temporary file in the same directory as the <paramref name="fileName"/> will be used and <paramref name="fileName"/> will be over-written upon successful completion.</para>
    /// </param>
    /// <remarks>
    /// <para>MP4Optimize reads an existing mp4 file and writes a new version of the file with the two important changes:</para>
    /// <para>First, the mp4 control information is moved to the beginning of the file. (Frequenty it is at the end of the file due to it being constantly modified as track samples are added to an mp4 file). This optimization is useful in that in allows the mp4 file to be HTTP streamed.</para>
    /// <para>Second, the track samples are interleaved so that the samples for a particular instant in time are colocated within the file. This eliminates disk seeks during playback of the file which results in better performance.</para>
    /// <para>There are also two important side effects of <see cref="MP4Optimize"/>:</para>
    /// <para>First, any free blocks within the mp4 file are eliminated.</para>
    /// <para>Second, as a side effect of the sample interleaving process any media data chunks that are not actually referenced by the mp4 control structures are deleted. This is useful if you have called MP4DeleteTrack() which only deletes the control information for a track, and not the actual media data.</para></remarks>
    /// <returns><see langword="true"/> on success, <see langword="false"/> on failure.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool MP4Optimize(byte[] fileName, byte[]? newName);

    /// <summary>
    /// Read an existing mp4 file.
    /// </summary>
    /// <param name="fileName">pathname of the file to be read. On Windows, this should be a UTF-8 encoded string. On other platforms, it should be an 8-bit encoding that is appropriate for the platform, locale, file system, etc. (prefer to use UTF-8 when possible).</param>
    /// <param name="cb">The call back.</param>
    /// <remarks>MP4Read is the first call that should be used when you want to just read an existing mp4 file.It is equivalent to opening a file for reading, but in addition the mp4 file is parsed and the controlinformation is loaded into memory.Note that actual track samples are notread into memory until MP4ReadSample() is called.</remarks>
    /// <returns>On success a handle of the file for use in subsequent calls to the library. On error, #MP4_INVALID_FILE_HANDLE.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr MP4Read(byte[] fileName, IntPtr cb);
}