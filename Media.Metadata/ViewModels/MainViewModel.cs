// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

using System;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

/// <summary>
/// The main view model.
/// </summary>
internal partial class MainViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    private readonly IReader reader;

    private readonly IUpdater updater;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    [CommunityToolkit.Mvvm.ComponentModel.AlsoNotifyChangeFor(nameof(SelectedEditableVideo))]
    private Video? selectedVideo;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private bool isSaving;

    /// <summary>
    /// Initialises a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="updater">The updater.</param>
    public MainViewModel(IReader reader, IUpdater updater)
    {
        this.reader = reader;
        this.updater = updater;
    }

    /// <summary>
    /// Gets the videos.
    /// </summary>
    public IList<Video> Videos { get; } = new System.Collections.ObjectModel.ObservableCollection<Video>();

    /// <summary>
    /// Gets the selected editable video.
    /// </summary>
    public VideoViewModel? SelectedEditableVideo { get; private set; }

    /// <summary>
    /// Gets the selected videos.
    /// </summary>
    public IList<Video> SelectedVideos { get; } = new System.Collections.ObjectModel.ObservableCollection<Video>();

    /// <summary>
    /// Adds videos.
    /// </summary>
    /// <returns>The task.</returns>
    [ICommand]
    public async Task AddVideos()
    {
        // Open a text file.
        var picker = new Windows.Storage.Pickers.FileOpenPicker
        {
            SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary,
            ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
            FileTypeFilter = { ".mp4", ".m4v", "*" },
        };

        picker.Init();

        var files = await picker.PickMultipleFilesAsync();

        if (files is not null)
        {
            foreach (var file in files)
            {
                if (await this.ReadVideoAsync(file.Path).ConfigureAwait(true) is Video video)
                {
                    this.Videos.Add(video);
                }
            }
        }
    }

    /// <summary>
    /// Removes the selected video.
    /// </summary>
    /// <returns>The task.</returns>
    [ICommand]
    public async Task RemoveVideo()
    {
        if (this.SelectedVideos.Count > 0)
        {
            // get the selected videos
            var selectedVideos = new Video[this.SelectedVideos.Count];
            this.SelectedVideos.CopyTo(selectedVideos, 0);

            foreach (var video in selectedVideos)
            {
                this.Videos.Remove(video);
                if (video is IAsyncDisposable asyncDisposable)
                {
                    await asyncDisposable.DisposeAsync().ConfigureAwait(true);
                }
                else if (video is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            this.SelectedVideo = default;
        }
    }

    /// <summary>
    /// Saves the current video.
    /// </summary>
    /// <returns>The task.</returns>
    [ICommand]
    public async Task Save()
    {
        if (this.SelectedEditableVideo is not null)
        {
            var video = await this.SelectedEditableVideo.ToVideoAsync().ConfigureAwait(true);
            if (video is ILocalVideo localVideo)
            {
                this.IsSaving = true;
                await Task.Run(() => this.updater.UpdateVideo(localVideo.FileInfo.FullName, video)).ConfigureAwait(true);
                await Refresh(localVideo).ConfigureAwait(true);
                this.IsSaving = false;
            }

            // refresh the local video from the file
            async Task Refresh(ILocalVideo localVideo)
            {
                for (var i = 0; i < this.Videos.Count; i++)
                {
                    if (this.Videos[i] == this.selectedVideo)
                    {
                        var video = await this.ReadVideoAsync(localVideo.FileInfo.FullName).ConfigureAwait(true);
                        if (video is not null)
                        {
                            if (this.Videos[i] is IAsyncDisposable asyncDisposable)
                            {
                                await asyncDisposable.DisposeAsync().ConfigureAwait(true);
                            }
                            else if (this.Videos[i] is IDisposable disposable)
                            {
                                disposable.Dispose();
                            }

                            this.SelectedVideo = this.Videos[i] = video;
                        }

                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Searches for the current video.
    /// </summary>
    /// <returns>The task.</returns>
    [ICommand]
    public async Task Search()
    {
        VideoSearchViewModel? viewModel = this.SelectedEditableVideo switch
        {
            MovieViewModel movie => new MovieSearchViewModel(Ioc.Default.GetRequiredService<IMovieSearch>()) { Name = movie.Name, Year = GetYear(movie.Release) },
            EpisodeViewModel episode => new EpisodeSearchViewModel(Ioc.Default.GetRequiredService<IShowSearch>()) { Name = episode.Show },
            _ => default,
        };

        object? view = viewModel switch
        {
            MovieSearchViewModel movieViewModel => new Views.MovieSearchView(movieViewModel),
            EpisodeSearchViewModel episodeViewModel => new Views.EpisodeSearchView(episodeViewModel),
            _ => default,
        };

        var dialog = new ContentDialog
        {
            Title = "Search",
            PrimaryButtonText = "Save",
            IsSecondaryButtonEnabled = false,
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary,
            Content = view,
            XamlRoot = GetXamlRoot(),
        };

        var result = await dialog.ShowAsync(ContentDialogPlacement.Popup);
        if (result == ContentDialogResult.Primary
            && this.SelectedEditableVideo is not null
            && viewModel?.SelectedVideo is not null)
        {
            Video? videoWithImageSource = viewModel.SelectedVideo switch
            {
                Episode episode => await Models.EpisodeWithImageSource.CreateAsync(episode).ConfigureAwait(true),
                Movie movie => await Models.MovieWithImageSource.CreateAsync(movie).ConfigureAwait(true),
                Video video => await Models.VideoWithImageSource.CreateAsync(video).ConfigureAwait(true),
                _ => default,
            };

            if (videoWithImageSource is not null)
            {
                // apply this to the editable video
                this.SelectedEditableVideo.Update(videoWithImageSource);
            }
        }

        static int? GetYear(DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset?.Year;
        }

        static Microsoft.UI.Xaml.XamlRoot? GetXamlRoot()
        {
            return App.Current?.GetWindow() is Microsoft.UI.Xaml.Window window
                ? window.Content.XamlRoot
                : default;
        }
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (string.Equals(e.PropertyName, nameof(this.selectedVideo), StringComparison.OrdinalIgnoreCase))
        {
            this.SelectedEditableVideo = this.selectedVideo switch
            {
                Models.LocalEpisodeWithImageSource episode => new EpisodeViewModel(episode),
                Models.LocalMovieWithImageSource movie => new MovieViewModel(movie),
                Models.LocalVideoWithImageSource video => new VideoViewModel(video),
                _ => default,
            };
        }

        base.OnPropertyChanged(e);
    }

    private async Task<Video?> ReadVideoAsync(string path) => this.reader.ReadVideo(path) switch
    {
        LocalMovie movie => await Models.LocalMovieWithImageSource.CreateAsync(movie).ConfigureAwait(true),
        LocalEpisode episode => await Models.LocalEpisodeWithImageSource.CreateAsync(episode).ConfigureAwait(true),
        LocalVideo video => await Models.LocalVideoWithImageSource.CreateAsync(video).ConfigureAwait(true),
        _ => default(Video?),
    };
}