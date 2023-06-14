// -----------------------------------------------------------------------
// <copyright file="EpisodeSearchView.xaml.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Views;

/// <summary>
/// The <see cref="Episode"/> search view.
/// </summary>
public sealed partial class EpisodeSearchView : Microsoft.UI.Xaml.Controls.UserControl
{
    /// <summary>
    /// Initialises a new instance of the <see cref="EpisodeSearchView"/> class.
    /// </summary>
    /// <param name="episode">The episode.</param>
    internal EpisodeSearchView(ViewModels.EpisodeSearchViewModel episode)
    {
        this.InitializeComponent();
        this.DataContext = episode;
    }
}