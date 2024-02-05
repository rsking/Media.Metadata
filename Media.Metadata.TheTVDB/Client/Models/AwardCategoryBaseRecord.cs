// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models {
    /// <summary>
    /// base award category record
    /// </summary>
    public class AwardCategoryBaseRecord : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The allowCoNominees property</summary>
        public bool? AllowCoNominees { get; set; }
        /// <summary>base award record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public AwardBaseRecord? Award { get; set; }
#nullable restore
#else
        public AwardBaseRecord Award { get; set; }
#endif
        /// <summary>The forMovies property</summary>
        public bool? ForMovies { get; set; }
        /// <summary>The forSeries property</summary>
        public bool? ForSeries { get; set; }
        /// <summary>The id property</summary>
        public long? Id { get; set; }
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>
        /// Instantiates a new AwardCategoryBaseRecord and sets the default values.
        /// </summary>
        public AwardCategoryBaseRecord() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static AwardCategoryBaseRecord CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new AwardCategoryBaseRecord();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"allowCoNominees", n => { AllowCoNominees = n.GetBoolValue(); } },
                {"award", n => { Award = n.GetObjectValue<AwardBaseRecord>(AwardBaseRecord.CreateFromDiscriminatorValue); } },
                {"forMovies", n => { ForMovies = n.GetBoolValue(); } },
                {"forSeries", n => { ForSeries = n.GetBoolValue(); } },
                {"id", n => { Id = n.GetLongValue(); } },
                {"name", n => { Name = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteBoolValue("allowCoNominees", AllowCoNominees);
            writer.WriteObjectValue<AwardBaseRecord>("award", Award);
            writer.WriteBoolValue("forMovies", ForMovies);
            writer.WriteBoolValue("forSeries", ForSeries);
            writer.WriteLongValue("id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
