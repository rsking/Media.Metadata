// -----------------------------------------------------------------------
// <copyright file="EditableVideo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

/// <summary>
/// The editable video.
/// </summary>
internal class EditableVideo : ILocalVideo, IHasImageSource, IHasSoftwareBitmap
{
    /// <summary>
    /// Initialises a new instance of the <see cref="EditableVideo"/> class.
    /// </summary>
    /// <param name="video">The video.</param>
    public EditableVideo(VideoWithImageSource video)
        : this(video, video.FileInfo, video.SoftwareBitmap, video.ImageSource)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="EditableVideo"/> class.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <param name="fileInfo">The file information.</param>
    /// <param name="softwareBitmap">The software bitmap.</param>
    /// <param name="imageSource">The image source.</param>
    protected EditableVideo(Video video, FileInfo fileInfo, Windows.Graphics.Imaging.SoftwareBitmap? softwareBitmap, Microsoft.UI.Xaml.Media.ImageSource? imageSource)
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
        this.Rating = video.Rating;
        this.SoftwareBitmap = softwareBitmap;
        this.ImageSource = imageSource;

        static IList<T> Create<T>(IEnumerable<T>? source)
        {
            return source?.ToList() ?? new List<T>();
        }
    }

    /// <inheritdoc/>
    public FileInfo FileInfo { get; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the producers.
    /// </summary>
    public IList<string>? Producers { get; set; }

    /// <summary>
    /// Gets or sets the directors.
    /// </summary>
    public IList<string>? Directors { get; set; }

    /// <summary>
    /// Gets or sets the studios.
    /// </summary>
    public IList<string>? Studios { get; set; }

    /// <summary>
    /// Gets or sets the genre.
    /// </summary>
    public IList<string>? Genre { get; set; }

    /// <summary>
    /// Gets or sets the screen writers.
    /// </summary>
    public IList<string>? ScreenWriters { get; set; }

    /// <summary>
    /// Gets or sets the cast.
    /// </summary>
    public IList<string>? Cast { get; set; }

    /// <summary>
    /// Gets or sets the composers.
    /// </summary>
    public IList<string>? Composers { get; set; }

    /// <summary>
    /// Gets or sets the release date.
    /// </summary>
    public System.DateTimeOffset? Release { get; set; }

    /// <summary>
    /// Gets or sets the rating.
    /// </summary>
    public Rating? Rating { get; set; }

    /// <inheritdoc/>
    public Windows.Graphics.Imaging.SoftwareBitmap? SoftwareBitmap { get; init; }

    /// <inheritdoc/>
    public Microsoft.UI.Xaml.Media.ImageSource? ImageSource { get; set; }

    /// <summary>
    /// Converts this to a video.
    /// </summary>
    /// <returns>The video.</returns>
    public virtual async Task<Video> ToVideoAsync() => new LocalVideo(this.FileInfo, this.Name, this.Description, this.Producers, this.Directors, this.Studios, this.Genre, this.ScreenWriters, this.Cast, this.Composers)
    {
        Rating = this.Rating,
        Release = this.Release?.DateTime,
        Image = await this.CreateImageAsync().ConfigureAwait(false),
    };
}