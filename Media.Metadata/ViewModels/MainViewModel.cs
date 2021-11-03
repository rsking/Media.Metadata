// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

using System.Windows.Input;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using MvvmDialogs;

/// <summary>
/// The main view model.
/// </summary>
[Microsoft.Toolkit.Mvvm.ComponentModel.ObservableObject]
internal partial class MainViewModel
{
    private readonly IDialogService dialogService;

    public MainViewModel(IDialogService dialogService)
    {
        this.dialogService = dialogService;
    }

    /// <summary>
    /// Gets the videos.
    /// </summary>
    public IList<Video> Videos { get; } = new System.Collections.ObjectModel.ObservableCollection<Video>();

    /// <summary>
    /// Adds videos.
    /// </summary>
    [ICommand]
    public void AddVideos()
    {
        var settings = new MvvmDialogs.FrameworkDialogs.OpenFile.OpenFileDialogSettings
        {
            DefaultExt = "mp4",
            Filter = "MP4 files (*.mp4;*.m4v)|*.mp4;*.m4v",
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = true,
            Title = "Select Video files."
        };

#pragma warning disable S1944 // Inappropriate casts should not be made
        if (this.dialogService.ShowOpenFileDialog((INotifyPropertyChanged)this, settings) == true)

#pragma warning restore S1944 // Inappropriate casts should not be made
        {
            foreach (var fileName in settings.FileNames)
            {
                if (VideoFile.ReadMetadata(fileName) is Video video)
                {
                    this.Videos.Add(video);
                }
            }
        }
    }
}