// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

using System;
using Microsoft.Toolkit.Mvvm.Input;

/// <summary>
/// The main view model.
/// </summary>
[Microsoft.Toolkit.Mvvm.ComponentModel.ObservableObject]
internal partial class MainViewModel
{
    [Microsoft.Toolkit.Mvvm.ComponentModel.ObservableProperty]
    private Video? selectedVideo;

    /// <summary>
    /// Gets the videos.
    /// </summary>
    public IList<Video> Videos { get; } = new System.Collections.ObjectModel.ObservableCollection<Video>();

    /// <summary>
    /// Adds videos.
    /// </summary>
    /// <returns>The task.</returns>
    [ICommand]
    public async Task AddVideos()
    {
        // Open a text file.
        var picker = new Windows.Storage.Pickers.FileOpenPicker()
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
                if (VideoFile.ReadMetadata(file.Path) is Video video)
                {
                    this.Videos.Add(await VideoWithImageSource.CreateAsync(video).ConfigureAwait(true));
                }
            }
        }
    }
}