﻿// -----------------------------------------------------------------------
// <copyright file="MetadataTags.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

/// <summary>
/// Represents the metadata tags of an MP4 file.
/// </summary>
internal class MetadataTags : IDisposable
{
    private const string WorkCode = "©wrk";
    private bool isArtworkEdited;

    /// <summary>
    /// Prevents a default instance of the <see cref="MetadataTags"/> class from being created.
    /// </summary>
    private MetadataTags()
    {
    }

    /// <summary>
    /// Gets or sets the title of the content contained in this file.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the artist of the content contained in this file.
    /// </summary>
    public string? Artist { get; set; }

    /// <summary>
    /// Gets or sets the album artist of the content contained in this file.
    /// </summary>
    public string? AlbumArtist { get; set; }

    /// <summary>
    /// Gets or sets the album of the content contained in this file.
    /// </summary>
    public string? Album { get; set; }

    /// <summary>
    /// Gets or sets the grouping of the content contained in this file.
    /// </summary>
    public string? Grouping { get; set; }

    /// <summary>
    /// Gets or sets the composer of the content contained in this file.
    /// </summary>
    public string? Composer { get; set; }

    /// <summary>
    /// Gets or sets the comments of the content contained in this file.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the genre of the content contained in this file.
    /// </summary>
    public string? Genre { get; set; }

    /// <summary>
    /// Gets or sets the genre type of the content contained in this file.
    /// </summary>
    public short? GenreType { get; set; }

    /// <summary>
    /// Gets or sets the release date of the content contained in this file.
    /// </summary>
    public string? ReleaseDate { get; set; }

    /// <summary>
    /// Gets or sets the track number of the content contained in this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public short? TrackNumber { get; set; }

    /// <summary>
    /// Gets or sets the total number of tracks of the content contained in this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public short? TotalTracks { get; set; }

    /// <summary>
    /// Gets or sets the disc number of tracks of the content contained in this file.
    /// /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public short? DiscNumber { get; set; }

    /// <summary>
    /// Gets or sets the total number of discs of the content contained in this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public short? TotalDiscs { get; set; }

    /// <summary>
    /// Gets or sets the tempo of the content contained in this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public short? Tempo { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the content contained in this file is part of a compilation.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public bool? IsCompilation { get; set; }

    /// <summary>
    /// Gets or sets the name of the TV show for the content contained in this file.
    /// </summary>
    public string? TVShow { get; set; }

    /// <summary>
    /// Gets or sets the name of the TV network for the content contained in this file.
    /// </summary>
    public string? TVNetwork { get; set; }

    /// <summary>
    /// Gets or sets the episode ID of the content contained in this file.
    /// </summary>
    public string? EpisodeId { get; set; }

    /// <summary>
    /// Gets or sets the season number of the content contained in this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public int? SeasonNumber { get; set; }

    /// <summary>
    /// Gets or sets the episode number of the content contained in this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public int? EpisodeNumber { get; set; }

    /// <summary>
    /// Gets or sets the description of the content contained in this file.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the long description of the content contained in this file.
    /// </summary>
    public string? LongDescription { get; set; }

    /// <summary>
    /// Gets or sets the lyrics of the content contained in this file.
    /// </summary>
    public string? Lyrics { get; set; }

    /// <summary>
    /// Gets or sets the sort name for the content contained in this file.
    /// </summary>
    public string? SortName { get; set; }

    /// <summary>
    /// Gets or sets the sort artist for the content contained in this file.
    /// </summary>
    public string? SortArtist { get; set; }

    /// <summary>
    /// Gets or sets the sort album artist for the content contained in this file.
    /// </summary>
    public string? SortAlbumArtist { get; set; }

    /// <summary>
    /// Gets or sets the sort album for the content contained in this file.
    /// </summary>
    public string? SortAlbum { get; set; }

    /// <summary>
    /// Gets or sets the sort composer for the content contained in this file.
    /// </summary>
    public string? SortComposer { get; set; }

    /// <summary>
    /// Gets or sets the sort TV show name for the content contained in this file.
    /// </summary>
    public string? SortTVShow { get; set; }

    /// <summary>
    /// Gets the count of the artwork contained in this file.
    /// </summary>
    public int ArtworkCount { get; private set; }

    /// <summary>
    /// Gets the format of the artwork contained in this file.
    /// </summary>
    public IImageFormat? ArtworkFormat { get; private set; }

    /// <summary>
    /// Gets or sets the copyright information for the content contained in this file.
    /// </summary>
    public string? Copyright { get; set; }

    /// <summary>
    /// Gets or sets the name of the encoding tool used for the content contained in this file.
    /// </summary>
    public string? EncodingTool { get; set; }

    /// <summary>
    /// Gets or sets the name of the person who encoded the content contained in this file.
    /// </summary>
    public string? EncodedBy { get; set; }

    /// <summary>
    /// Gets or sets the date this file was purchased from a media store.
    /// </summary>
    public string? PurchasedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the content contained in this file is part of a podcast.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public bool? IsPodcast { get; set; }

    /// <summary>
    /// Gets or sets the podcast keywords for the content contained in this file.
    /// </summary>
    public string? Keywords { get; set; }

    /// <summary>
    /// Gets or sets the podcast category for the content contained in this file.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the work for this file.
    /// </summary>
    public string? Work { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the content contained in this file is high-definition video.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public bool? IsHDVideo { get; set; }

    /// <summary>
    /// Gets or sets the type of media for the content contained in this file.
    /// </summary>
    public MediaKind MediaType { get; set; }

    /// <summary>
    /// Gets or sets the content rating for the content contained in this file.
    /// </summary>
    public Mp4ContentRating ContentRating { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the content contained in this file is part of a gapless playback album.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public bool? IsGapless { get; set; }

    /// <summary>
    /// Gets or sets the account used to purchase this file from a media store, such as iTunes.
    /// </summary>
    public string? MediaStoreAccount { get; set; }

    /// <summary>
    /// Gets or sets the type of account used to purchase this file from a media store, such as iTunes.
    /// </summary>
    public MediaStoreAccountKind MediaStoreAccountType { get; set; }

    /// <summary>
    /// Gets or sets the country where this file was purchased from a media store, such as iTunes.
    /// </summary>
    public MediaStoreCountry MediaStoreCountry { get; set; }

    /// <summary>
    /// Gets or sets the media store ID of the of the content contained in this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public int? ContentId { get; set; }

    /// <summary>
    /// Gets or sets the media store ID of the of the artist of the content contained in this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public int? ArtistId { get; set; }

    /// <summary>
    /// Gets or sets the playlist ID of this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public long? PlaylistId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the of the genre of the content contained in this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public int? GenreId { get; set; }

    /// <summary>
    /// Gets or sets the media store ID of the of the composer of the content contained in this file.
    /// May be <see langword="null"/> if the value is not set in the file.
    /// </summary>
    public int? ComposerId { get; set; }

    /// <summary>
    /// Gets or sets the X ID of this file.
    /// </summary>
    public string? Xid { get; set; }

    /// <summary>
    /// Gets or sets the ratings information for the content contained in this file, including source
    /// of the rating and the rating value.
    /// </summary>
    public RatingInfo? RatingInfo { get; set; }

    /// <summary>
    /// Gets or sets the movie information for the content contained in this file, including cast,
    /// directors, producers, and writers.
    /// </summary>
    public MovieInfo? MovieInfo { get; set; }

    /// <summary>
    /// Gets the <see cref="Image"/> used for the artwork in this file.
    /// </summary>
    public Image? Artwork { get; private set; }

    /// <summary>
    /// Sets the artwork.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="format">The format.</param>
    public void SetArtwork(Image? image, IImageFormat? format)
    {
        if (!ImagesEqual(this.Artwork, image))
        {
            this.Artwork = image;
            this.ArtworkFormat = format;
            this.isArtworkEdited = true;
        }
    }

    /// <summary>
    /// Releases all managed and unmanaged resources referenced by this instance.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Reads the tags from the tag.
    /// </summary>
    /// <param name="appleTag">The apple tag.</param>
    /// <returns>A new instance of a <see cref="MetadataTags"/> object containing the values in the metadata tags.</returns>
    internal static MetadataTags Create(TagLib.Mpeg4.AppleTag appleTag)
    {
        var tags = new MetadataTags
        {
            Title = appleTag.Title,
            Artist = appleTag.JoinedPerformers,
            AlbumArtist = appleTag.JoinedAlbumArtists,
            Album = appleTag.Album,
            Grouping = appleTag.Grouping,
            Composer = appleTag.JoinedComposers,
            Comment = appleTag.Comment,
            Genre = appleTag.JoinedGenres,
            Tempo = (short)appleTag.BeatsPerMinute,
            IsCompilation = appleTag.IsCompilation,
            Copyright = appleTag.Copyright,
            EncodingTool = appleTag.GetEncodingTool(),
            EncodedBy = appleTag.GetEncodedBy(),
            ReleaseDate = appleTag.GetReleaseDate(),

            // Tags specific to TV Episodes.
            EpisodeNumber = appleTag.GetEpisodeNumber(),
            SeasonNumber = appleTag.GetSeasonNumber(),
            EpisodeId = appleTag.GetEpisodeId(),
            TVNetwork = appleTag.GetNetwork(),
            TVShow = appleTag.GetShowName(),
            Description = appleTag.Description,
            LongDescription = appleTag.GetLongDescription(),
            Lyrics = appleTag.Lyrics,
            SortName = appleTag.TitleSort,
            SortArtist = appleTag.JoinedPerformersSort,
            SortAlbumArtist = JoinGroup(appleTag.AlbumArtistsSort),
            SortAlbum = appleTag.AlbumSort,
            SortComposer = JoinGroup(appleTag.ComposersSort),
            SortTVShow = appleTag.GetSortShowName(),
            ArtworkCount = appleTag.Pictures.Length,

            IsPodcast = appleTag.GetPodcast(),
            Keywords = appleTag.GetKeywords(),
            Category = appleTag.GetCategory(),
            Work = appleTag.GetWork(),

            IsHDVideo = appleTag.GetHdVideo(),
            MediaType = (MediaKind)appleTag.GetMediaType(),
            ContentRating = (Mp4ContentRating)appleTag.GetContentRating(),
            IsGapless = appleTag.GetGapless(),

            MediaStoreAccount = appleTag.GetMediaStoreAccount(),
            MediaStoreCountry = GetEnumValue(appleTag.GetMediaStoreCountry(), MediaStoreCountry.None),
            MediaStoreAccountType = GetEnumValue(appleTag.GetMediaStoreAccountType(), MediaStoreAccountKind.NotSet),
            ContentId = appleTag.GetContentId(),
            ArtistId = appleTag.GetArtistId(),
            PlaylistId = appleTag.GetPlaylistId(),
            GenreId = appleTag.GetGenreId(),
            ComposerId = appleTag.GetComposerId(),
            Xid = appleTag.GetXId(),
        };

        return tags;

        static T GetEnumValue<T>(int? value, T defaultValue)
            where T : Enum
        {
            return value.HasValue && Enum.IsDefined(typeof(T), value.Value)
                ? (T)Enum.ToObject(typeof(T), value.Value)
                : defaultValue;
        }

        static string? JoinGroup(string[] group)
        {
            return group is { Length: not 0 } g
                ? string.Join("; ", g)
                : default;
        }
    }

    /// <summary>
    /// Reads the tags from the specified file.
    /// </summary>
    /// <param name="fileHandle">The handle to the file from which to read the tags.</param>
    /// <returns>A new instance of a <see cref="MetadataTags"/> object containing the values
    /// in the metadata tags for the file.</returns>
    internal static MetadataTags ReadFromFile(IntPtr fileHandle)
    {
        var tagPtr = NativeMethods.MP4TagsAlloc();
        _ = NativeMethods.MP4TagsFetch(tagPtr, fileHandle);
        var tags = tagPtr.ToStructure<NativeMethods.MP4Tags>();

        var managedTags = new MetadataTags
        {
            Title = StringMarshaller.AnsiToUtf8(tags.name),
            Artist = StringMarshaller.AnsiToUtf8(tags.artist),
            AlbumArtist = StringMarshaller.AnsiToUtf8(tags.albumArtist),
            Album = StringMarshaller.AnsiToUtf8(tags.album),
            Grouping = StringMarshaller.AnsiToUtf8(tags.grouping),
            Composer = StringMarshaller.AnsiToUtf8(tags.composer),
            Comment = StringMarshaller.AnsiToUtf8(tags.comment),
            Genre = StringMarshaller.AnsiToUtf8(tags.genre),
            Tempo = tags.tempo.ReadInt16(),
            IsCompilation = tags.compilation.ReadBoolean(),
            Copyright = StringMarshaller.AnsiToUtf8(tags.copyright),
            EncodingTool = StringMarshaller.AnsiToUtf8(tags.encodingTool),
            EncodedBy = StringMarshaller.AnsiToUtf8(tags.encodedBy),
            ReleaseDate = StringMarshaller.AnsiToUtf8(tags.releaseDate),

            // Tags specific to TV Episodes.
            EpisodeNumber = tags.tvEpisode.ReadInt32(),
            SeasonNumber = tags.tvSeason.ReadInt32(),
            EpisodeId = StringMarshaller.AnsiToUtf8(tags.tvEpisodeID),
            TVNetwork = StringMarshaller.AnsiToUtf8(tags.tvNetwork),
            TVShow = StringMarshaller.AnsiToUtf8(tags.tvShow),
            Description = StringMarshaller.AnsiToUtf8(tags.description),
            LongDescription = StringMarshaller.AnsiToUtf8(tags.longDescription),
            Lyrics = StringMarshaller.AnsiToUtf8(tags.lyrics),
            SortName = StringMarshaller.AnsiToUtf8(tags.sortName),
            SortArtist = StringMarshaller.AnsiToUtf8(tags.sortArtist),
            SortAlbumArtist = StringMarshaller.AnsiToUtf8(tags.sortAlbumArtist),
            SortAlbum = StringMarshaller.AnsiToUtf8(tags.sortAlbum),
            SortComposer = StringMarshaller.AnsiToUtf8(tags.sortComposer),
            SortTVShow = StringMarshaller.AnsiToUtf8(tags.sortTVShow),
            ArtworkCount = tags.artworkCount,

            IsPodcast = tags.podcast.ReadBoolean(),
            Keywords = StringMarshaller.AnsiToUtf8(tags.keywords),
            Category = StringMarshaller.AnsiToUtf8(tags.category),

            IsHDVideo = tags.hdVideo.ReadBoolean(),
            MediaType = tags.mediaType.ReadEnumValue(MediaKind.NotSet),
            ContentRating = tags.contentRating.ReadEnumValue(Mp4ContentRating.NotSet),
            IsGapless = tags.gapless.ReadBoolean(),

            MediaStoreAccount = StringMarshaller.AnsiToUtf8(tags.itunesAccount),
            MediaStoreCountry = tags.iTunesCountry.ReadEnumValue(MediaStoreCountry.None),
            MediaStoreAccountType = tags.iTunesAccountType.ReadEnumValue(MediaStoreAccountKind.NotSet),
            ContentId = tags.contentID.ReadInt32(),
            ArtistId = tags.artistID.ReadInt32(),
            PlaylistId = tags.playlistID.ReadInt32(),
            GenreId = tags.genreID.ReadInt32(),
            ComposerId = tags.composerID.ReadInt32(),
            Xid = StringMarshaller.AnsiToUtf8(tags.xid),
        };

        managedTags.ReadTrackInfo(tags.track);
        managedTags.ReadDiskInfo(tags.disk);
        managedTags.ReadArtwork(tags.artwork);

        NativeMethods.MP4TagsFree(tagPtr);

        managedTags.Work = ReadRawString(fileHandle, WorkCode);
        managedTags.RatingInfo = ReadRawAtom<RatingInfo>(fileHandle);
        managedTags.MovieInfo = ReadRawAtom<MovieInfo>(fileHandle);
        return managedTags;
    }

    /// <summary>
    /// Writes the tags to the specified file.
    /// </summary>
    /// <param name="fileHandle">The handle to the file to which to write the tags.</param>
    internal void WriteToFile(IntPtr fileHandle)
    {
        var tagsPtr = NativeMethods.MP4TagsAlloc();
        _ = NativeMethods.MP4TagsFetch(tagsPtr, fileHandle);
        var tags = tagsPtr.ToStructure<NativeMethods.MP4Tags>();

        SetStringValue(tagsPtr, this.Title, tags.name, NativeMethods.MP4TagsSetName);
        SetStringValue(tagsPtr, this.Artist, tags.artist, NativeMethods.MP4TagsSetArtist);
        SetStringValue(tagsPtr, this.Album, tags.album, NativeMethods.MP4TagsSetAlbum);
        SetStringValue(tagsPtr, this.AlbumArtist, tags.albumArtist, NativeMethods.MP4TagsSetAlbumArtist);
        SetStringValue(tagsPtr, this.Grouping, tags.grouping, NativeMethods.MP4TagsSetGrouping);
        SetStringValue(tagsPtr, this.Composer, tags.composer, NativeMethods.MP4TagsSetComposer);
        SetStringValue(tagsPtr, this.Comment, tags.comment, NativeMethods.MP4TagsSetComments);
        SetStringValue(tagsPtr, this.Genre, tags.genre, NativeMethods.MP4TagsSetGenre);
        SetInt16Value(tagsPtr, this.GenreType, tags.genreType.ReadInt16(), NativeMethods.MP4TagsSetGenreType);
        SetStringValue(tagsPtr, this.ReleaseDate, tags.releaseDate, NativeMethods.MP4TagsSetReleaseDate);
        SetInt16Value(tagsPtr, this.Tempo, tags.tempo.ReadInt16(), NativeMethods.MP4TagsSetTempo);
        SetBoolValue(tagsPtr, this.IsCompilation, tags.compilation.ReadBoolean(), NativeMethods.MP4TagsSetCompilation);
        SetStringValue(tagsPtr, this.TVShow, tags.tvShow, NativeMethods.MP4TagsSetTVShow);
        SetStringValue(tagsPtr, this.TVNetwork, tags.tvNetwork, NativeMethods.MP4TagsSetTVNetwork);
        SetStringValue(tagsPtr, this.EpisodeId, tags.tvEpisodeID, NativeMethods.MP4TagsSetTVEpisodeID);
        SetInt32Value(tagsPtr, this.SeasonNumber, tags.tvSeason.ReadInt32(), NativeMethods.MP4TagsSetTVSeason);
        SetInt32Value(tagsPtr, this.EpisodeNumber, tags.tvEpisode.ReadInt32(), NativeMethods.MP4TagsSetTVEpisode);
        SetStringValue(tagsPtr, this.Description, tags.description, NativeMethods.MP4TagsSetDescription);
        SetStringValue(tagsPtr, this.LongDescription, tags.longDescription, NativeMethods.MP4TagsSetLongDescription);
        SetStringValue(tagsPtr, this.Lyrics, tags.lyrics, NativeMethods.MP4TagsSetLyrics);
        SetStringValue(tagsPtr, this.SortName, tags.sortName, NativeMethods.MP4TagsSetSortName);
        SetStringValue(tagsPtr, this.SortArtist, tags.sortArtist, NativeMethods.MP4TagsSetSortArtist);
        SetStringValue(tagsPtr, this.SortAlbum, tags.sortAlbum, NativeMethods.MP4TagsSetSortAlbum);
        SetStringValue(tagsPtr, this.SortAlbumArtist, tags.sortAlbumArtist, NativeMethods.MP4TagsSetSortAlbumArtist);
        SetStringValue(tagsPtr, this.SortComposer, tags.sortComposer, NativeMethods.MP4TagsSetSortComposer);
        SetStringValue(tagsPtr, this.SortTVShow, tags.sortTVShow, NativeMethods.MP4TagsSetSortTVShow);
        SetStringValue(tagsPtr, this.Copyright, tags.copyright, NativeMethods.MP4TagsSetCopyright);
        SetStringValue(tagsPtr, this.EncodingTool, tags.encodingTool, NativeMethods.MP4TagsSetEncodingTool);
        SetStringValue(tagsPtr, this.EncodedBy, tags.encodedBy, NativeMethods.MP4TagsSetEncodedBy);
        SetStringValue(tagsPtr, this.PurchasedDate, tags.purchasedDate, NativeMethods.MP4TagsSetPurchaseDate);
        SetStringValue(tagsPtr, this.Keywords, tags.keywords, NativeMethods.MP4TagsSetKeywords);
        SetStringValue(tagsPtr, this.Category, tags.category, NativeMethods.MP4TagsSetCategory);
        SetBoolValue(tagsPtr, this.IsHDVideo, tags.hdVideo.ReadBoolean(), NativeMethods.MP4TagsSetHDVideo);
        SetEnumValue(tagsPtr, this.MediaType, tags.mediaType, MediaKind.NotSet, NativeMethods.MP4TagsSetMediaType);
        SetEnumValue(tagsPtr, this.ContentRating, tags.contentRating, Mp4ContentRating.NotSet, NativeMethods.MP4TagsSetContentRating);
        SetBoolValue(tagsPtr, this.IsGapless, tags.gapless.ReadBoolean(), NativeMethods.MP4TagsSetGapless);
        SetStringValue(tagsPtr, this.MediaStoreAccount, tags.itunesAccount, NativeMethods.MP4TagsSetITunesAccount);
        SetEnumValue(tagsPtr, this.MediaStoreAccountType, tags.iTunesAccountType, MediaStoreAccountKind.NotSet, NativeMethods.MP4TagsSetITunesAccountType);
        if (this.MediaStoreCountry != tags.iTunesCountry.ReadEnumValue(MediaStoreCountry.None))
        {
            var countryValue = this.MediaStoreCountry is MediaStoreCountry.None ? null : (int?)this.MediaStoreCountry;
            tagsPtr.WriteInt32(countryValue, NativeMethods.MP4TagsSetITunesCountry);
        }

        SetInt32Value(tagsPtr, this.ContentId, tags.contentID.ReadInt32(), NativeMethods.MP4TagsSetContentID);
        SetInt32Value(tagsPtr, this.ArtistId, tags.artistID.ReadInt32(), NativeMethods.MP4TagsSetArtistID);
        SetInt64Value(tagsPtr, this.PlaylistId, tags.playlistID.ReadInt64(), NativeMethods.MP4TagsSetPlaylistID);
        SetInt32Value(tagsPtr, this.GenreId, tags.genreID.ReadInt32(), NativeMethods.MP4TagsSetGenreID);
        SetInt32Value(tagsPtr, this.ComposerId, tags.composerID.ReadInt32(), NativeMethods.MP4TagsSetComposerID);
        SetStringValue(tagsPtr, this.Xid, tags.xid, NativeMethods.MP4TagsSetXID);

        this.WriteTrackInfo(tagsPtr, tags.track);
        this.WriteDiscInfo(tagsPtr, tags.disk);

        // If the artwork has been edited, there are two possibilities:
        // First we are replacing an existing piece of artwork with another; or
        // second, we are deleting the artwork that already existed.
        if (this.isArtworkEdited)
        {
            if (this.Artwork is not null)
            {
                this.WriteArtwork(tagsPtr);
            }
            else if (this.ArtworkCount is not 0)
            {
                _ = NativeMethods.MP4TagsRemoveArtwork(tagsPtr, 0);
            }
        }

        _ = NativeMethods.MP4TagsStore(tagsPtr, fileHandle);
        NativeMethods.MP4TagsFree(tagsPtr);

        WriteRawString(fileHandle, WorkCode, this.Work);

        var info = ReadRawAtom<RatingInfo>(fileHandle);
        if (!Equals(this.RatingInfo, info))
        {
            WriteRawAtom(fileHandle, this.RatingInfo);
        }

        var movieInfo = ReadRawAtom<MovieInfo>(fileHandle);
        if (!Equals(this.MovieInfo, movieInfo))
        {
            WriteRawAtom(fileHandle, this.MovieInfo);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:Opening parenthesis should be spaced correctly", Justification = "This is correct.")]
        static bool Equals<T>(T? x, T? y)
        {
            return (x, y) switch
            {
                (null, null) => true,
                (null, not null) or (not null, null) => false,
                _ => x.Equals(y),
            };
        }

        static void SetStringValue(IntPtr tags, string? newValue, string? oldValue, Func<IntPtr, string?, bool> setFunc)
        {
            var value = StringMarshaller.Utf8ToAnsi(newValue);
            if (string.Equals(oldValue, value, StringComparison.Ordinal))
            {
                return;
            }

            _ = setFunc(tags, value);
        }

        static void SetBoolValue(IntPtr tagsPtr, bool? newValue, bool? oldValue, Func<IntPtr, IntPtr, bool> setFunc)
        {
            if (newValue != oldValue)
            {
                tagsPtr.WriteBoolean(newValue, setFunc);
            }
        }

        static void SetEnumValue<T>(IntPtr tagsPtr, T newValue, IntPtr oldValue, T defaultValue, Func<IntPtr, IntPtr, bool> setFunc)
            where T : struct, Enum
        {
            if (!newValue.Equals(oldValue.ReadEnumValue<T>(defaultValue)))
            {
                var value = newValue.Equals(defaultValue)
                    ? default(byte?)
                    : (byte)(object)newValue;
                tagsPtr.WriteByte(value, setFunc);
            }
        }

        static void SetInt16Value(IntPtr tagsPtr, short? newValue, short? oldValue, Func<IntPtr, IntPtr, bool> setFunc)
        {
            if (newValue != oldValue)
            {
                tagsPtr.WriteInt16(newValue, setFunc);
            }
        }

        static void SetInt32Value(IntPtr tagsPtr, int? newValue, int? oldValue, Func<IntPtr, IntPtr, bool> setFunc)
        {
            if (newValue != oldValue)
            {
                tagsPtr.WriteInt32(newValue, setFunc);
            }
        }

        static void SetInt64Value(IntPtr tagsPtr, long? newValue, long? oldValue, Func<IntPtr, IntPtr, bool> setFunc)
        {
            if (newValue != oldValue)
            {
                tagsPtr.WriteInt64(newValue, setFunc);
            }
        }
    }

    /// <summary>
    /// Releases all managed and unmanaged resources referenced by this instance.
    /// </summary>
    /// <param name="disposing"><see langword="true"/> to dispose of managed and unmanaged resources;
    /// <see langword="false"/> to dispose of only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.Artwork?.Dispose();
        }
    }

    private static T? ReadRawAtom<T>(IntPtr fileHandle)
        where T : Atom, new()
    {
        // Must use this construct, as generics don't allow constructors with parameters.
        var atom = new T();
        return atom.Initialize(fileHandle) ? atom : null;
    }

    private static void WriteRawAtom<T>(IntPtr fileHandle, T? atom)
        where T : Atom, new()
    {
        // Because Generics don't allow inheritable static members, and we
        // really don't want to resort to reflection, we can create an instance
        // of the appropriate Atom type, only to get the Meaning and Name properties.
        // Passing in the parameters leads to potentially getting the strings wrong.
        // This solution is hacky in the worst possible way; let's think of a better
        // approach.
        var templateAtom = new T();
        var atomMeaning = templateAtom.Meaning;
        var atomName = templateAtom.Name;

        var listPtr = NativeMethods.MP4ItmfGetItemsByMeaning(fileHandle, atomMeaning, atomName);
        if (listPtr != IntPtr.Zero)
        {
            var list = listPtr.ToStructure<NativeMethods.MP4ItmfItemList>();
            for (var i = 0; i < list.size; i++)
            {
                var item = list.elements[i];
                _ = NativeMethods.MP4ItmfRemoveItem(fileHandle, item);
            }

            NativeMethods.MP4ItmfItemListFree(listPtr);

            if (atom is not null)
            {
                var newItemPtr = NativeMethods.MP4ItmfItemAlloc("----", 1);
                var newItem = newItemPtr.ToStructure<NativeMethods.MP4ItmfItem>();
                newItem.mean = atom.Meaning;
                newItem.name = atom.Name;

                var dataBuffer = atom.ToByteArray();
                var data = new NativeMethods.MP4ItmfData
                {
                    typeCode = atom.DataType,
                    valueSize = dataBuffer.Length,
                };

                var dataValuePointer = Marshal.AllocHGlobal(dataBuffer.Length);
                Marshal.Copy(dataBuffer, 0, dataValuePointer, dataBuffer.Length);
                data.value = dataValuePointer;

                var dataPointer = Marshal.AllocHGlobal(Marshal.SizeOf(data));
                Marshal.StructureToPtr(data, dataPointer, fDeleteOld: false);
                newItem.dataList.elements[0] = dataPointer;

                Marshal.StructureToPtr(newItem, newItemPtr, fDeleteOld: false);
                _ = NativeMethods.MP4ItmfAddItem(fileHandle, newItemPtr);

                Marshal.FreeHGlobal(dataPointer);
                Marshal.FreeHGlobal(dataValuePointer);
            }
        }
    }

    private static void WriteRawString(IntPtr fileHandle, string code, string? value)
    {
        var listPtr = NativeMethods.MP4ItmfGetItemsByCode(fileHandle, code);
        if (listPtr != IntPtr.Zero)
        {
            var list = listPtr.ToStructure<NativeMethods.MP4ItmfItemList>();
            for (var i = 0; i < list.size; i++)
            {
                var item = list.elements[i];
                _ = NativeMethods.MP4ItmfRemoveItem(fileHandle, item);
            }

            NativeMethods.MP4ItmfItemListFree(listPtr);

            if (value is not null)
            {
                var newItemPtr = NativeMethods.MP4ItmfItemAlloc(code, 1);
                var newItem = newItemPtr.ToStructure<NativeMethods.MP4ItmfItem>();

                var dataBuffer = System.Text.Encoding.UTF8.GetBytes(value);
                var data = new NativeMethods.MP4ItmfData
                {
                    typeCode = NativeMethods.MP4ItmfBasicType.Utf8,
                    valueSize = dataBuffer.Length,
                };

                var dataValuePointer = Marshal.AllocHGlobal(dataBuffer.Length);
                Marshal.Copy(dataBuffer, 0, dataValuePointer, dataBuffer.Length);
                data.value = dataValuePointer;

                var dataPointer = Marshal.AllocHGlobal(Marshal.SizeOf(data));
                Marshal.StructureToPtr(data, dataPointer, fDeleteOld: false);
                newItem.dataList.elements[0] = dataPointer;

                Marshal.StructureToPtr(newItem, newItemPtr, fDeleteOld: false);
                _ = NativeMethods.MP4ItmfAddItem(fileHandle, newItemPtr);

                Marshal.FreeHGlobal(dataPointer);
                Marshal.FreeHGlobal(dataValuePointer);
            }
        }
    }

    private static string? ReadRawString(IntPtr fileHandle, string code)
    {
        var returnString = default(string);
        var listPtr = NativeMethods.MP4ItmfGetItemsByCode(fileHandle, code);
        if (listPtr != IntPtr.Zero)
        {
            var list = listPtr.ToStructure<NativeMethods.MP4ItmfItemList>();
            if (list.size is 1)
            {
                var itemPointer = list.elements[0];
                var item = itemPointer.ToStructure<NativeMethods.MP4ItmfItem>();
                var dataList = item.dataList;
                for (var j = 0; j < dataList.size; j++)
                {
                    var dataListItemPointer = dataList.elements[j];
                    var data = dataListItemPointer.ToStructure<NativeMethods.MP4ItmfData>();
                    if (data.value.ToByteArray(data.valueSize) is { } dataBuffer)
                    {
                        returnString = System.Text.Encoding.UTF8.GetString(dataBuffer);
                    }
                }
            }

            NativeMethods.MP4ItmfItemListFree(listPtr);
        }

        return returnString;
    }

    private static bool ImagesEqual(Image? first, Image? second) => (first is null && second is null) || (first is not null && second is not null && ImageComparer.Compare(first, second));

    private void ReadArtwork(IntPtr artworkStructurePointer)
    {
        if (artworkStructurePointer == IntPtr.Zero)
        {
            return;
        }

        var artworkStructure = artworkStructurePointer.ToStructure<NativeMethods.MP4TagArtwork>();
        if (artworkStructure.data == IntPtr.Zero)
        {
            return;
        }

        var artworkBuffer = new byte[artworkStructure.size];
        Marshal.Copy(artworkStructure.data, artworkBuffer, 0, artworkStructure.size);
        this.Artwork = Image.Load(artworkBuffer, out var format);
        this.ArtworkFormat = format;
    }

    private void WriteArtwork(IntPtr tagsPtr)
    {
        if (this.Artwork is null)
        {
            return;
        }

        var newArtwork = default(NativeMethods.MP4TagArtwork);

        byte[] artworkBytes;
        using (var stream = new MemoryStream())
        {
            this.Artwork.Save(stream, this.ArtworkFormat);
            artworkBytes = stream.ToArray();
        }

        newArtwork.data = Marshal.AllocHGlobal(artworkBytes.Length);
        newArtwork.size = artworkBytes.Length;
        Marshal.Copy(artworkBytes, 0, newArtwork.data, artworkBytes.Length);

        newArtwork.type = this.ArtworkFormat switch
        {
            SixLabors.ImageSharp.Formats.Bmp.BmpFormat => NativeMethods.MP4TagArtworkType.Bmp,
            SixLabors.ImageSharp.Formats.Jpeg.JpegFormat => NativeMethods.MP4TagArtworkType.Jpeg,
            SixLabors.ImageSharp.Formats.Gif.GifFormat => NativeMethods.MP4TagArtworkType.Gif,
            SixLabors.ImageSharp.Formats.Png.PngFormat => NativeMethods.MP4TagArtworkType.Png,
            _ => NativeMethods.MP4TagArtworkType.Undefined,
        };

        var newArtworkPtr = Marshal.AllocHGlobal(Marshal.SizeOf(newArtwork));
        Marshal.StructureToPtr(newArtwork, newArtworkPtr, fDeleteOld: false);
        _ = this.ArtworkCount is 0
            ? NativeMethods.MP4TagsAddArtwork(tagsPtr, newArtworkPtr)
            : NativeMethods.MP4TagsSetArtwork(tagsPtr, 0, newArtworkPtr);

        Marshal.FreeHGlobal(newArtwork.data);
        Marshal.FreeHGlobal(newArtworkPtr);
    }

    private void ReadDiskInfo(IntPtr diskInfoPointer)
    {
        if (diskInfoPointer == IntPtr.Zero)
        {
            return;
        }

        var diskInfo = diskInfoPointer.ToStructure<NativeMethods.MP4TagDisk>();
        this.DiscNumber = diskInfo.index;
        this.TotalDiscs = diskInfo.total;
    }

    private void WriteDiscInfo(IntPtr tagsPtr, IntPtr discInfoPtr)
    {
        if (this.DiscNumber is { } discNumber && this.TotalDiscs is { } totalDiscs)
        {
            var discInfo = default(NativeMethods.MP4TagDisk);
            if (discInfoPtr != IntPtr.Zero)
            {
                _ = discInfoPtr.ToStructure<NativeMethods.MP4TagDisk>();
            }

            if (discNumber != discInfo.index || totalDiscs != discInfo.total)
            {
                discInfo.index = discNumber;
                discInfo.total = totalDiscs;
                var discPtr = Marshal.AllocHGlobal(Marshal.SizeOf(discInfo));
                Marshal.StructureToPtr(discInfo, discPtr, fDeleteOld: false);
                _ = NativeMethods.MP4TagsSetDisk(tagsPtr, discPtr);
                Marshal.FreeHGlobal(discPtr);
            }
        }
        else
        {
            _ = NativeMethods.MP4TagsSetDisk(tagsPtr, IntPtr.Zero);
        }
    }

    private void ReadTrackInfo(IntPtr trackInfoPointer)
    {
        if (trackInfoPointer == IntPtr.Zero)
        {
            return;
        }

        var trackInfo = trackInfoPointer.ToStructure<NativeMethods.MP4TagTrack>();
        this.TrackNumber = trackInfo.index;
        this.TotalTracks = trackInfo.total;
    }

    private void WriteTrackInfo(IntPtr tagsPtr, IntPtr trackInfoPtr)
    {
        if (this.TrackNumber is { } trackNumber && this.TotalTracks is { } totalTracks)
        {
            var trackInfo = default(NativeMethods.MP4TagTrack);
            if (trackInfoPtr != IntPtr.Zero)
            {
                trackInfo = trackInfoPtr.ToStructure<NativeMethods.MP4TagTrack>();
            }

            if (trackNumber != trackInfo.index || totalTracks != trackInfo.total)
            {
                trackInfo.index = trackNumber;
                trackInfo.total = totalTracks;
                var trackPtr = Marshal.AllocHGlobal(Marshal.SizeOf(trackInfo));
                Marshal.StructureToPtr(trackInfo, trackPtr, fDeleteOld: false);
                _ = NativeMethods.MP4TagsSetTrack(tagsPtr, trackPtr);
                Marshal.FreeHGlobal(trackPtr);
            }
        }
        else
        {
            _ = NativeMethods.MP4TagsSetTrack(tagsPtr, IntPtr.Zero);
        }
    }
}