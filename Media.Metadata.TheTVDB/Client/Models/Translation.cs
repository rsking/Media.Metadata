// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models {
    /// <summary>
    /// translation record
    /// </summary>
    public class Translation : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The aliases property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Aliases { get; set; }
#nullable restore
#else
        public List<string> Aliases { get; set; }
#endif
        /// <summary>The isAlias property</summary>
        public bool? IsAlias { get; set; }
        /// <summary>The isPrimary property</summary>
        public bool? IsPrimary { get; set; }
        /// <summary>The language property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Language { get; set; }
#nullable restore
#else
        public string Language { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The overview property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Overview { get; set; }
#nullable restore
#else
        public string Overview { get; set; }
#endif
        /// <summary>Only populated for movie translations.  We disallow taglines without a title.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Tagline { get; set; }
#nullable restore
#else
        public string Tagline { get; set; }
#endif
        /// <summary>
        /// Instantiates a new Translation and sets the default values.
        /// </summary>
        public Translation() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Translation CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Translation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"aliases", n => { Aliases = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                {"isAlias", n => { IsAlias = n.GetBoolValue(); } },
                {"isPrimary", n => { IsPrimary = n.GetBoolValue(); } },
                {"language", n => { Language = n.GetStringValue(); } },
                {"name", n => { Name = n.GetStringValue(); } },
                {"overview", n => { Overview = n.GetStringValue(); } },
                {"tagline", n => { Tagline = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfPrimitiveValues<string>("aliases", Aliases);
            writer.WriteBoolValue("isAlias", IsAlias);
            writer.WriteBoolValue("isPrimary", IsPrimary);
            writer.WriteStringValue("language", Language);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("overview", Overview);
            writer.WriteStringValue("tagline", Tagline);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
