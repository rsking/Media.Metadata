// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models
{
    /// <summary>
    /// Entity record
    /// </summary>
    public class Entity : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The movieId property</summary>
        public int? MovieId { get; set; }
        /// <summary>The order property</summary>
        public long? Order { get; set; }
        /// <summary>The seriesId property</summary>
        public int? SeriesId { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Models.Entity"/> and sets the default values.
        /// </summary>
        public Entity()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ApiSdk.Models.Entity"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ApiSdk.Models.Entity CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ApiSdk.Models.Entity();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "movieId", n => { MovieId = n.GetIntValue(); } },
                { "order", n => { Order = n.GetLongValue(); } },
                { "seriesId", n => { SeriesId = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("movieId", MovieId);
            writer.WriteLongValue("order", Order);
            writer.WriteIntValue("seriesId", SeriesId);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
