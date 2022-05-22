// -----------------------------------------------------------------------
// <copyright file="EpisodeWithImageSource.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

/// <summary>
/// A episode with an <see cref="Microsoft.UI.Xaml.Media.ImageSource"/>.
/// </summary>
/// <inheritdoc />
internal record class EpisodeWithImageSource(
    string? Name,
    string? Description,
    IEnumerable<string>? Producers,
    IEnumerable<string>? Directors,
    IEnumerable<string>? Studios,
    IEnumerable<string>? Genre,
    IEnumerable<string>? ScreenWriters,
    IEnumerable<string>? Cast,
    IEnumerable<string>? Composers) : Episode(Name, Description, Producers, Directors, Studios, Genre, ScreenWriters, Cast, Composers), IHasImageSource, IHasSoftwareBitmap
{
    /// <inheritdoc/>
    public Windows.Graphics.Imaging.SoftwareBitmap? SoftwareBitmap { get; init; }

    /// <inheritdoc/>
    public Microsoft.UI.Xaml.Media.ImageSource? ImageSource { get; init; }

    /// <summary>
    /// Creates a <see cref="EpisodeWithImageSource"/> from a <see cref="Episode"/>.
    /// </summary>
    /// <param name="episode">The episode.</param>
    /// <returns>The video with image source.</returns>
    public static async Task<EpisodeWithImageSource> CreateAsync(Episode episode)
    {
        var softwareBitmap = episode switch
        {
            IHasSoftwareBitmap hasSoftwareBitmap => hasSoftwareBitmap.SoftwareBitmap,
            _ => await episode.CreateSoftwareBitmapAsync().ConfigureAwait(true),
        };

        var imageSource = (episode, softwareBitmap) switch
        {
            (IHasImageSource hasImageSource, _) => hasImageSource.ImageSource,
            (_, not null) => await softwareBitmap.CreateImageSourceAsync().ConfigureAwait(true),
            (_, _) => null,
        };

        return new EpisodeWithImageSource(episode.Name, episode.Description, episode.Producers, episode.Directors, episode.Studios, episode.Genre, episode.ScreenWriters, episode.Cast, episode.Composers)
        {
            Release = episode.Release,
            Rating = episode.Rating,
            Tracks = episode.Tracks,
            Show = episode.Show,
            Network = episode.Network,
            Season = episode.Season,
            Number = episode.Number,
            Id = episode.Id,
            Part = episode.Part,
            SoftwareBitmap = softwareBitmap,
            ImageSource = imageSource,
        };
    }
}