// -----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Plex;

/// <summary>
/// Extension methods.
/// </summary>
internal static class ExtensionMethods
{
    private const char ReplacementChar = '_';

    private static readonly char[] SingleQuoteChars = ['’', '‘'];
    private static readonly char[] DoubleQuoteChars = ['“', '”'];
    private static readonly char[] DirectorySeparatorChars = [Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar];

    /// <summary>
    /// Sanitises the string, changing exotic characters, to common ones, such as smart quotes.
    /// </summary>
    /// <param name="stringValue">The string value.</param>
    /// <returns>A sanitised version of <paramref name="stringValue"/>.</returns>
    public static string Sanitise(this string stringValue) => stringValue
        .ReplaceAll(SingleQuoteChars, '\'') // single quotes
        .ReplaceAll(DoubleQuoteChars, '"') // double quotes
        .ReplaceAll(DirectorySeparatorChars, ReplacementChar); // possible directory separators

    /// <summary>
    /// Replaces all instances of <paramref name="oldValues" /> with <paramref name="newValue" />.
    /// </summary>
    /// <param name="stringValue">The string value.</param>
    /// <param name="oldValues">The old values.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns><paramref name="stringValue"/> with all instances of <paramref name="oldValues" /> with <paramref name="newValue" />.</returns>
    public static string ReplaceAll(this string stringValue, char[] oldValues, char newValue = ReplacementChar) => oldValues.Aggregate(stringValue, (v, c) => v.Replace(c, newValue));

    /// <summary>
    /// Gets the directory name.
    /// </summary>
    /// <param name="directoryInfo">The directory information.</param>
    /// <param name="invalidCharacters">The invalid characters.</param>
    /// <returns>The directory name.</returns>
    public static string GetName(this DirectoryInfo directoryInfo, char[] invalidCharacters)
    {
        // remove the root
        var name = directoryInfo.FullName;

        if (directoryInfo.Root?.FullName is { } rootName)
        {
            name = name[rootName.Length..];
            return Path.Combine(rootName, name.ReplaceAll(invalidCharacters));
        }

        return name.ReplaceAll(invalidCharacters);
    }
}