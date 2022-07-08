// -----------------------------------------------------------------------
// <copyright file="VideoViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

/// <summary>
/// The editable video.
/// </summary>
internal partial class VideoViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject, ILocalVideo, IHasImage, Models.IHasImageSource, System.IAsyncDisposable, System.IDisposable
{
    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private string? name;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private string? description;

    private IList<string> producers;

    private IList<string> directors;

    private IList<string> studios;

    private IList<string> genre;

    private IList<string> screenWriters;

    private IList<string> cast;

    private IList<string> composers;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private System.DateTimeOffset? release;

    private ImageSource? imageSource;

    private SixLabors.ImageSharp.Image? image;

    private SixLabors.ImageSharp.Formats.IImageFormat? imageFormat;

    private bool disposedValue;

    /// <summary>
    /// Initialises a new instance of the <see cref="VideoViewModel"/> class.
    /// </summary>
    /// <param name="video">The video.</param>
    public VideoViewModel(Models.LocalVideoWithImageSource video)
        : this(video, video.FileInfo, video.Image, video.ImageFormat, video.ImageSource)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="VideoViewModel"/> class.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <param name="fileInfo">The file information.</param>
    /// <param name="image">The image.</param>
    /// <param name="imageFormat">The image format.</param>
    /// <param name="imageSource">The image source.</param>
    protected VideoViewModel(Video video, FileInfo fileInfo, SixLabors.ImageSharp.Image? image, SixLabors.ImageSharp.Formats.IImageFormat? imageFormat, ImageSource? imageSource)
    {
        this.FileInfo = fileInfo;
        this.Name = video.Name;
        this.Description = video.Description;
        this.producers = CreateList(video.Producers);
        this.directors = CreateList(video.Directors);
        this.studios = CreateList(video.Studios);
        this.genre = CreateList(video.Genre);
        this.screenWriters = CreateList(video.ScreenWriters);
        this.cast = CreateList(video.Cast);
        this.composers = CreateList(video.Composers);
        this.Release = video.Release is null || video.Release.Value.Ticks == default ? default(System.DateTimeOffset?) : new System.DateTimeOffset(video.Release.Value);
        this.Rating = new RatingViewModel(video.Rating);
        this.Tracks = video.Tracks.Select(track => new MediaTrackViewModel(track)).ToArray();
        this.Image = image;
        this.ImageFormat = imageFormat;
        this.ImageSource = imageSource;
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

    /// <summary>
    /// Gets the producers.
    /// </summary>
    public IList<string> Producers { get => this.producers; init => this.SetProperty(ref this.producers, CreateList(value)); }

    /// <summary>
    /// Gets the directors.
    /// </summary>
    public IList<string> Directors { get => this.directors; init => this.SetProperty(ref this.directors, CreateList(value)); }

    /// <summary>
    /// Gets the directors.
    /// </summary>
    public IList<string> Studios { get => this.studios; init => this.SetProperty(ref this.studios, CreateList(value)); }

    /// <summary>
    /// Gets the directors.
    /// </summary>
    public IList<string> Genre { get => this.genre; init => this.SetProperty(ref this.genre, CreateList(value)); }

    /// <summary>
    /// Gets the directors.
    /// </summary>
    public IList<string> ScreenWriters { get => this.screenWriters; init => this.SetProperty(ref this.screenWriters, CreateList(value)); }

    /// <summary>
    /// Gets the directors.
    /// </summary>
    public IList<string> Cast { get => this.cast; init => this.SetProperty(ref this.cast, CreateList(value)); }

    /// <summary>
    /// Gets the directors.
    /// </summary>
    public IList<string> Composers { get => this.composers; init => this.SetProperty(ref this.composers, CreateList(value)); }

    /// <inheritdoc/>
    public ImageSource? ImageSource
    {
        get => this.imageSource;
        private set => this.SetProperty(ref this.imageSource, value);
    }

    /// <inheritdoc/>
    public SixLabors.ImageSharp.Image? Image
    {
        get => this.image;
        set => this.SetProperty(ref this.image, value);
    }

    /// <inheritdoc/>
    public SixLabors.ImageSharp.Formats.IImageFormat? ImageFormat
    {
        get => this.imageFormat;
        set => this.SetProperty(ref this.imageFormat, value);
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
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The video.</returns>
    public virtual Task<Video> ToVideoAsync(CancellationToken cancellationToken = default)
    {
        var localVideo = new LocalVideo(this.FileInfo, this.Name, this.Description, this.Producers, this.Directors, this.Studios, this.Genre, this.ScreenWriters, this.Cast, this.Composers)
        {
            Rating = this.Rating.SelectedRating,
            Release = this.Release?.DateTime,
            Tracks = this.Tracks.Select(track => track.ToMediaTrack()).ToList(),
            Image = this.Image,
            ImageFormat = this.ImageFormat,
        };

        Video returnValue = this.VideoType switch
        {
            Models.VideoType.Movie => new LocalMovie(localVideo),
            Models.VideoType.TVShow => new LocalEpisode(localVideo),
            _ => localVideo,
        };

        return Task.FromResult(returnValue);
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
        this.SetProperty(ref this.producers, CreateList(video.Producers), nameof(this.Producers));
        this.SetProperty(ref this.directors, CreateList(video.Directors), nameof(this.Directors));
        this.SetProperty(ref this.studios, CreateList(video.Studios), nameof(this.Studios));
        this.SetProperty(ref this.genre, CreateList(video.Genre), nameof(this.Genre));
        this.SetProperty(ref this.screenWriters, CreateList(video.ScreenWriters), nameof(this.ScreenWriters));
        this.SetProperty(ref this.cast, CreateList(video.Cast), nameof(this.Cast));
        this.SetProperty(ref this.composers, CreateList(video.Composers), nameof(this.Composers));
        this.Release = video.Release is null ? default(System.DateTimeOffset?) : new System.DateTimeOffset(video.Release.Value);
        this.Rating.SelectedRating = video.Rating;
        this.Image = video.Image;
        this.ImageFormat = video.ImageFormat;

        if (video is Models.IHasImageSource imageSource)
        {
            this.ImageSource = imageSource.ImageSource;
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

                this.imageSource = default;

                if (this.image is System.IDisposable imageDisposable)
                {
                    imageDisposable.Dispose();
                }

                this.image = default;
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
        await DisposeAsync(this.image).ConfigureAwait(false);
        this.image = default;

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

    private static IList<T> CreateList<T>(IEnumerable<T>? source)
    {
        return source switch
        {
            System.Collections.ObjectModel.ObservableCollection<T> observable => observable,
            List<T> list => new System.Collections.ObjectModel.ObservableCollection<T>(list),
            not null => new System.Collections.ObjectModel.ObservableCollection<T>(source),
            _ => new System.Collections.ObjectModel.ObservableCollection<T>(),
        };
    }
}