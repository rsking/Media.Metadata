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
    /// Companies by type record
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    internal partial class Companies : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The distributor property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Company>? Distributor { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Company> Distributor { get; set; }
#endif
        /// <summary>The network property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Company>? Network { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Company> Network { get; set; }
#endif
        /// <summary>The production property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Company>? Production { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Company> Production { get; set; }
#endif
        /// <summary>The special_effects property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Company>? SpecialEffects { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Company> SpecialEffects { get; set; }
#endif
        /// <summary>The studio property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::ApiSdk.Models.Company>? Studio { get; set; }
#nullable restore
#else
        public List<global::ApiSdk.Models.Company> Studio { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Models.Companies"/> and sets the default values.
        /// </summary>
        public Companies()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Models.Companies"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::ApiSdk.Models.Companies CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::ApiSdk.Models.Companies();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "distributor", n => { Distributor = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Company>(global::ApiSdk.Models.Company.CreateFromDiscriminatorValue)?.AsList(); } },
                { "network", n => { Network = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Company>(global::ApiSdk.Models.Company.CreateFromDiscriminatorValue)?.AsList(); } },
                { "production", n => { Production = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Company>(global::ApiSdk.Models.Company.CreateFromDiscriminatorValue)?.AsList(); } },
                { "special_effects", n => { SpecialEffects = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Company>(global::ApiSdk.Models.Company.CreateFromDiscriminatorValue)?.AsList(); } },
                { "studio", n => { Studio = n.GetCollectionOfObjectValues<global::ApiSdk.Models.Company>(global::ApiSdk.Models.Company.CreateFromDiscriminatorValue)?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Company>("distributor", Distributor);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Company>("network", Network);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Company>("production", Production);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Company>("special_effects", SpecialEffects);
            writer.WriteCollectionOfObjectValues<global::ApiSdk.Models.Company>("studio", Studio);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
