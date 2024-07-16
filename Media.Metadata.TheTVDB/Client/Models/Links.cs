// <auto-generated/>
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace ApiSdk.Models
{
    /// <summary>
    /// Links for next, previous and current record
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    public partial class Links : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The next property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Next { get; set; }
#nullable restore
#else
        public string Next { get; set; }
#endif
        /// <summary>The page_size property</summary>
        public int? PageSize { get; set; }
        /// <summary>The prev property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Prev { get; set; }
#nullable restore
#else
        public string Prev { get; set; }
#endif
        /// <summary>The self property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Self { get; set; }
#nullable restore
#else
        public string Self { get; set; }
#endif
        /// <summary>The total_items property</summary>
        public int? TotalItems { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Models.Links"/> and sets the default values.
        /// </summary>
        public Links()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Models.Links"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::ApiSdk.Models.Links CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::ApiSdk.Models.Links();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "next", n => { Next = n.GetStringValue(); } },
                { "page_size", n => { PageSize = n.GetIntValue(); } },
                { "prev", n => { Prev = n.GetStringValue(); } },
                { "self", n => { Self = n.GetStringValue(); } },
                { "total_items", n => { TotalItems = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("next", Next);
            writer.WriteIntValue("page_size", PageSize);
            writer.WriteStringValue("prev", Prev);
            writer.WriteStringValue("self", Self);
            writer.WriteIntValue("total_items", TotalItems);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
