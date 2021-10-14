// -----------------------------------------------------------------------
// <copyright file="AppleTagExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Extensions for <see cref="TagLib.Mpeg4.AppleTag"/>.
/// </summary>
internal static class AppleTagExtensions
{
    private static readonly TagLib.ReadOnlyByteVector StikAtom = "stik";

    /// <summary>
    /// Returns whether the <see cref="TagLib.Mpeg4.AppleTag"/> represents a movie.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns><see langword="true"/> if <paramref name="appleTag"/> is a movie; otherwise <see langword="false"/>.</returns>
    public static bool IsMovie(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.DataBoxes(StikAtom).FirstOrDefault(item => item.Data.Count == 1) is TagLib.Mpeg4.AppleDataBox stikAtom && stikAtom.Data.Data[0] == 9;

    /// <summary>
    /// Sets the apple TAG as a movie.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    public static void SetMovie(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.SetData(StikAtom, new TagLib.ByteVector(new byte[] { 9 }), 21U);

    /// <summary>
    /// Returns whether the <see cref="TagLib.Mpeg4.AppleTag"/> represents a TV show.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns><see langword="true"/> if <paramref name="appleTag"/> is a TV show; otherwise <see langword="false"/>.</returns>
    public static bool IsTvShow(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.DataBoxes(StikAtom).FirstOrDefault(item => item.Data.Count == 1) is TagLib.Mpeg4.AppleDataBox stikAtom && stikAtom.Data.Data[0] == 10;

    /// <summary>
    /// Sets the apple tag as a TV show.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    public static void SetTvShow(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.SetData(StikAtom, new TagLib.ByteVector(new byte[] { 10 }), 21U);
}