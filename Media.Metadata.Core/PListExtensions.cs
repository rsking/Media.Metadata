// -----------------------------------------------------------------------
// <copyright file="PListExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Extension methods for <see cref="PList"/>.
/// </summary>
public static class PListExtensions
{
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
    public static void AddIfNotNullOrEmpty(this PList plist, string key, string separator, IEnumerable<string>? values)
    {
        if (values is null)
        {
            return;
        }

        plist.AddIfNotNullOrEmpty(key, string.Join(separator, values));
    }

    /// <summary>
    /// Adds the string values as dictionaries wrapped in an array.
    /// </summary>
    /// <param name="plist">The PList.</param>
    /// <param name="key">The array key.</param>
    /// <param name="values">The values.</param>
    public static void AddIfNotNull(this PList plist, string key, IEnumerable<string>? values)
    {
        if (values is null)
        {
            return;
        }

        Add(plist, key, values);
    }

    /// <summary>
    /// Adds the string values as dictionaries wrapped in an array.
    /// </summary>
    /// <param name="plist">The PList.</param>
    /// <param name="key">The array key.</param>
    /// <param name="values">The values.</param>
    public static void Add(this PList plist, string key, IEnumerable<string> values)
    {
        var @array = values.ToPListArray("name");
        if (array.Length == 0)
        {
            return;
        }

        plist.Add(key, array);
    }

    private static object[] ToPListArray(this IEnumerable<string> values, string key) => values.Select<string, object>(value => new Dictionary<string, object>(StringComparer.Ordinal) { { key, value } }).ToArray();
}