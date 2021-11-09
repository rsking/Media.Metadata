// -----------------------------------------------------------------------
// <copyright file="MediaStoreAccountKind.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Indicates the type of iTunes Music Store account with which this file was purchased.
/// </summary>
internal enum MediaStoreAccountKind : byte
{
    /// <summary>
    /// Indicates the file was purchased with an Apple iTunes account.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "iTunes is a proper name and is spelled correctly.")]
    iTunes = 0,

    /// <summary>
    /// Indicates the file was purchased with an AOL iTunes account.
    /// </summary>
    Aol = 1,

    /// <summary>
    /// Indicates the account type was not set in this file.
    /// </summary>
    NotSet = byte.MaxValue,
}