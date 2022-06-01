// -----------------------------------------------------------------------
// <copyright file="Snapshot.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The snapshot.
/// </summary>
internal class Snapshot
{
    private readonly Color[,] buffer;

    /// <summary>
    /// Initialises a new instance of the <see cref="Snapshot"/> class.
    /// </summary>
    /// <param name="height">The height.</param>
    /// <param name="width">The width.</param>
    internal Snapshot(int height, int width) => this.buffer = new Color[height, width];

    /// <summary>
    /// Initialises a new instance of the <see cref="Snapshot"/> class.
    /// </summary>
    protected Snapshot()
        : this(0, 0)
    {
    }

    /// <summary>
    /// Gets the width.
    /// </summary>
    internal int Width => this.buffer.GetLength(1);

    /// <summary>
    /// Gets the height.
    /// </summary>
    internal int Height => this.buffer.GetLength(0);

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <param name="row">The row.</param>
    /// <param name="column">The column.</param>
    /// <returns>The color.</returns>
    internal virtual Color this[int row, int column]
    {
        get => this.buffer[row, column];
        set => this.buffer[row, column] = value;
    }

    /// <summary>
    /// Creates a <see cref="Snapshot"/> from the source image.
    /// </summary>
    /// <param name="sourceImage">The source image.</param>
    /// <returns>The snap shot.</returns>
    internal static Snapshot FromImage(Image sourceImage) => FromImage(sourceImage.CloneAs<SixLabors.ImageSharp.PixelFormats.Rgba32>());

    /// <summary>
    /// Creates a <see cref="Snapshot"/> from the source image.
    /// </summary>
    /// <param name="sourceImage">The source image.</param>
    /// <returns>The snap shot.</returns>
    internal static Snapshot FromImage(Image<SixLabors.ImageSharp.PixelFormats.Rgba32> sourceImage)
    {
        var snapshot = new Snapshot(sourceImage.Height, sourceImage.Width);
        sourceImage.ProcessPixelRows(accessor =>
        {
            for (var i = 0; i < accessor.Height; i++)
            {
                var pixelRow = accessor.GetRowSpan(i);

                for (var j = 0; j < pixelRow.Length; j++)
                {
                    // Get a reference to the pixel at position j
                    snapshot[i, j] = pixelRow[j];
                }
            }
        });

        return snapshot;
    }

    /// <summary>
    /// Converts this instance to an image.
    /// </summary>
    /// <returns>The image.</returns>
    internal Image ToImage()
    {
        return CreateBitmap();

        Image CreateBitmap()
        {
            var image = new Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(this.Width, this.Height);
            image.ProcessPixelRows(accessor =>
            {
                for (var i = 0; i < accessor.Height; i++)
                {
                    var pixelRow = accessor.GetRowSpan(i);

                    for (var j = 0; j < pixelRow.Length; j++)
                    {
                        // Get a reference to the pixel at position j
                        ref var pixel = ref pixelRow[j];
                        pixel = this[i, j];
                    }
                }
            });

            return image;
        }
    }

    /// <summary>
    /// Sets all the pixels.
    /// </summary>
    /// <param name="colorToSet">The color to set.</param>
    internal void SetAllPixels(Color colorToSet)
    {
        for (var i = 0; i < this.Height; i++)
        {
            for (var j = 0; j < this.Width; j++)
            {
                this[i, j] = colorToSet;
            }
        }
    }
}