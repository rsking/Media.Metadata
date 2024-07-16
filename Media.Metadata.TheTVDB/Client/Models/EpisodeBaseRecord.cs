// <auto-generated/>
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace ApiSdk.Models
{
    /// <summary>
    /// base episode record
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    public partial class EpisodeBaseRecord : IAdditionalDataHolder, IParsable
    {
        /// <summary>The absoluteNumber property</summary>
        public int? AbsoluteNumber { get; set; }
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The aired property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Aired { get; set; }
#nullable restore
#else
        public string Aired { get; set; }
#endif
        /// <summary>The airsAfterSeason property</summary>
        public int? AirsAfterSeason { get; set; }
        /// <summary>The airsBeforeEpisode property</summary>
        public int? AirsBeforeEpisode { get; set; }
        /// <summary>The airsBeforeSeason property</summary>
        public int? AirsBeforeSeason { get; set; }
        /// <summary>season, midseason, or series</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FinaleType { get; set; }
#nullable restore
#else
        public string FinaleType { get; set; }
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
        /// <summary>The imageType property</summary>
        public int? ImageType { get; set; }
        /// <summary>The isMovie property</summary>
        public long? IsMovie { get; set; }
        /// <summary>The lastUpdated property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastUpdated { get; set; }
#nullable restore
#else
        public string LastUpdated { get; set; }
#endif
        /// <summary>The linkedMovie property</summary>
        public int? LinkedMovie { get; set; }
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
        public int? Number { get; set; }
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
        /// <summary>The runtime property</summary>
        public int? Runtime { get; set; }
        /// <summary>The seasonName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SeasonName { get; set; }
#nullable restore
#else
        public string SeasonName { get; set; }
#endif
        /// <summary>The seasonNumber property</summary>
        public int? SeasonNumber { get; set; }
        /// <summary>The seasons property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.SeasonBaseRecord>? Seasons { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.SeasonBaseRecord> Seasons { get; set; }
#endif
        /// <summary>The seriesId property</summary>
        public long? SeriesId { get; set; }
        /// <summary>The year property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Year { get; set; }
#nullable restore
#else
        public string Year { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Models.EpisodeBaseRecord"/> and sets the default values.
        /// </summary>
        public EpisodeBaseRecord()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Models.EpisodeBaseRecord"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::ApiSdk.Models.EpisodeBaseRecord CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::ApiSdk.Models.EpisodeBaseRecord();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "absoluteNumber", n => { AbsoluteNumber = n.GetIntValue(); } },
                { "aired", n => { Aired = n.GetStringValue(); } },
                { "airsAfterSeason", n => { AirsAfterSeason = n.GetIntValue(); } },
                { "airsBeforeEpisode", n => { AirsBeforeEpisode = n.GetIntValue(); } },
                { "airsBeforeSeason", n => { AirsBeforeSeason = n.GetIntValue(); } },
                { "finaleType", n => { FinaleType = n.GetStringValue(); } },
                { "id", n => { Id = n.GetLongValue(); } },
                { "image", n => { Image = n.GetStringValue(); } },
                { "imageType", n => { ImageType = n.GetIntValue(); } },
                { "isMovie", n => { IsMovie = n.GetLongValue(); } },
                { "lastUpdated", n => { LastUpdated = n.GetStringValue(); } },
                { "linkedMovie", n => { LinkedMovie = n.GetIntValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "nameTranslations", n => { NameTranslations = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "number", n => { Number = n.GetIntValue(); } },
                { "overview", n => { Overview = n.GetStringValue(); } },
                { "overviewTranslations", n => { OverviewTranslations = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "runtime", n => { Runtime = n.GetIntValue(); } },
                { "seasonName", n => { SeasonName = n.GetStringValue(); } },
                { "seasonNumber", n => { SeasonNumber = n.GetIntValue(); } },
                { "seasons", n => { Seasons = n.GetCollectionOfObjectValues<global::ApiSdk.Models.SeasonBaseRecord>(global::ApiSdk.Models.SeasonBaseRecord.CreateFromDiscriminatorValue)?.AsList(); } },
                { "seriesId", n => { SeriesId = n.GetLongValue(); } },
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
            writer.WriteIntValue("absoluteNumber", AbsoluteNumber);
            writer.WriteStringValue("aired", Aired);
            writer.WriteIntValue("airsAfterSeason", AirsAfterSeason);
            writer.WriteIntValue("airsBeforeEpisode", AirsBeforeEpisode);
            writer.WriteIntValue("airsBeforeSeason", AirsBeforeSeason);
            writer.WriteStringValue("finaleType", FinaleType);
            writer.WriteLongValue("id", Id);
            writer.WriteStringValue("image", Image);
            writer.WriteIntValue("imageType", ImageType);
            writer.WriteLongValue("isMovie", IsMovie);
            writer.WriteStringValue("lastUpdated", LastUpdated);
            writer.WriteIntValue("linkedMovie", LinkedMovie);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfPrimitiveValues<string>("nameTranslations", NameTranslations);
            writer.WriteIntValue("number", Number);
            writer.WriteStringValue("overview", Overview);
            writer.WriteCollectionOfPrimitiveValues<string>("overviewTranslations", OverviewTranslations);
            writer.WriteIntValue("runtime", Runtime);
            writer.WriteStringValue("seasonName", SeasonName);
            writer.WriteIntValue("seasonNumber", SeasonNumber);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.SeasonBaseRecord>("seasons", Seasons);
            writer.WriteLongValue("seriesId", SeriesId);
            writer.WriteStringValue("year", Year);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
