// -----------------------------------------------------------------------
// <copyright file="EditableEpisode.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Models;

/// <summary>
/// An editable <see cref="Episode"/>.
/// </summary>
internal partial class EditableEpisode : EditableVideo
{
    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private string? show;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private string? network;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private int season;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private int number;

    [CommunityToolkit.Mvvm.ComponentModel.ObservableProperty]
    private string? id;

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

    /// <inheritdoc/>
    public override async Task<Video> ToVideoAsync() => new LocalEpisode(this.FileInfo, this.Name, this.Description, this.Producers, this.Directors, this.Studios, this.Genre, this.ScreenWriters, this.Cast, this.Composers)
    {
        Rating = this.Rating.SelectedRating,
        Release = this.Release?.DateTime,
        Show = this.Show,
        Network = this.Network,
        Season = this.Season,
        Number = this.Number,
        Id = this.Id,
        Image = await this.CreateImageAsync().ConfigureAwait(false),
    };

    /// <inheritdoc/>
    public override void Update(Video video)
    {
        base.Update(video);

        if (video is Episode episode)
        {
            this.Show = episode.Show;
            this.Network = episode.Network;
            this.Season = episode.Season;
            this.Number = episode.Number;
            this.Id = episode.Id;
        }
    }
}