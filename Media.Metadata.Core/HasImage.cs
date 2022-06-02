// -----------------------------------------------------------------------
// <copyright file="HasImage.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// A resource that has an image.
/// </summary>
public abstract record class HasImage : IHasImage, IAsyncDisposable, IDisposable
{
    private bool imageRetrived;

    private SixLabors.ImageSharp.Formats.IImageFormat? imageFormat;

    private Image? image;

    private bool disposedValue;

    /// <inheritdoc/>
    public Image? Image
    {
        get
        {
            this.EnsureImage();
            return this.image;
        }

        init
        {
            this.image = value;
            this.imageRetrived = true;
        }
    }

    /// <inheritdoc/>
    public SixLabors.ImageSharp.Formats.IImageFormat? ImageFormat
    {
        get
        {
            this.EnsureImage();
            return this.imageFormat;
        }

        init => this.imageFormat = value;
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
    protected virtual ValueTask<(Image Image, SixLabors.ImageSharp.Formats.IImageFormat ImageFormat)> GetImageAsync() => default;

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

    private void EnsureImage()
    {
        if (!this.imageRetrived)
        {
            (this.image, this.imageFormat) = GetImage(this.GetImageAsync());
            this.imageRetrived = true;

            static T GetImage<T>(ValueTask<T> task)
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
    }
}