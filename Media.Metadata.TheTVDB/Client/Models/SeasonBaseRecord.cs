// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models
{
    /// <summary>
    /// season genre record
    /// </summary>
    public class SeasonBaseRecord : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Companies by type record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.Companies? Companies { get; set; }
#nullable restore
#else
        public ApiSdk.Models.Companies Companies { get; set; }
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
        /// <summary>season type record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.SeasonType? Type { get; set; }
#nullable restore
#else
        public ApiSdk.Models.SeasonType Type { get; set; }
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
        /// Instantiates a new <see cref="ApiSdk.Models.SeasonBaseRecord"/> and sets the default values.
        /// </summary>
        public SeasonBaseRecord()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ApiSdk.Models.SeasonBaseRecord"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ApiSdk.Models.SeasonBaseRecord CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ApiSdk.Models.SeasonBaseRecord();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "companies", n => { Companies = n.GetObjectValue<ApiSdk.Models.Companies>(ApiSdk.Models.Companies.CreateFromDiscriminatorValue); } },
                { "id", n => { Id = n.GetIntValue(); } },
                { "image", n => { Image = n.GetStringValue(); } },
                { "imageType", n => { ImageType = n.GetIntValue(); } },
                { "lastUpdated", n => { LastUpdated = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "nameTranslations", n => { NameTranslations = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                { "number", n => { Number = n.GetLongValue(); } },
                { "overviewTranslations", n => { OverviewTranslations = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                { "seriesId", n => { SeriesId = n.GetLongValue(); } },
                { "type", n => { Type = n.GetObjectValue<ApiSdk.Models.SeasonType>(ApiSdk.Models.SeasonType.CreateFromDiscriminatorValue); } },
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
            writer.WriteObjectValue<ApiSdk.Models.Companies>("companies", Companies);
            writer.WriteIntValue("id", Id);
            writer.WriteStringValue("image", Image);
            writer.WriteIntValue("imageType", ImageType);
            writer.WriteStringValue("lastUpdated", LastUpdated);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfPrimitiveValues<string>("nameTranslations", NameTranslations);
            writer.WriteLongValue("number", Number);
            writer.WriteCollectionOfPrimitiveValues<string>("overviewTranslations", OverviewTranslations);
            writer.WriteLongValue("seriesId", SeriesId);
            writer.WriteObjectValue<ApiSdk.Models.SeasonType>("type", Type);
            writer.WriteStringValue("year", Year);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
