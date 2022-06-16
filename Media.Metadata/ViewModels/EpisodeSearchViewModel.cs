// -----------------------------------------------------------------------
// <copyright file="EpisodeSearchViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

/// <summary>
/// The <see cref="Episode"/> <see cref="VideoSearchViewModel"/>.
/// </summary>
internal partial class EpisodeSearchViewModel : VideoSearchViewModel
{
    private readonly System.Collections.ObjectModel.ObservableCollection<Series> series = new();

    private readonly System.Collections.ObjectModel.ObservableCollection<Season> seasons = new();

    private readonly IShowSearch showSearch;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private Series? selectedSeries;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private Season? selectedSeason;

    /// <summary>
    /// Initialises a new instance of the <see cref="EpisodeSearchViewModel"/> class.
    /// </summary>
    /// <param name="showSearch">The series search.</param>
    public EpisodeSearchViewModel(IShowSearch showSearch) => this.showSearch = showSearch;

    /// <summary>
    /// Gets the series.
    /// </summary>
    public IEnumerable<Series> Series => this.series;

    /// <summary>
    /// Gets the seasons.
    /// </summary>
    public IEnumerable<Season> Seasons => this.seasons;

    /// <summary>
    /// Gets or sets the year.
    /// </summary>
    public int? Year { get; set; }

    /// <summary>
    /// Searches for the episode.
    /// </summary>
    /// <returns>The task.</returns>
    [ICommand(AllowConcurrentExecutions = false)]
    public async Task SearchSeries()
    {
        this.series.Clear();
        await foreach (var s in this.showSearch.SearchAsync(this.Name!, this.Year ?? 0, this.SelectedCountry?.Abbreviation ?? Country.Australia.Abbreviation).ConfigureAwait(true))
        {
            this.series.Add(s);
        }
    }
}