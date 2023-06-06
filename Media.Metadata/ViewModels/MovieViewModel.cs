// -----------------------------------------------------------------------
// <copyright file="MovieViewModel.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.ViewModels;

/// <summary>
/// An editable <see cref="Movie"/>.
/// </summary>
internal class MovieViewModel : VideoViewModel
{
    /// <summary>
    /// Initialises a new instance of the <see cref="MovieViewModel"/> class.
    /// </summary>
    /// <param name="movie">The movie.</param>
    public MovieViewModel(Models.LocalMovieWithImageSource movie)
        : base(movie, movie.FileInfo, movie.Image, movie.ImageFormat, movie.ImageSource)
    {
    }

    /// <inheritdoc/>
    public override Task<Video> ToVideoAsync(CancellationToken cancellationToken = default) => Task.FromResult<Video>(
        new LocalMovie(this.FileInfo, this.Name, this.Description, this.Producers, this.Directors, this.Studios, this.Genre, this.ScreenWriters, this.Cast, this.Composers)
        {
            Rating = this.Rating.SelectedRating,
            Release = this.Release?.DateTime,
            Tracks = this.Tracks.Select(track => track.ToMediaTrack()).ToList(),
            Image = this.Image,
            ImageFormat = this.ImageFormat,
        });
}