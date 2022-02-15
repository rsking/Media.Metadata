// -----------------------------------------------------------------------
// <copyright file="NativeMethods.Chapter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System.Runtime.InteropServices;

/// <content>
/// Methods from <c>chapter.h</c>.
/// </content>
internal static partial class NativeMethods
{
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
    public enum MP4ChapterType
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
    /// Get list of chapters.
    /// </summary>
    /// <param name="hFile">handle of file to read.</param>
    /// <param name="chapterList">address receiving array of chapter items. If a non-NULL is received the caller is responsible for freeing the memory with MP4Free().</param>
    /// <param name="chapterCount">address receiving count of items in array.</param>
    /// <param name="chapterType">the type of chapters to read.</param>
    /// <returns>the first type of chapters found.</returns>
    [DllImport("libmp4v2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I4)]
    public static extern MP4ChapterType MP4GetChapters(IntPtr hFile, ref IntPtr chapterList, ref int chapterCount, MP4ChapterType chapterType);

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
    public static extern MP4ChapterType MP4SetChapters(IntPtr hFile, [In, Out] MP4Chapter[] chapterList, int chapterCount, MP4ChapterType chapterType);

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
    public struct MP4Chapter
    {
        /// <summary>
        /// Duration of chapter in milliseconds.
        /// </summary>
        public long duration;

        /// <summary>
        /// Title of chapter.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public byte[] title;
    }
}