// -----------------------------------------------------------------------
// <copyright file="VideoType.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

/// <summary>
/// The video type.
/// </summary>
public enum VideoType
{
    /// <summary>
    /// Not set.
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Properties.Resources), Name = nameof(NotSet))]
    [Display(ResourceType = typeof(Properties.Resources), Name = nameof(NotSet))]
    NotSet,

    /// <summary>
    /// A Movie.
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Properties.Resources), Name = nameof(Movie))]
    [Display(ResourceType = typeof(Properties.Resources), Name = nameof(Movie))]
    Movie,

    /// <summary>
    /// A TV Show.
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Properties.Resources), Name = nameof(TVShow))]
    [Display(ResourceType = typeof(Properties.Resources), Name = nameof(TVShow))]
    TVShow,
}