// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models
{
    /// <summary>
    /// The extended record for a series. All series airs time like firstAired, lastAired, nextAired, etc. are in US EST for US series, and for all non-US series, the time of the show’s country capital or most populous city. For streaming services, is the official release time. See https://support.thetvdb.com/kb/faq.php?id=29.
    /// </summary>
    public class SeriesExtendedRecord : IAdditionalDataHolder, IParsable
    {
        /// <summary>The abbreviation property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Abbreviation { get; set; }
#nullable restore
#else
        public string Abbreviation { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>A series airs day record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.SeriesAirsDays? AirsDays { get; set; }
#nullable restore
#else
        public ApiSdk.Models.SeriesAirsDays AirsDays { get; set; }
#endif
        /// <summary>The airsTime property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AirsTime { get; set; }
#nullable restore
#else
        public string AirsTime { get; set; }
#endif
        /// <summary>The aliases property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.Alias>? Aliases { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.Alias> Aliases { get; set; }
#endif
        /// <summary>The artworks property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.ArtworkExtendedRecord>? Artworks { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.ArtworkExtendedRecord> Artworks { get; set; }
#endif
        /// <summary>The averageRuntime property</summary>
        public int? AverageRuntime { get; set; }
        /// <summary>The characters property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.Character>? Characters { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.Character> Characters { get; set; }
#endif
        /// <summary>The companies property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.Company>? Companies { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.Company> Companies { get; set; }
#endif
        /// <summary>The contentRatings property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.ContentRating>? ContentRatings { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.ContentRating> ContentRatings { get; set; }
#endif
        /// <summary>The country property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Country { get; set; }
#nullable restore
#else
        public string Country { get; set; }
#endif
        /// <summary>The defaultSeasonType property</summary>
        public long? DefaultSeasonType { get; set; }
        /// <summary>The episodes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.EpisodeBaseRecord>? Episodes { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.EpisodeBaseRecord> Episodes { get; set; }
#endif
        /// <summary>The firstAired property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FirstAired { get; set; }
#nullable restore
#else
        public string FirstAired { get; set; }
#endif
        /// <summary>The genres property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.GenreBaseRecord>? Genres { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.GenreBaseRecord> Genres { get; set; }
#endif
        /// <summary>The id property</summary>
        public int? Id { get; set; }
        /// <summary>The image property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Image { get; set; }
#nullable restore
#else
        public string Image { get; set; }
#endif
        /// <summary>The isOrderRandomized property</summary>
        public bool? IsOrderRandomized { get; set; }
        /// <summary>The lastAired property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastAired { get; set; }
#nullable restore
#else
        public string LastAired { get; set; }
#endif
        /// <summary>The lastUpdated property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastUpdated { get; set; }
#nullable restore
#else
        public string LastUpdated { get; set; }
#endif
        /// <summary>A company record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.Company? LatestNetwork { get; set; }
#nullable restore
#else
        public ApiSdk.Models.Company LatestNetwork { get; set; }
#endif
        /// <summary>The lists property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public UntypedNode? Lists { get; set; }
#nullable restore
#else
        public UntypedNode Lists { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The nameTranslations property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? NameTranslations { get; set; }
#nullable restore
#else
        public List<string> NameTranslations { get; set; }
#endif
        /// <summary>The nextAired property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? NextAired { get; set; }
#nullable restore
#else
        public string NextAired { get; set; }
#endif
        /// <summary>The originalCountry property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OriginalCountry { get; set; }
#nullable restore
#else
        public string OriginalCountry { get; set; }
#endif
        /// <summary>The originalLanguage property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OriginalLanguage { get; set; }
#nullable restore
#else
        public string OriginalLanguage { get; set; }
#endif
        /// <summary>A company record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.Company? OriginalNetwork { get; set; }
#nullable restore
#else
        public ApiSdk.Models.Company OriginalNetwork { get; set; }
#endif
        /// <summary>The overview property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Overview { get; set; }
#nullable restore
#else
        public string Overview { get; set; }
#endif
        /// <summary>The overviewTranslations property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? OverviewTranslations { get; set; }
#nullable restore
#else
        public List<string> OverviewTranslations { get; set; }
#endif
        /// <summary>The remoteIds property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.RemoteID>? RemoteIds { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.RemoteID> RemoteIds { get; set; }
#endif
        /// <summary>The score property</summary>
        public double? Score { get; set; }
        /// <summary>The seasons property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.SeasonBaseRecord>? Seasons { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.SeasonBaseRecord> Seasons { get; set; }
#endif
        /// <summary>The seasonTypes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.SeasonType>? SeasonTypes { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.SeasonType> SeasonTypes { get; set; }
#endif
        /// <summary>The slug property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Slug { get; set; }
#nullable restore
#else
        public string Slug { get; set; }
#endif
        /// <summary>status record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.Status? Status { get; set; }
#nullable restore
#else
        public ApiSdk.Models.Status Status { get; set; }
#endif
        /// <summary>The tags property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.TagOption>? Tags { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.TagOption> Tags { get; set; }
#endif
        /// <summary>The trailers property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.Trailer>? Trailers { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.Trailer> Trailers { get; set; }
#endif
        /// <summary>translation extended record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.TranslationExtended? Translations { get; set; }
#nullable restore
#else
        public ApiSdk.Models.TranslationExtended Translations { get; set; }
#endif
        /// <summary>The year property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Year { get; set; }
#nullable restore
#else
        public string Year { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Models.SeriesExtendedRecord"/> and sets the default values.
        /// </summary>
        public SeriesExtendedRecord()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ApiSdk.Models.SeriesExtendedRecord"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ApiSdk.Models.SeriesExtendedRecord CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ApiSdk.Models.SeriesExtendedRecord();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "abbreviation", n => { Abbreviation = n.GetStringValue(); } },
                { "airsDays", n => { AirsDays = n.GetObjectValue<ApiSdk.Models.SeriesAirsDays>(ApiSdk.Models.SeriesAirsDays.CreateFromDiscriminatorValue); } },
                { "airsTime", n => { AirsTime = n.GetStringValue(); } },
                { "aliases", n => { Aliases = n.GetCollectionOfObjectValues<ApiSdk.Models.Alias>(ApiSdk.Models.Alias.CreateFromDiscriminatorValue)?.ToList(); } },
                { "artworks", n => { Artworks = n.GetCollectionOfObjectValues<ApiSdk.Models.ArtworkExtendedRecord>(ApiSdk.Models.ArtworkExtendedRecord.CreateFromDiscriminatorValue)?.ToList(); } },
                { "averageRuntime", n => { AverageRuntime = n.GetIntValue(); } },
                { "characters", n => { Characters = n.GetCollectionOfObjectValues<ApiSdk.Models.Character>(ApiSdk.Models.Character.CreateFromDiscriminatorValue)?.ToList(); } },
                { "companies", n => { Companies = n.GetCollectionOfObjectValues<ApiSdk.Models.Company>(ApiSdk.Models.Company.CreateFromDiscriminatorValue)?.ToList(); } },
                { "contentRatings", n => { ContentRatings = n.GetCollectionOfObjectValues<ApiSdk.Models.ContentRating>(ApiSdk.Models.ContentRating.CreateFromDiscriminatorValue)?.ToList(); } },
                { "country", n => { Country = n.GetStringValue(); } },
                { "defaultSeasonType", n => { DefaultSeasonType = n.GetLongValue(); } },
                { "episodes", n => { Episodes = n.GetCollectionOfObjectValues<ApiSdk.Models.EpisodeBaseRecord>(ApiSdk.Models.EpisodeBaseRecord.CreateFromDiscriminatorValue)?.ToList(); } },
                { "firstAired", n => { FirstAired = n.GetStringValue(); } },
                { "genres", n => { Genres = n.GetCollectionOfObjectValues<ApiSdk.Models.GenreBaseRecord>(ApiSdk.Models.GenreBaseRecord.CreateFromDiscriminatorValue)?.ToList(); } },
                { "id", n => { Id = n.GetIntValue(); } },
                { "image", n => { Image = n.GetStringValue(); } },
                { "isOrderRandomized", n => { IsOrderRandomized = n.GetBoolValue(); } },
                { "lastAired", n => { LastAired = n.GetStringValue(); } },
                { "lastUpdated", n => { LastUpdated = n.GetStringValue(); } },
                { "latestNetwork", n => { LatestNetwork = n.GetObjectValue<ApiSdk.Models.Company>(ApiSdk.Models.Company.CreateFromDiscriminatorValue); } },
                { "lists", n => { Lists = n.GetObjectValue<UntypedNode>(UntypedNode.CreateFromDiscriminatorValue); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "nameTranslations", n => { NameTranslations = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                { "nextAired", n => { NextAired = n.GetStringValue(); } },
                { "originalCountry", n => { OriginalCountry = n.GetStringValue(); } },
                { "originalLanguage", n => { OriginalLanguage = n.GetStringValue(); } },
                { "originalNetwork", n => { OriginalNetwork = n.GetObjectValue<ApiSdk.Models.Company>(ApiSdk.Models.Company.CreateFromDiscriminatorValue); } },
                { "overview", n => { Overview = n.GetStringValue(); } },
                { "overviewTranslations", n => { OverviewTranslations = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                { "remoteIds", n => { RemoteIds = n.GetCollectionOfObjectValues<ApiSdk.Models.RemoteID>(ApiSdk.Models.RemoteID.CreateFromDiscriminatorValue)?.ToList(); } },
                { "score", n => { Score = n.GetDoubleValue(); } },
                { "seasonTypes", n => { SeasonTypes = n.GetCollectionOfObjectValues<ApiSdk.Models.SeasonType>(ApiSdk.Models.SeasonType.CreateFromDiscriminatorValue)?.ToList(); } },
                { "seasons", n => { Seasons = n.GetCollectionOfObjectValues<ApiSdk.Models.SeasonBaseRecord>(ApiSdk.Models.SeasonBaseRecord.CreateFromDiscriminatorValue)?.ToList(); } },
                { "slug", n => { Slug = n.GetStringValue(); } },
                { "status", n => { Status = n.GetObjectValue<ApiSdk.Models.Status>(ApiSdk.Models.Status.CreateFromDiscriminatorValue); } },
                { "tags", n => { Tags = n.GetCollectionOfObjectValues<ApiSdk.Models.TagOption>(ApiSdk.Models.TagOption.CreateFromDiscriminatorValue)?.ToList(); } },
                { "trailers", n => { Trailers = n.GetCollectionOfObjectValues<ApiSdk.Models.Trailer>(ApiSdk.Models.Trailer.CreateFromDiscriminatorValue)?.ToList(); } },
                { "translations", n => { Translations = n.GetObjectValue<ApiSdk.Models.TranslationExtended>(ApiSdk.Models.TranslationExtended.CreateFromDiscriminatorValue); } },
                { "year", n => { Year = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("abbreviation", Abbreviation);
            writer.WriteObjectValue<ApiSdk.Models.SeriesAirsDays>("airsDays", AirsDays);
            writer.WriteStringValue("airsTime", AirsTime);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.Alias>("aliases", Aliases);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.ArtworkExtendedRecord>("artworks", Artworks);
            writer.WriteIntValue("averageRuntime", AverageRuntime);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.Character>("characters", Characters);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.Company>("companies", Companies);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.ContentRating>("contentRatings", ContentRatings);
            writer.WriteStringValue("country", Country);
            writer.WriteLongValue("defaultSeasonType", DefaultSeasonType);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.EpisodeBaseRecord>("episodes", Episodes);
            writer.WriteStringValue("firstAired", FirstAired);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.GenreBaseRecord>("genres", Genres);
            writer.WriteIntValue("id", Id);
            writer.WriteStringValue("image", Image);
            writer.WriteBoolValue("isOrderRandomized", IsOrderRandomized);
            writer.WriteStringValue("lastAired", LastAired);
            writer.WriteStringValue("lastUpdated", LastUpdated);
            writer.WriteObjectValue<ApiSdk.Models.Company>("latestNetwork", LatestNetwork);
            writer.WriteObjectValue<UntypedNode>("lists", Lists);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfPrimitiveValues<string>("nameTranslations", NameTranslations);
            writer.WriteStringValue("nextAired", NextAired);
            writer.WriteStringValue("originalCountry", OriginalCountry);
            writer.WriteStringValue("originalLanguage", OriginalLanguage);
            writer.WriteObjectValue<ApiSdk.Models.Company>("originalNetwork", OriginalNetwork);
            writer.WriteStringValue("overview", Overview);
            writer.WriteCollectionOfPrimitiveValues<string>("overviewTranslations", OverviewTranslations);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.RemoteID>("remoteIds", RemoteIds);
            writer.WriteDoubleValue("score", Score);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.SeasonBaseRecord>("seasons", Seasons);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.SeasonType>("seasonTypes", SeasonTypes);
            writer.WriteStringValue("slug", Slug);
            writer.WriteObjectValue<ApiSdk.Models.Status>("status", Status);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.TagOption>("tags", Tags);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.Trailer>("trailers", Trailers);
            writer.WriteObjectValue<ApiSdk.Models.TranslationExtended>("translations", Translations);
            writer.WriteStringValue("year", Year);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
