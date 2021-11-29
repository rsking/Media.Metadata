// -----------------------------------------------------------------------
// <copyright file="DummyClass.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

/// <summary>
/// Dummy class for binding.
/// </summary>
[Microsoft.UI.Xaml.Data.Bindable]
public class DummyClass
{
    /// <summary>
    /// Gets or sets the dummy video type.
    /// </summary>
    public VideoType DummyVideoType { get; set; }
}