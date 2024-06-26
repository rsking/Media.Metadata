// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models
{
    /// <summary>
    /// extended people record
    /// </summary>
    public class PeopleExtendedRecord : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The aliases property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.Alias>? Aliases { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.Alias> Aliases { get; set; }
#endif
        /// <summary>The awards property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.AwardBaseRecord>? Awards { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.AwardBaseRecord> Awards { get; set; }
#endif
        /// <summary>The biographies property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.Biography>? Biographies { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.Biography> Biographies { get; set; }
#endif
        /// <summary>The birth property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Birth { get; set; }
#nullable restore
#else
        public string Birth { get; set; }
#endif
        /// <summary>The birthPlace property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? BirthPlace { get; set; }
#nullable restore
#else
        public string BirthPlace { get; set; }
#endif
        /// <summary>The characters property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.Character>? Characters { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.Character> Characters { get; set; }
#endif
        /// <summary>The death property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Death { get; set; }
#nullable restore
#else
        public string Death { get; set; }
#endif
        /// <summary>The gender property</summary>
        public int? Gender { get; set; }
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
        /// <summary>The races property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.Race>? Races { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.Race> Races { get; set; }
#endif
        /// <summary>The remoteIds property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.RemoteID>? RemoteIds { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.RemoteID> RemoteIds { get; set; }
#endif
        /// <summary>The score property</summary>
        public long? Score { get; set; }
        /// <summary>The slug property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Slug { get; set; }
#nullable restore
#else
        public string Slug { get; set; }
#endif
        /// <summary>The tagOptions property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.TagOption>? TagOptions { get; set; }
#nullable restore
#else
        public List<ApiSdk.Models.TagOption> TagOptions { get; set; }
#endif
        /// <summary>translation extended record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.TranslationExtended? Translations { get; set; }
#nullable restore
#else
        public ApiSdk.Models.TranslationExtended Translations { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Models.PeopleExtendedRecord"/> and sets the default values.
        /// </summary>
        public PeopleExtendedRecord()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ApiSdk.Models.PeopleExtendedRecord"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ApiSdk.Models.PeopleExtendedRecord CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ApiSdk.Models.PeopleExtendedRecord();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "aliases", n => { Aliases = n.GetCollectionOfObjectValues<ApiSdk.Models.Alias>(ApiSdk.Models.Alias.CreateFromDiscriminatorValue)?.ToList(); } },
                { "awards", n => { Awards = n.GetCollectionOfObjectValues<ApiSdk.Models.AwardBaseRecord>(ApiSdk.Models.AwardBaseRecord.CreateFromDiscriminatorValue)?.ToList(); } },
                { "biographies", n => { Biographies = n.GetCollectionOfObjectValues<ApiSdk.Models.Biography>(ApiSdk.Models.Biography.CreateFromDiscriminatorValue)?.ToList(); } },
                { "birth", n => { Birth = n.GetStringValue(); } },
                { "birthPlace", n => { BirthPlace = n.GetStringValue(); } },
                { "characters", n => { Characters = n.GetCollectionOfObjectValues<ApiSdk.Models.Character>(ApiSdk.Models.Character.CreateFromDiscriminatorValue)?.ToList(); } },
                { "death", n => { Death = n.GetStringValue(); } },
                { "gender", n => { Gender = n.GetIntValue(); } },
                { "id", n => { Id = n.GetLongValue(); } },
                { "image", n => { Image = n.GetStringValue(); } },
                { "lastUpdated", n => { LastUpdated = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "nameTranslations", n => { NameTranslations = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                { "overviewTranslations", n => { OverviewTranslations = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                { "races", n => { Races = n.GetCollectionOfObjectValues<ApiSdk.Models.Race>(ApiSdk.Models.Race.CreateFromDiscriminatorValue)?.ToList(); } },
                { "remoteIds", n => { RemoteIds = n.GetCollectionOfObjectValues<ApiSdk.Models.RemoteID>(ApiSdk.Models.RemoteID.CreateFromDiscriminatorValue)?.ToList(); } },
                { "score", n => { Score = n.GetLongValue(); } },
                { "slug", n => { Slug = n.GetStringValue(); } },
                { "tagOptions", n => { TagOptions = n.GetCollectionOfObjectValues<ApiSdk.Models.TagOption>(ApiSdk.Models.TagOption.CreateFromDiscriminatorValue)?.ToList(); } },
                { "translations", n => { Translations = n.GetObjectValue<ApiSdk.Models.TranslationExtended>(ApiSdk.Models.TranslationExtended.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.Alias>("aliases", Aliases);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.AwardBaseRecord>("awards", Awards);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.Biography>("biographies", Biographies);
            writer.WriteStringValue("birth", Birth);
            writer.WriteStringValue("birthPlace", BirthPlace);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.Character>("characters", Characters);
            writer.WriteStringValue("death", Death);
            writer.WriteIntValue("gender", Gender);
            writer.WriteLongValue("id", Id);
            writer.WriteStringValue("image", Image);
            writer.WriteStringValue("lastUpdated", LastUpdated);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfPrimitiveValues<string>("nameTranslations", NameTranslations);
            writer.WriteCollectionOfPrimitiveValues<string>("overviewTranslations", OverviewTranslations);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.Race>("races", Races);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.RemoteID>("remoteIds", RemoteIds);
            writer.WriteLongValue("score", Score);
            writer.WriteStringValue("slug", Slug);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.TagOption>("tagOptions", TagOptions);
            writer.WriteObjectValue<ApiSdk.Models.TranslationExtended>("translations", Translations);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
