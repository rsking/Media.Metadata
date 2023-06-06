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
    [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(UI.Properties.Resources), Name = nameof(UI.Properties.Resources.NotSet))]
    [Display(ResourceType = typeof(UI.Properties.Resources), Name = nameof(UI.Properties.Resources.NotSet))]
    NotSet,

    /// <summary>
    /// A Movie.
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(UI.Properties.Resources), Name = nameof(UI.Properties.Resources.Movie))]
    [Display(ResourceType = typeof(UI.Properties.Resources), Name = nameof(UI.Properties.Resources.Movie))]
    Movie,

    /// <summary>
    /// A TV Show.
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(UI.Properties.Resources), Name = nameof(UI.Properties.Resources.TVShow))]
    [Display(ResourceType = typeof(UI.Properties.Resources), Name = nameof(UI.Properties.Resources.TVShow))]
    TVShow,
}