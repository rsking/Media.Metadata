// -----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Extension methods.
/// </summary>
internal static class ExtensionMethods
{
    private static readonly TagLib.ReadOnlyByteVector StikAtom = "stik";

    /// <summary>
    /// Adds the value if it is not null or empty.
    /// </summary>
    /// <param name="plist">The list.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public static void AddIfNotNullOrEmpty(this PList plist, string key, string? value)
    {
        if (value is null || string.IsNullOrEmpty(value))
        {
            return;
        }

        plist.Add(key, value);
    }

    /// <summary>
    /// Adds the values as a join string, if it is not null or empty.
    /// </summary>
    /// <param name="plist">The list.</param>
    /// <param name="key">The key.</param>
    /// <param name="separator">The separator.</param>
    /// <param name="values">The values.</param>
    public static void AddIfNotNullOrEmpty(this PList plist, string key, string separator, IEnumerable<string> values) => plist.AddIfNotNullOrEmpty(key, string.Join(separator, values));

    /// <summary>
    /// Adds the string values as dictionaries wrapped in an array.
    /// </summary>
    /// <param name="plist">The PList.</param>
    /// <param name="key">The array key.</param>
    /// <param name="values">The values.</param>
    public static void AddAsArray(this PList plist, string key, IEnumerable<string> values)
    {
        var @array = values.ToPListArray("name");
        if (array.Length == 0)
        {
            return;
        }

        plist.Add(key, array);
    }

    /// <summary>
    /// Converts the values as dictionaries wrapped in an array.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <param name="key">The dictionary key.</param>
    /// <returns>The array.</returns>
    public static object[] ToPListArray(this IEnumerable<string> values, string key) => values.Select<string, object>(value => new Dictionary<string, object>(StringComparer.Ordinal) { { key, value } }).ToArray();

    /// <summary>
    /// Returns whether the <see cref="TagLib.Mpeg4.AppleTag"/> represents a movie.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns><see langword="true"/> if <paramref name="appleTag"/> is a movie; otherwise <see langword="false"/>.</returns>
    public static bool IsMovie(this TagLib.Mpeg4.AppleTag appleTag)
    {
        return appleTag.DataBoxes(StikAtom).FirstOrDefault(item => item.Data.Count == 1) is TagLib.Mpeg4.AppleDataBox stikAtom
            && stikAtom.Data.Data[0] == 9;
    }

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
    public static bool IsTvShow(this TagLib.Mpeg4.AppleTag appleTag)
    {
        return appleTag.DataBoxes(StikAtom).FirstOrDefault(item => item.Data.Count == 1) is TagLib.Mpeg4.AppleDataBox stikAtom
            && stikAtom.Data.Data[0] == 10;
    }

    /// <summary>
    /// Sets the apple tag as a TV show.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    public static void SetTvShow(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.SetData(StikAtom, new TagLib.ByteVector(new byte[] { 10 }), 21U);
}