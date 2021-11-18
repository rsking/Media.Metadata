// -----------------------------------------------------------------------
// <copyright file="EditableEpisode.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

/// <summary>
/// An editable <see cref="Episode"/>.
/// </summary>
internal class EditableEpisode : EditableVideo
{
    /// <summary>
    /// Initialises a new instance of the <see cref="EditableEpisode"/> class.
    /// </summary>
    /// <param name="episode">The episode.</param>
    public EditableEpisode(EpisodeWithImageSource episode)
        : base(episode, episode.FileInfo, episode.SoftwareBitmap, episode.ImageSource)
    {
        this.Show = episode.Show;
        this.Network = episode.Network;
        this.Season = episode.Season;
        this.Number = episode.Number;
        this.Id = episode.Id;
    }

    /// <summary>
    /// Gets or sets the show.
    /// </summary>
    public string? Show { get; set; }

    /// <summary>
    /// Gets or sets the network.
    /// </summary>
    public string? Network { get; set; }

    /// <summary>
    /// Gets or sets the season.
    /// </summary>
    public int Season { get; set; }

    /// <summary>
    /// Gets or sets the number.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public override async Task<Video> ToVideoAsync() => new LocalEpisode(this.FileInfo, this.Name, this.Description, this.Producers, this.Directors, this.Studios, this.Genre, this.ScreenWriters, this.Cast, this.Composers)
    {
        Rating = this.Rating,
        Release = this.Release?.DateTime,
        Show = this.Show,
        Network = this.Network,
        Season = this.Season,
        Number = this.Number,
        Id = this.Id,
        Image = await this.CreateImageAsync().ConfigureAwait(false),
    };
}