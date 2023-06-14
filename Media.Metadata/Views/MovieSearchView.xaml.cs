// -----------------------------------------------------------------------
// <copyright file="MovieSearchView.xaml.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Views;

/// <summary>
/// The <see cref="Movie"/> search view.
/// </summary>
public sealed partial class MovieSearchView : Microsoft.UI.Xaml.Controls.UserControl
{
    /// <summary>
    /// Initialises a new instance of the <see cref="MovieSearchView"/> class.
    /// </summary>
    /// <param name="movie">The movie.</param>
    internal MovieSearchView(ViewModels.MovieSearchViewModel movie)
    {
        this.InitializeComponent();
        this.DataContext = movie;
    }
}