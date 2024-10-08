// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace ApiSdk.Models
{
    /// <summary>
    /// extended season record
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    internal partial class SeasonExtendedRecord : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The artwork property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.ArtworkBaseRecord>? Artwork { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.ArtworkBaseRecord> Artwork { get; set; }
#endif
        /// <summary>Companies by type record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::ApiSdk.Models.Companies? Companies { get; set; }
#nullable restore
#else
        public global::ApiSdk.Models.Companies Companies { get; set; }
#endif
        /// <summary>The episodes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.EpisodeBaseRecord>? Episodes { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.EpisodeBaseRecord> Episodes { get; set; }
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
        /// <summary>The imageType property</summary>
        public int? ImageType { get; set; }
        /// <summary>The lastUpdated property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastUpdated { get; set; }
#nullable restore
#else
        public string LastUpdated { get; set; }
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
        /// <summary>The number property</summary>
        public long? Number { get; set; }
        /// <summary>The overviewTranslations property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? OverviewTranslations { get; set; }
#nullable restore
#else
        public List<string> OverviewTranslations { get; set; }
#endif
        /// <summary>The seriesId property</summary>
        public long? SeriesId { get; set; }
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
        /// <summary>The translations property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Translation>? Translations { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Translation> Translations { get; set; }
#endif
        /// <summary>season type record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::ApiSdk.Models.SeasonType? Type { get; set; }
#nullable restore
#else
        public global::ApiSdk.Models.SeasonType Type { get; set; }
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
        /// Instantiates a new <see cref="global::ApiSdk.Models.SeasonExtendedRecord"/> and sets the default values.
        /// </summary>
        public SeasonExtendedRecord()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Models.SeasonExtendedRecord"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::ApiSdk.Models.SeasonExtendedRecord CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::ApiSdk.Models.SeasonExtendedRecord();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "artwork", n => { Artwork = n.GetCollectionOfObjectValues<global::ApiSdk.Models.ArtworkBaseRecord>(global::ApiSdk.Models.ArtworkBaseRecord.CreateFromDiscriminatorValue)?.AsList(); } },
                { "companies", n => { Companies = n.GetObjectValue<global::ApiSdk.Models.Companies>(global::ApiSdk.Models.Companies.CreateFromDiscriminatorValue); } },
                { "episodes", n => { Episodes = n.GetCollectionOfObjectValues<global::ApiSdk.Models.EpisodeBaseRecord>(global::ApiSdk.Models.EpisodeBaseRecord.CreateFromDiscriminatorValue)?.AsList(); } },
                { "id", n => { Id = n.GetIntValue(); } },
                { "image", n => { Image = n.GetStringValue(); } },
                { "imageType", n => { ImageType = n.GetIntValue(); } },
                { "lastUpdated", n => { LastUpdated = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "nameTranslations", n => { NameTranslations = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "number", n => { Number = n.GetLongValue(); } },
                { "overviewTranslations", n => { OverviewTranslations = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "seriesId", n => { SeriesId = n.GetLongValue(); } },
                { "tagOptions", n => { TagOptions = n.GetCollectionOfObjectValues<global::ApiSdk.Models.TagOption>(global::ApiSdk.Models.TagOption.CreateFromDiscriminatorValue)?.AsList(); } },
                { "trailers", n => { Trailers = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Trailer>(global::ApiSdk.Models.Trailer.CreateFromDiscriminatorValue)?.AsList(); } },
                { "translations", n => { Translations = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Translation>(global::ApiSdk.Models.Translation.CreateFromDiscriminatorValue)?.AsList(); } },
                { "type", n => { Type = n.GetObjectValue<global::ApiSdk.Models.SeasonType>(global::ApiSdk.Models.SeasonType.CreateFromDiscriminatorValue); } },
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
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.ArtworkBaseRecord>("artwork", Artwork);
            writer.WriteObjectValue<global::ApiSdk.Models.Companies>("companies", Companies);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.EpisodeBaseRecord>("episodes", Episodes);
            writer.WriteIntValue("id", Id);
            writer.WriteStringValue("image", Image);
            writer.WriteIntValue("imageType", ImageType);
            writer.WriteStringValue("lastUpdated", LastUpdated);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfPrimitiveValues<string>("nameTranslations", NameTranslations);
            writer.WriteLongValue("number", Number);
            writer.WriteCollectionOfPrimitiveValues<string>("overviewTranslations", OverviewTranslations);
            writer.WriteLongValue("seriesId", SeriesId);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.TagOption>("tagOptions", TagOptions);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Trailer>("trailers", Trailers);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Translation>("translations", Translations);
            writer.WriteObjectValue<global::ApiSdk.Models.SeasonType>("type", Type);
            writer.WriteStringValue("year", Year);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
