﻿// -----------------------------------------------------------------------
// <copyright file="MovieWithImageSource.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

/// <summary>
/// A movie with an <see cref="Microsoft.UI.Xaml.Media.ImageSource"/>.
/// </summary>
/// <inheritdoc />
internal record class MovieWithImageSource(
    string? Name,
    string? Description,
    IEnumerable<string>? Producers,
    IEnumerable<string>? Directors,
    IEnumerable<string>? Studios,
    IEnumerable<string>? Genre,
    IEnumerable<string>? ScreenWriters,
    IEnumerable<string>? Cast,
    IEnumerable<string>? Composers) : Movie(Name, Description, Producers, Directors, Studios, Genre, ScreenWriters, Cast, Composers), IHasImageSource, IHasSoftwareBitmap
{
    /// <inheritdoc/>
    public Windows.Graphics.Imaging.SoftwareBitmap? SoftwareBitmap { get; init; }

    /// <inheritdoc/>
    public Microsoft.UI.Xaml.Media.ImageSource? ImageSource { get; init; }

    /// <summary>
    /// Creates a <see cref="MovieWithImageSource"/> from a <see cref="Movie"/>.
    /// </summary>
    /// <param name="movie">The movie.</param>
    /// <returns>The video with image source.</returns>
    public static async Task<MovieWithImageSource> CreateAsync(Movie movie)
    {
        var softwareBitmap = await movie.CreateSoftwareBitmapAsync().ConfigureAwait(true);
        return new MovieWithImageSource(movie.Name, movie.Description, movie.Producers, movie.Directors, movie.Studios, movie.Genre, movie.ScreenWriters, movie.Cast, movie.Composers)
        {
            Release = movie.Release,
            Rating = movie.Rating,
            Tracks = movie.Tracks,
            SoftwareBitmap = softwareBitmap,
            ImageSource = await softwareBitmap.CreateImageSourceAsync().ConfigureAwait(true),
        };
    }
}