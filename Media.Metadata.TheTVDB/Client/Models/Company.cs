// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models
{
    /// <summary>
    /// A company record
    /// </summary>
    public class Company : IAdditionalDataHolder, IParsable
    {
        /// <summary>The activeDate property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ActiveDate { get; set; }
#nullable restore
#else
        public string ActiveDate { get; set; }
#endif
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
        /// <summary>The country property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Country { get; set; }
#nullable restore
#else
        public string Country { get; set; }
#endif
        /// <summary>The id property</summary>
        public long? Id { get; set; }
        /// <summary>The inactiveDate property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? InactiveDate { get; set; }
#nullable restore
#else
        public string InactiveDate { get; set; }
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
        /// <summary>A parent company record</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.ParentCompany? ParentCompany { get; set; }
#nullable restore
#else
        public ApiSdk.Models.ParentCompany ParentCompany { get; set; }
#endif
        /// <summary>The primaryCompanyType property</summary>
        public long? PrimaryCompanyType { get; set; }
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
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Models.Company"/> and sets the default values.
        /// </summary>
        public Company()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ApiSdk.Models.Company"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ApiSdk.Models.Company CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ApiSdk.Models.Company();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "activeDate", n => { ActiveDate = n.GetStringValue(); } },
                { "aliases", n => { Aliases = n.GetCollectionOfObjectValues<ApiSdk.Models.Alias>(ApiSdk.Models.Alias.CreateFromDiscriminatorValue)?.ToList(); } },
                { "country", n => { Country = n.GetStringValue(); } },
                { "id", n => { Id = n.GetLongValue(); } },
                { "inactiveDate", n => { InactiveDate = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "nameTranslations", n => { NameTranslations = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                { "overviewTranslations", n => { OverviewTranslations = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                { "parentCompany", n => { ParentCompany = n.GetObjectValue<ApiSdk.Models.ParentCompany>(ApiSdk.Models.ParentCompany.CreateFromDiscriminatorValue); } },
                { "primaryCompanyType", n => { PrimaryCompanyType = n.GetLongValue(); } },
                { "slug", n => { Slug = n.GetStringValue(); } },
                { "tagOptions", n => { TagOptions = n.GetCollectionOfObjectValues<ApiSdk.Models.TagOption>(ApiSdk.Models.TagOption.CreateFromDiscriminatorValue)?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("activeDate", ActiveDate);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.Alias>("aliases", Aliases);
            writer.WriteStringValue("country", Country);
            writer.WriteLongValue("id", Id);
            writer.WriteStringValue("inactiveDate", InactiveDate);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfPrimitiveValues<string>("nameTranslations", NameTranslations);
            writer.WriteCollectionOfPrimitiveValues<string>("overviewTranslations", OverviewTranslations);
            writer.WriteObjectValue<ApiSdk.Models.ParentCompany>("parentCompany", ParentCompany);
            writer.WriteLongValue("primaryCompanyType", PrimaryCompanyType);
            writer.WriteStringValue("slug", Slug);
            writer.WriteCollectionOfObjectValues<ApiSdk.Models.TagOption>("tagOptions", TagOptions);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}