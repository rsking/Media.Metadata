// -----------------------------------------------------------------------
// <copyright file="EditableMovie.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

using Media.Metadata;

/// <summary>
/// An editable <see cref="Movie"/>.
/// </summary>
internal class EditableMovie : EditableVideo
{
    /// <summary>
    /// Initialises a new instance of the <see cref="EditableMovie"/> class.
    /// </summary>
    /// <param name="movie">The movie.</param>
    public EditableMovie(MovieWithImageSource movie)
        : base(movie, movie.FileInfo, movie.SoftwareBitmap, movie.ImageSource)
    {
    }

    /// <inheritdoc/>
    public override async Task<Video> ToVideoAsync() => new LocalMovie(this.FileInfo, this.Name, this.Description, this.Producers, this.Directors, this.Studios, this.Genre, this.ScreenWriters, this.Cast, this.Composers)
    {
        Rating = this.Rating.SelectedRating,
        Release = this.Release?.DateTime,
        Image = await this.CreateImageAsync().ConfigureAwait(false),
    };
}