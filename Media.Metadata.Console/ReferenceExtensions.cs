// -----------------------------------------------------------------------
// <copyright file="ReferenceExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Extensions.
/// </summary>
internal static class ReferenceExtensions
{
    /// <summary>
    /// Throws an exception if the value is null.
    /// </summary>
    /// <typeparam name="T">The type of value.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>The non-null value.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> was <see langword="null"/>.</exception>
    public static T ThrowIfNull<T>(this T? value)
        where T : class => value ?? throw new ArgumentNullException(nameof(value));

    /// <summary>
    /// Throws if the file does not exist.
    /// </summary>
    /// <param name="value">The file information.</param>
    /// <returns>The existing file.</returns>
    /// <exception cref="FileNotFoundException"><paramref name="value"/> does not exist.</exception>
    public static FileInfo ThrowIfNotExists(this FileInfo value) => value.Exists ? value : throw new FileNotFoundException(null, value.FullName);
}