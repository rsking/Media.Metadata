// <auto-generated/>
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace ApiSdk.Models
{
    /// <summary>
    /// extended movie record
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    public partial class MovieExtendedRecord : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The aliases property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Alias>? Aliases { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Alias> Aliases { get; set; }
#endif
        /// <summary>The artworks property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.ArtworkBaseRecord>? Artworks { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.ArtworkBaseRecord> Artworks { get; set; }
#endif
        /// <summary>The audioLanguages property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? AudioLanguages { get; set; }
#nullable restore
#else
        public List<string> AudioLanguages { get; set; }
#endif
        /// <summary>The awards property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.AwardBaseRecord>? Awards { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.AwardBaseRecord> Awards { get; set; }
#endif
        /// <summary>The boxOffice property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? BoxOffice { get; set; }
#nullable restore
#else
        public string BoxOffice { get; set; }
#endif
        /// <summary>The boxOfficeUS property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? BoxOfficeUS { get; set; }
#nullable restore
#else
        public string BoxOfficeUS { get; set; }
#endif
        /// <summary>The budget property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Budget { get; set; }
#nullable restore
#else
        public string Budget { get; set; }
#endif
        /// <summary>The characters property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Character>? Characters { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Character> Characters { get; set; }
#endif
        /// <summary>Companies by type record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::ApiSdk.Models.Companies? Companies { get; set; }
#nullable restore
#else
        public global::ApiSdk.Models.Companies Companies { get; set; }
#endif
        /// <summary>The contentRatings property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.ContentRating>? ContentRatings { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.ContentRating> ContentRatings { get; set; }
#endif
        /// <summary>release record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::ApiSdk.Models.Release? FirstRelease { get; set; }
#nullable restore
#else
        public global::ApiSdk.Models.Release FirstRelease { get; set; }
#endif
        /// <summary>The genres property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.GenreBaseRecord>? Genres { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.GenreBaseRecord> Genres { get; set; }
#endif
        /// <summary>The id property</summary>
        public long? Id { get; set; }
        /// <summary>The image property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Image { get; set; }
#nullable restore
#else
        public string Image { get; set; }
#endif
        /// <summary>The inspirations property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Inspiration>? Inspirations { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Inspiration> Inspirations { get; set; }
#endif
        /// <summary>The lastUpdated property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastUpdated { get; set; }
#nullable restore
#else
        public string LastUpdated { get; set; }
#endif
        /// <summary>The lists property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.ListBaseRecord>? Lists { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.ListBaseRecord> Lists { get; set; }
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
        /// <summary>The overviewTranslations property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? OverviewTranslations { get; set; }
#nullable restore
#else
        public List<string> OverviewTranslations { get; set; }
#endif
        /// <summary>The production_countries property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.ProductionCountry>? ProductionCountries { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.ProductionCountry> ProductionCountries { get; set; }
#endif
        /// <summary>The releases property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Release>? Releases { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Release> Releases { get; set; }
#endif
        /// <summary>The remoteIds property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.RemoteID>? RemoteIds { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.RemoteID> RemoteIds { get; set; }
#endif
        /// <summary>The runtime property</summary>
        public int? Runtime { get; set; }
        /// <summary>The score property</summary>
        public double? Score { get; set; }
        /// <summary>The slug property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Slug { get; set; }
#nullable restore
#else
        public string Slug { get; set; }
#endif
        /// <summary>The spoken_languages property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? SpokenLanguages { get; set; }
#nullable restore
#else
        public List<string> SpokenLanguages { get; set; }
#endif
        /// <summary>status record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::ApiSdk.Models.Status? Status { get; set; }
#nullable restore
#else
        public global::ApiSdk.Models.Status Status { get; set; }
#endif
        /// <summary>The studios property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.StudioBaseRecord>? Studios { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.StudioBaseRecord> Studios { get; set; }
#endif
        /// <summary>The subtitleLanguages property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? SubtitleLanguages { get; set; }
#nullable restore
#else
        public List<string> SubtitleLanguages { get; set; }
#endif
        /// <summary>The tagOptions property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.TagOption>? TagOptions { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.TagOption> TagOptions { get; set; }
#endif
        /// <summary>The trailers property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Trailer>? Trailers { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Trailer> Trailers { get; set; }
#endif
        /// <summary>translation extended record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::ApiSdk.Models.TranslationExtended? Translations { get; set; }
#nullable restore
#else
        public global::ApiSdk.Models.TranslationExtended Translations { get; set; }
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
        /// Instantiates a new <see cref="global::ApiSdk.Models.MovieExtendedRecord"/> and sets the default values.
        /// </summary>
        public MovieExtendedRecord()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Models.MovieExtendedRecord"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::ApiSdk.Models.MovieExtendedRecord CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::ApiSdk.Models.MovieExtendedRecord();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "aliases", n => { Aliases = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Alias>(global::ApiSdk.Models.Alias.CreateFromDiscriminatorValue)?.AsList(); } },
                { "artworks", n => { Artworks = n.GetCollectionOfObjectValues<global::ApiSdk.Models.ArtworkBaseRecord>(global::ApiSdk.Models.ArtworkBaseRecord.CreateFromDiscriminatorValue)?.AsList(); } },
                { "audioLanguages", n => { AudioLanguages = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "awards", n => { Awards = n.GetCollectionOfObjectValues<global::ApiSdk.Models.AwardBaseRecord>(global::ApiSdk.Models.AwardBaseRecord.CreateFromDiscriminatorValue)?.AsList(); } },
                { "boxOffice", n => { BoxOffice = n.GetStringValue(); } },
                { "boxOfficeUS", n => { BoxOfficeUS = n.GetStringValue(); } },
                { "budget", n => { Budget = n.GetStringValue(); } },
                { "characters", n => { Characters = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Character>(global::ApiSdk.Models.Character.CreateFromDiscriminatorValue)?.AsList(); } },
                { "companies", n => { Companies = n.GetObjectValue<global::ApiSdk.Models.Companies>(global::ApiSdk.Models.Companies.CreateFromDiscriminatorValue); } },
                { "contentRatings", n => { ContentRatings = n.GetCollectionOfObjectValues<global::ApiSdk.Models.ContentRating>(global::ApiSdk.Models.ContentRating.CreateFromDiscriminatorValue)?.AsList(); } },
                { "first_release", n => { FirstRelease = n.GetObjectValue<global::ApiSdk.Models.Release>(global::ApiSdk.Models.Release.CreateFromDiscriminatorValue); } },
                { "genres", n => { Genres = n.GetCollectionOfObjectValues<global::ApiSdk.Models.GenreBaseRecord>(global::ApiSdk.Models.GenreBaseRecord.CreateFromDiscriminatorValue)?.AsList(); } },
                { "id", n => { Id = n.GetLongValue(); } },
                { "image", n => { Image = n.GetStringValue(); } },
                { "inspirations", n => { Inspirations = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Inspiration>(global::ApiSdk.Models.Inspiration.CreateFromDiscriminatorValue)?.AsList(); } },
                { "lastUpdated", n => { LastUpdated = n.GetStringValue(); } },
                { "lists", n => { Lists = n.GetCollectionOfObjectValues<global::ApiSdk.Models.ListBaseRecord>(global::ApiSdk.Models.ListBaseRecord.CreateFromDiscriminatorValue)?.AsList(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "nameTranslations", n => { NameTranslations = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "originalCountry", n => { OriginalCountry = n.GetStringValue(); } },
                { "originalLanguage", n => { OriginalLanguage = n.GetStringValue(); } },
                { "overviewTranslations", n => { OverviewTranslations = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "production_countries", n => { ProductionCountries = n.GetCollectionOfObjectValues<global::ApiSdk.Models.ProductionCountry>(global::ApiSdk.Models.ProductionCountry.CreateFromDiscriminatorValue)?.AsList(); } },
                { "releases", n => { Releases = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Release>(global::ApiSdk.Models.Release.CreateFromDiscriminatorValue)?.AsList(); } },
                { "remoteIds", n => { RemoteIds = n.GetCollectionOfObjectValues<global::ApiSdk.Models.RemoteID>(global::ApiSdk.Models.RemoteID.CreateFromDiscriminatorValue)?.AsList(); } },
                { "runtime", n => { Runtime = n.GetIntValue(); } },
                { "score", n => { Score = n.GetDoubleValue(); } },
                { "slug", n => { Slug = n.GetStringValue(); } },
                { "spoken_languages", n => { SpokenLanguages = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "status", n => { Status = n.GetObjectValue<global::ApiSdk.Models.Status>(global::ApiSdk.Models.Status.CreateFromDiscriminatorValue); } },
                { "studios", n => { Studios = n.GetCollectionOfObjectValues<global::ApiSdk.Models.StudioBaseRecord>(global::ApiSdk.Models.StudioBaseRecord.CreateFromDiscriminatorValue)?.AsList(); } },
                { "subtitleLanguages", n => { SubtitleLanguages = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "tagOptions", n => { TagOptions = n.GetCollectionOfObjectValues<global::ApiSdk.Models.TagOption>(global::ApiSdk.Models.TagOption.CreateFromDiscriminatorValue)?.AsList(); } },
                { "trailers", n => { Trailers = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Trailer>(global::ApiSdk.Models.Trailer.CreateFromDiscriminatorValue)?.AsList(); } },
                { "translations", n => { Translations = n.GetObjectValue<global::ApiSdk.Models.TranslationExtended>(global::ApiSdk.Models.TranslationExtended.CreateFromDiscriminatorValue); } },
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
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Alias>("aliases", Aliases);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.ArtworkBaseRecord>("artworks", Artworks);
            writer.WriteCollectionOfPrimitiveValues<string>("audioLanguages", AudioLanguages);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.AwardBaseRecord>("awards", Awards);
            writer.WriteStringValue("boxOffice", BoxOffice);
            writer.WriteStringValue("boxOfficeUS", BoxOfficeUS);
            writer.WriteStringValue("budget", Budget);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Character>("characters", Characters);
            writer.WriteObjectValue<global::ApiSdk.Models.Companies>("companies", Companies);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.ContentRating>("contentRatings", ContentRatings);
            writer.WriteObjectValue<global::ApiSdk.Models.Release>("first_release", FirstRelease);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.GenreBaseRecord>("genres", Genres);
            writer.WriteLongValue("id", Id);
            writer.WriteStringValue("image", Image);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Inspiration>("inspirations", Inspirations);
            writer.WriteStringValue("lastUpdated", LastUpdated);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.ListBaseRecord>("lists", Lists);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfPrimitiveValues<string>("nameTranslations", NameTranslations);
            writer.WriteStringValue("originalCountry", OriginalCountry);
            writer.WriteStringValue("originalLanguage", OriginalLanguage);
            writer.WriteCollectionOfPrimitiveValues<string>("overviewTranslations", OverviewTranslations);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.ProductionCountry>("production_countries", ProductionCountries);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Release>("releases", Releases);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.RemoteID>("remoteIds", RemoteIds);
            writer.WriteIntValue("runtime", Runtime);
            writer.WriteDoubleValue("score", Score);
            writer.WriteStringValue("slug", Slug);
            writer.WriteCollectionOfPrimitiveValues<string>("spoken_languages", SpokenLanguages);
            writer.WriteObjectValue<global::ApiSdk.Models.Status>("status", Status);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.StudioBaseRecord>("studios", Studios);
            writer.WriteCollectionOfPrimitiveValues<string>("subtitleLanguages", SubtitleLanguages);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.TagOption>("tagOptions", TagOptions);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Trailer>("trailers", Trailers);
            writer.WriteObjectValue<global::ApiSdk.Models.TranslationExtended>("translations", Translations);
            writer.WriteStringValue("year", Year);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
