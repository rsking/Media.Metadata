// <auto-generated/>
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace ApiSdk.Models
{
    /// <summary>
    /// translation extended record
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    public partial class TranslationExtended : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The alias property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Alias { get; set; }
#nullable restore
#else
        public List<string> Alias { get; set; }
#endif
        /// <summary>The nameTranslations property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Translation>? NameTranslations { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Translation> NameTranslations { get; set; }
#endif
        /// <summary>The overviewTranslations property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Translation>? OverviewTranslations { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Translation> OverviewTranslations { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Models.TranslationExtended"/> and sets the default values.
        /// </summary>
        public TranslationExtended()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Models.TranslationExtended"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::ApiSdk.Models.TranslationExtended CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::ApiSdk.Models.TranslationExtended();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "alias", n => { Alias = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "nameTranslations", n => { NameTranslations = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Translation>(global::ApiSdk.Models.Translation.CreateFromDiscriminatorValue)?.AsList(); } },
                { "overviewTranslations", n => { OverviewTranslations = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Translation>(global::ApiSdk.Models.Translation.CreateFromDiscriminatorValue)?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfPrimitiveValues<string>("alias", Alias);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Translation>("nameTranslations", NameTranslations);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Translation>("overviewTranslations", OverviewTranslations);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
