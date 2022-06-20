// -----------------------------------------------------------------------
// <copyright file="MovieSearchViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

using CommunityToolkit.Mvvm.Input;

/// <summary>
/// The <see cref="Movie"/> <see cref="VideoSearchViewModel"/>.
/// </summary>
internal partial class MovieSearchViewModel : VideoSearchViewModel
{
    private readonly IMovieSearch movieSearch;

    /// <summary>
    /// Initialises a new instance of the <see cref="MovieSearchViewModel"/> class.
    /// </summary>
    /// <param name="movieSearch">The movie search.</param>
    public MovieSearchViewModel(IMovieSearch movieSearch) => this.movieSearch = movieSearch;

    /// <summary>
    /// Gets or sets the year.
    /// </summary>
    public int? Year { get; set; }

    /// <summary>
    /// Searches for the movie.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The task.</returns>
    [RelayCommand(AllowConcurrentExecutions = false)]
    public Task Search() => this.SetVideos(this.movieSearch.SearchAsync(this.Name!, this.Year ?? 0, this.SelectedCountry?.Abbreviation ?? Country.Australia.Abbreviation));
}