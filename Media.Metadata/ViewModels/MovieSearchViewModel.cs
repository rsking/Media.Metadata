// -----------------------------------------------------------------------
// <copyright file="MovieSearchViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

/// <summary>
/// The <see cref="Movie"/> <see cref="VideoSearchViewModel"/>.
/// </summary>
/// <param name="movieSearch">The movie search.</param>
internal sealed partial class MovieSearchViewModel(IMovieSearch movieSearch) : VideoSearchViewModel
{
    private readonly IMovieSearch movieSearch = movieSearch;

    /// <summary>
    /// Gets or sets the year.
    /// </summary>
    public int? Year { get; set; }

    /// <summary>
    /// Searches for the movie.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The task.</returns>
    [CommunityToolkit.Mvvm.Input.RelayCommand(AllowConcurrentExecutions = false)]
    public Task Search(CancellationToken cancellationToken) => this.SetVideos(this.movieSearch.SearchAsync(this.Name!, this.Year ?? 0, this.SelectedCountry?.Abbreviation ?? Country.Australia.Abbreviation, cancellationToken));
}