// -----------------------------------------------------------------------
// <copyright file="Video.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// A video file.
/// </summary>
/// <param name="Name">Gets the name.</param>
/// <param name="Description">Gets the description.</param>
/// <param name="Producers">Gets the producers.</param>
/// <param name="Directors">Gets the directors.</param>
/// <param name="Studios">Gets the studios.</param>
/// <param name="Genre">Gets the genres.</param>
/// <param name="ScreenWriters">Gets the screen writers.</param>
/// <param name="Cast">Gets the cast.</param>
/// <param name="Composers">Gets the composers.</param>
public abstract record class Video(
    string? Name,
    string? Description,
    IEnumerable<string>? Producers,
    IEnumerable<string>? Directors,
    IEnumerable<string>? Studios,
    IEnumerable<string>? Genre,
    IEnumerable<string>? ScreenWriters,
    IEnumerable<string>? Cast,
    IEnumerable<string>? Composers) : IAsyncDisposable, IDisposable
{
    private const string Separator = ", ";

    private System.Drawing.Image? image;

    private bool imageRetrived;

    private bool disposedValue;

    /// <summary>
    /// Gets the release date.
    /// </summary>
    public DateTime? Release { get; init; }

    /// <summary>
    /// Gets the rating.
    /// </summary>
    public Rating? Rating { get; init; }

    /// <summary>
    /// Gets the image.
    /// </summary>
    public System.Drawing.Image? Image
    {
        get
        {
            if (!this.imageRetrived)
            {
                this.image = GetImage(this.GetImageAsync());
                this.imageRetrived = true;

                static System.Drawing.Image? GetImage(ValueTask<System.Drawing.Image?> task)
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        return task.Result;
                    }

                    if (task.IsFaulted)
                    {
                        // this should throw the exception.
                        return task.GetAwaiter().GetResult();
                    }

                    return task.AsTask().Result;
                }
            }

            return this.image;
        }

        init
        {
            this.image = value;
            this.imageRetrived = true;
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var plist = new PList(new Dictionary<string, object>(StringComparer.Ordinal));
        plist.AddIfNotNullOrEmpty("studio", Separator, this.Studios);
        plist.AddIfNotNull("producers", this.Producers);
        plist.AddIfNotNull("directors", this.Directors);
        plist.AddIfNotNull("cast", this.Cast);
        plist.AddIfNotNull("screenwriters", this.ScreenWriters);
        return plist.ToString();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        // Perform async cleanup.
        await this.DisposeAsyncCore().ConfigureAwait(false);

        // Dispose of unmanaged resources.
        this.Dispose(disposing: false);

        // Suppress finalization.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets the image.
    /// </summary>
    /// <returns>The image.</returns>
    protected virtual ValueTask<System.Drawing.Image?> GetImageAsync() => default;

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
                if (this.imageRetrived && this.image is not null)
                {
                    this.image.Dispose();
                }

                this.image = default;
                this.imageRetrived = false;
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
        if (this.imageRetrived && this.image is not null)
        {
            if (this.image is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync().ConfigureAwait(false);
            }
            else
            {
                this.image.Dispose();
            }
        }

        this.image = default;
        this.imageRetrived = false;
    }
}