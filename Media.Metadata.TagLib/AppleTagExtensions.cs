﻿// -----------------------------------------------------------------------
// <copyright file="AppleTagExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using TagLib;

/// <summary>
/// Extensions for <see cref="TagLib.Mpeg4.AppleTag"/>.
/// </summary>
public static class AppleTagExtensions
{
    // general
    private static readonly ReadOnlyByteVector EncodingTool = FixId("too");
    private static readonly ReadOnlyByteVector EncodedBy = FixId("enc");
    private static readonly ReadOnlyByteVector ReleaseDate = FixId("day");
    private static readonly ReadOnlyByteVector LongDescription = "ldes";

    // TV shows
    private static readonly ReadOnlyByteVector TvShow = "tvsh";
    private static readonly ReadOnlyByteVector TvNetwork = "tvnn";
    private static readonly ReadOnlyByteVector TvEpisodeId = "tven";
    private static readonly ReadOnlyByteVector TvSeason = "tvsn";
    private static readonly ReadOnlyByteVector TvEpisode = "tves";

    // sorting
    private static readonly ReadOnlyByteVector SortTvShow = "sosn";

    // misc
    private static readonly ReadOnlyByteVector Podcast = "pcst";
    private static readonly ReadOnlyByteVector Keywords = "keyw";
    private static readonly ReadOnlyByteVector Category = "catg";

    // video
    private static readonly ReadOnlyByteVector HdVideo = "hdvd";
    private static readonly ReadOnlyByteVector MediaType = "stik";
    private static readonly ReadOnlyByteVector ContentRating = "rtng";
    private static readonly ReadOnlyByteVector Gapless = "pgap";

    // iTunes account
    private static readonly ReadOnlyByteVector ITunesAccount = "apID";
    private static readonly ReadOnlyByteVector ITunesAccountType = "akID";
    private static readonly ReadOnlyByteVector ITunesCountry = "sfID";

    // IDs
    private static readonly ReadOnlyByteVector ContentId = "cnID";
    private static readonly ReadOnlyByteVector ArtistId = "atID";
    private static readonly ReadOnlyByteVector PlaylistId = "plID";
    private static readonly ReadOnlyByteVector GenreId = "geID";
    private static readonly ReadOnlyByteVector ComposerId = "cmID";
    private static readonly ReadOnlyByteVector XId = "xid ";

    /// <summary>
    /// Gets the episode number.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The episode number.</returns>
    public static int? GetEpisodeNumber(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetInt32OrDefault(TvEpisode);

    /// <summary>
    /// Gets the release date.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The release date.</returns>
    public static string? GetReleaseDate(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(ReleaseDate);

    /// <summary>
    /// Gets the show name.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The show name.</returns>
    public static string? GetShowName(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(TvShow);

    /// <summary>
    /// Gets the network.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The network.</returns>
    public static string? GetNetwork(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(TvNetwork);

    /// <summary>
    /// Gets the episode ID.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The episode ID.</returns>
    public static string? GetEpisodeId(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(TvEpisodeId);

    /// <summary>
    /// Gets the season number.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The season number.</returns>
    public static int? GetSeasonNumber(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetInt32OrDefault(TvSeason);

    /// <summary>
    /// Gets the sort show name.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The sort show name.</returns>
    public static string? GetSortShowName(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(SortTvShow);

    /// <summary>
    /// Gets the keywords.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The keywords.</returns>
    public static string? GetKeywords(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(Keywords);

    /// <summary>
    /// Gets the category.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The category.</returns>
    public static string? GetCategory(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(Category);

    /// <summary>
    /// Gets the encoding tool.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The encoding tool.</returns>
    public static string? GetEncodingTool(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(EncodingTool);

    /// <summary>
    /// Gets the encoded by.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The encoded by.</returns>
    public static string? GetEncodedBy(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(EncodedBy);

    /// <summary>
    /// Gets the media type.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The media type.</returns>
    public static MediaType GetMediaType(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetByteOrDefault(MediaType) is byte mediaType ? (MediaType)mediaType : Metadata.MediaType.NotSet;

    /// <summary>
    /// Gets the content rating.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The content rating.</returns>
    public static ContentRating GetContentRating(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetByteOrDefault(ContentRating) is byte contentRating ? (ContentRating)contentRating : Metadata.ContentRating.NotSet;

    /// <summary>
    /// Gets the HD video.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The HD video.</returns>
    public static bool? GetHdVideo(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetBoolOrDefault(HdVideo);

    /// <summary>
    /// Gets the gapless.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The gapless.</returns>
    public static bool? GetGapless(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetBoolOrDefault(Gapless);

    /// <summary>
    /// Gets the podcast.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The podcast.</returns>
    public static bool? GetPodcast(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetBoolOrDefault(Podcast);

    /// <summary>
    /// Gets the media store account.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The media store account.</returns>
    public static string? GetMediaStoreAccount(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(ITunesAccount);

    /// <summary>
    /// Gets the media store country.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The media store country.</returns>
    public static int? GetMediaStoreCountry(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetInt32OrDefault(ITunesCountry);

    /// <summary>
    /// Gets the media store account type.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The media store account type.</returns>
    public static int? GetMediaStoreAccountType(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetInt32OrDefault(ITunesAccountType);

    /// <summary>
    /// Gets the content ID.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The content ID.</returns>
    public static int? GetContentId(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetInt32OrDefault(ContentId);

    /// <summary>
    /// Gets the artist ID.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The artist ID.</returns>
    public static int? GetArtistId(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetInt32OrDefault(ArtistId);

    /// <summary>
    /// Gets the playlist ID.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The playlist ID.</returns>
    public static int? GetPlaylistId(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetInt32OrDefault(PlaylistId);

    /// <summary>
    /// Gets the genre ID.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The genre ID.</returns>
    public static int? GetGenreId(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetInt32OrDefault(GenreId);

    /// <summary>
    /// Gets the composer ID.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The composer ID.</returns>
    public static int? GetComposerId(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetInt32OrDefault(ComposerId);

    /// <summary>
    /// Gets the X ID.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The X ID.</returns>
    public static string? GetXId(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(XId);

    /// <summary>
    /// Gets the long description.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>The long description.</returns>
    public static string? GetLongDescription(this TagLib.Mpeg4.AppleTag appleTag) => appleTag.GetJoinedText(LongDescription);

    private static byte? GetByteOrDefault(this TagLib.Mpeg4.AppleTag appleTag, ReadOnlyByteVector type) => appleTag.DataBoxes(type).FirstOrDefault(item => item.Data.Count == 1) is TagLib.Mpeg4.AppleDataBox item
        ? item.Data.Data[0]
        : default(byte?);

    private static int? GetInt32OrDefault(this TagLib.Mpeg4.AppleTag appleTag, ReadOnlyByteVector type) => appleTag.DataBoxes(type).FirstOrDefault(item => item.Data.Count == 4) is TagLib.Mpeg4.AppleDataBox item
        ? (int)GetUInt32(item.Data.Data)
        : default(int?);

    private static bool? GetBoolOrDefault(this TagLib.Mpeg4.AppleTag appleTag, ReadOnlyByteVector type) => appleTag.GetInt32OrDefault(type) is int value
        ? value > 0
        : default(bool?);

    private static string? GetJoinedText(this TagLib.Mpeg4.AppleTag appleTag, ByteVector byteVector) => appleTag.GetText(byteVector) switch
    {
        string[] { Length: 1 } value => value[0],
        string[] { Length: > 1 } values => string.Join("; ", values),
        _ => default,
    };

    private static uint GetUInt32(byte[] data)
    {
        var bytes = BitConverter.IsLittleEndian
            ? SwitchBytes(data)
            : data;

        return BitConverter.ToUInt32(bytes, 0);

        static byte[] SwitchBytes(byte[] data)
        {
            return new[] { data[3], data[2], data[1], data[0] };
        }
    }

    private static ReadOnlyByteVector FixId(ByteVector id) => id switch
    {
        ReadOnlyByteVector { Count: 4 } roid => roid,
        { Count: 4 } => new ReadOnlyByteVector(id),
        { Count: 3 } => new ReadOnlyByteVector(0xa9, id[0], id[1], id[2]),
        _ => throw new InvalidCastException(),
    };
}