// <auto-generated/>
#pragma warning disable CS0618
using ApiSdk.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace ApiSdk.Series.Item.Episodes.Item.Item
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    #pragma warning disable CS1591
    internal partial class WithLangGetResponse_data : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The base record for a series. All series airs time like firstAired, lastAired, nextAired, etc. are in US EST for US series, and for all non-US series, the time of the show’s country capital or most populous city. For streaming services, is the official release time. See https://support.thetvdb.com/kb/faq.php?id=29.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::ApiSdk.Models.SeriesBaseRecord? Series { get; set; }
#nullable restore
#else
        public global::ApiSdk.Models.SeriesBaseRecord Series { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangGetResponse_data"/> and sets the default values.
        /// </summary>
        public WithLangGetResponse_data()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangGetResponse_data"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangGetResponse_data CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangGetResponse_data();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "series", n => { Series = n.GetObjectValue<global::ApiSdk.Models.SeriesBaseRecord>(global::ApiSdk.Models.SeriesBaseRecord.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::ApiSdk.Models.SeriesBaseRecord>("series", Series);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
