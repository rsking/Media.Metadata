// -----------------------------------------------------------------------
// <copyright file="StringMarshaller.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System.Text;

/// <summary>
/// The string marshaller.
/// </summary>
internal static class StringMarshaller
{
    private static readonly Encoding Windows1252 = Encoding.GetEncoding(1252);

    /// <summary>
    /// Marshals from ANSI to UTF8.
    /// </summary>
    /// <param name="input">The input in ANSI.</param>
    /// <returns>The output in UTF8.</returns>
    public static string? AnsiToUtf8(string? input) => input is null ? input : Encoding.UTF8.GetString(Windows1252.GetBytes(input));

    /// <summary>
    /// Marshals from UTF8 to ANSI.
    /// </summary>
    /// <param name="input">The input in UTF8.</param>
    /// <returns>The output in ANSI.</returns>
    public static string? Utf8ToAnsi(string? input) => input is null ? input : Windows1252.GetString(Encoding.UTF8.GetBytes(input));
}