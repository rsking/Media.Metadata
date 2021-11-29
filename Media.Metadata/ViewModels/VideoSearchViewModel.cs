// -----------------------------------------------------------------------
// <copyright file="VideoSearchViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

/// <summary>
/// The <see cref="Video"/> <see cref="VideoSearchViewModel"/>.
/// </summary>
internal abstract partial class VideoSearchViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    private readonly System.Collections.ObjectModel.ObservableCollection<Video> videos = new();

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private Video? selectedVideo;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private Country? selectedCountry;

    /// <summary>
    /// Initialises a new instance of the <see cref="VideoSearchViewModel"/> class.
    /// </summary>
    protected VideoSearchViewModel()
    {
        this.Countries = Country.All;
        this.SelectedCountry = Country.Australia;
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets the countries.
    /// </summary>
    public IEnumerable<Country> Countries { get; }

    /// <summary>
    /// Gets the videos.
    /// </summary>
    public IEnumerable<Video> Videos => this.videos;

    /// <summary>
    /// Sets the videos.
    /// </summary>
    /// <param name="videos">The videos.</param>
    /// <returns>The task.</returns>
    protected async Task SetVideos(IAsyncEnumerable<Video> videos)
    {
        this.videos.Clear();
        await foreach (var video in videos.ConfigureAwait(true))
        {
            this.videos.Add(video);
        }
    }
}