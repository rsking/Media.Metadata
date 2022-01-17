// -----------------------------------------------------------------------
// <copyright file="VideoViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

/// <summary>
/// The editable video.
/// </summary>
internal partial class VideoViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject, ILocalVideo, Models.IHasImageSource, Models.IHasSoftwareBitmap
{
    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private string? name;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private string? description;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private IList<string>? producers;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private IList<string>? directors;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private IList<string>? studios;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private IList<string>? genre;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private IList<string>? screenWriters;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private IList<string>? cast;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private IList<string>? composers;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private System.DateTimeOffset? release;

    private Microsoft.UI.Xaml.Media.ImageSource? imageSource;

    private Windows.Graphics.Imaging.SoftwareBitmap? softwareBitmap;

    private bool disposedValue;

    /// <summary>
    /// Initialises a new instance of the <see cref="VideoViewModel"/> class.
    /// </summary>
    /// <param name="video">The video.</param>
    public VideoViewModel(Models.LocalVideoWithImageSource video)
        : this(video, video.FileInfo, video.SoftwareBitmap, video.ImageSource)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="VideoViewModel"/> class.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <param name="fileInfo">The file information.</param>
    /// <param name="softwareBitmap">The software bitmap.</param>
    /// <param name="imageSource">The image source.</param>
    protected VideoViewModel(Video video, FileInfo fileInfo, Windows.Graphics.Imaging.SoftwareBitmap? softwareBitmap, Microsoft.UI.Xaml.Media.ImageSource? imageSource)
    {
        this.FileInfo = fileInfo;
        this.Name = video.Name;
        this.Description = video.Description;
        this.Producers = Create(video.Producers);
        this.Directors = Create(video.Directors);
        this.Studios = Create(video.Studios);
        this.Genre = Create(video.Genre);
        this.ScreenWriters = Create(video.ScreenWriters);
        this.Cast = Create(video.Cast);
        this.Composers = Create(video.Composers);
        this.Release = video.Release is null ? default(System.DateTimeOffset?) : new System.DateTimeOffset(video.Release.Value);
        this.Rating = new RatingViewModel(video.Rating);
        this.Tracks = video.Tracks.Select(track => new MediaTrackViewModel(track)).ToArray();
        this.SoftwareBitmap = softwareBitmap;
        this.ImageSource = imageSource;

        static IList<T> Create<T>(IEnumerable<T>? source)
        {
            return source?.ToList() ?? new List<T>();
        }
    }

    /// <summary>
    /// Gets or sets the video type.
    /// </summary>
    public Models.VideoType VideoType { get; set; } = Models.VideoType.NotSet;

    /// <inheritdoc/>
    public FileInfo FileInfo { get; }

    /// <summary>
    /// Gets the rating.
    /// </summary>
    public RatingViewModel Rating { get; init; }

    /// <summary>
    /// Gets the tracks.
    /// </summary>
    public IEnumerable<MediaTrackViewModel> Tracks { get; init; }

    /// <inheritdoc/>
    public Windows.Graphics.Imaging.SoftwareBitmap? SoftwareBitmap
    {
        get => this.softwareBitmap;
        private set => this.SetProperty(ref this.softwareBitmap, value);
    }

    /// <inheritdoc/>
    public Microsoft.UI.Xaml.Media.ImageSource? ImageSource
    {
        get => this.imageSource;
        set => this.SetProperty(ref this.imageSource, value);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.Dispose(disposing: true);
        System.GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        // Perform async cleanup.
        await this.DisposeAsyncCore().ConfigureAwait(false);

        // Dispose of unmanaged resources.
        this.Dispose(disposing: false);

        // Suppress finalization.
        System.GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Converts this to a video.
    /// </summary>
    /// <returns>The video.</returns>
    public virtual async Task<Video> ToVideoAsync()
    {
        var localVideo = new LocalVideo(this.FileInfo, this.Name, this.Description, this.Producers, this.Directors, this.Studios, this.Genre, this.ScreenWriters, this.Cast, this.Composers)
        {
            Rating = this.Rating.SelectedRating,
            Release = this.Release?.DateTime,
            Tracks = this.Tracks.Select(track => track.ToMediaTrack()).ToList(),
            Image = await this.CreateImageAsync().ConfigureAwait(false),
        };

        return this.VideoType switch
        {
            Models.VideoType.Movie => new LocalMovie(localVideo),
            Models.VideoType.TVShow => new LocalEpisode(localVideo),
            _ => localVideo,
        };
    }

    /// <summary>
    /// Updates this instance with the information from the video.
    /// </summary>
    /// <param name="video">The video.</param>
    public virtual void Update(Video video)
    {
        if (video is null)
        {
            return;
        }

        this.Name = video.Name;
        this.Description = video.Description;
        this.Producers = Create(video.Producers);
        this.Directors = Create(video.Directors);
        this.Studios = Create(video.Studios);
        this.Genre = Create(video.Genre);
        this.ScreenWriters = Create(video.ScreenWriters);
        this.Cast = Create(video.Cast);
        this.Composers = Create(video.Composers);
        this.Release = video.Release is null ? default(System.DateTimeOffset?) : new System.DateTimeOffset(video.Release.Value);
        this.Rating.SelectedRating = video.Rating;

        if (video is Models.IHasSoftwareBitmap softwareBitmap)
        {
            this.SoftwareBitmap = softwareBitmap.SoftwareBitmap;
        }

        if (video is Models.IHasImageSource imageSource)
        {
            this.ImageSource = imageSource.ImageSource;
        }

        static IList<T> Create<T>(IEnumerable<T>? source)
        {
            return source?.ToList() ?? new List<T>();
        }
    }

    /// <summary>
    /// Disposes this instance.
    /// </summary>
    /// <param name="disposing">Set to <see langword="true"/> to dispose managed resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                if (this.imageSource is System.IDisposable imageSourceDisposable)
                {
                    imageSourceDisposable.Dispose();
                }

                if (this.softwareBitmap is System.IDisposable softwareBitmapDisposable)
                {
                    softwareBitmapDisposable.Dispose();
                }

                this.imageSource = default;
                this.softwareBitmap = default;
            }

            this.disposedValue = true;
        }
    }

    /// <summary>
    /// Disposes this instance asynchronously.
    /// </summary>
    /// <returns>The value task.</returns>
    protected virtual async ValueTask DisposeAsyncCore()
    {
        await DisposeAsync(this.imageSource).ConfigureAwait(false);
        this.imageSource = default;
        await DisposeAsync(this.softwareBitmap).ConfigureAwait(false);
        this.softwareBitmap = default;

        static async ValueTask DisposeAsync(object? value)
        {
            if (value is System.IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync().ConfigureAwait(false);
            }
            else if (value is System.IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}