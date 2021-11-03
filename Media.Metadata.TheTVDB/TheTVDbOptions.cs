// -----------------------------------------------------------------------
// <copyright file="TheTVDbOptions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.TheTVDB;

/// <summary>
/// The TVDb options.
/// </summary>
public class TheTVDbOptions
{
    /// <summary>
    /// Gets or sets the URL.
    /// </summary>
    public string Url { get; set; } = "https://api4.thetvdb.com/v4";

    /// <summary>
    /// Gets or sets the pin.
    /// </summary>
    public string? Pin { get; set; }
}