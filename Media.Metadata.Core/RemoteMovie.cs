// -----------------------------------------------------------------------
// <copyright file="RemoteMovie.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// Represents a remote movie.
/// </summary>
/// <inheritdoc />
public record class RemoteMovie(
    string? Name,
    string? Description,
    IEnumerable<string>? Producers,
    IEnumerable<string>? Directors,
    IEnumerable<string>? Studios,
    IEnumerable<string>? Genre,
    IEnumerable<string>? ScreenWriters,
    IEnumerable<string>? Cast,
    IEnumerable<string>? Composers) : Movie(Name, Description, Producers, Directors, Studios, Genre, ScreenWriters, Cast, Composers)
{
    /// <summary>
    /// Gets the image URI.
    /// </summary>
    public Uri? ImageUri { get; init; }

    /// <inheritdoc />
    protected override async ValueTask<System.Drawing.Image?> GetImageAsync()
    {
        if (this.ImageUri is null)
        {
            return default;
        }

        using var client = new HttpClient();
        using var stream = await client.GetStreamAsync(this.ImageUri).ConfigureAwait(false);
        var image = System.Drawing.Image.FromStream(stream);
        return image;
    }
}