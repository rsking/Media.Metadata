// -----------------------------------------------------------------------
// <copyright file="StringMarshaller.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The string marshaller.
/// </summary>
internal static class StringMarshaller
{
    private static readonly System.Text.Encoding Windows1252 = System.Text.Encoding.GetEncoding(1252);

    /// <summary>
    /// Marshals from ANSI to UTF8.
    /// </summary>
    /// <param name="input">The input in ANSI.</param>
    /// <returns>The output in UTF8.</returns>
    public static string? AnsiToUtf8(string? input) => input is null
        ? input
        : System.Text.Encoding.UTF8.GetString(Windows1252.GetBytes(input));

    /// <summary>
    /// Marshals from UTF8 to ANSI.
    /// </summary>
    /// <param name="input">The input in UTF8.</param>
    /// <returns>The output in ANSI.</returns>
    public static string? Utf8ToAnsi(string? input) => input is null
        ? input
        : Windows1252.GetString(System.Text.Encoding.UTF8.GetBytes(input));
}