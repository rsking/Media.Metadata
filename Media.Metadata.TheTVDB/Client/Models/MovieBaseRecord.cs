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
    /// base movie record
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    internal partial class MovieBaseRecord : IAdditionalDataHolder, IParsable
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
        /// <summary>status record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::ApiSdk.Models.Status? Status { get; set; }
#nullable restore
#else
        public global::ApiSdk.Models.Status Status { get; set; }
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
        /// Instantiates a new <see cref="global::ApiSdk.Models.MovieBaseRecord"/> and sets the default values.
        /// </summary>
        public MovieBaseRecord()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Models.MovieBaseRecord"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::ApiSdk.Models.MovieBaseRecord CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::ApiSdk.Models.MovieBaseRecord();
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
                { "id", n => { Id = n.GetLongValue(); } },
                { "image", n => { Image = n.GetStringValue(); } },
                { "lastUpdated", n => { LastUpdated = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "nameTranslations", n => { NameTranslations = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "overviewTranslations", n => { OverviewTranslations = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "runtime", n => { Runtime = n.GetIntValue(); } },
                { "score", n => { Score = n.GetDoubleValue(); } },
                { "slug", n => { Slug = n.GetStringValue(); } },
                { "status", n => { Status = n.GetObjectValue<global::ApiSdk.Models.Status>(global::ApiSdk.Models.Status.CreateFromDiscriminatorValue); } },
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
            writer.WriteLongValue("id", Id);
            writer.WriteStringValue("image", Image);
            writer.WriteStringValue("lastUpdated", LastUpdated);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfPrimitiveValues<string>("nameTranslations", NameTranslations);
            writer.WriteCollectionOfPrimitiveValues<string>("overviewTranslations", OverviewTranslations);
            writer.WriteIntValue("runtime", Runtime);
            writer.WriteDoubleValue("score", Score);
            writer.WriteStringValue("slug", Slug);
            writer.WriteObjectValue<global::ApiSdk.Models.Status>("status", Status);
            writer.WriteStringValue("year", Year);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
