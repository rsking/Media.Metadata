// <auto-generated/>
#pragma warning disable CS0618
using ApiSdk.Series.Item.Translations.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Series.Item.Translations
{
    /// <summary>
    /// Builds and executes requests for operations under \series\{id}\translations
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    internal partial class TranslationsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the ApiSdk.series.item.translations.item collection</summary>
        /// <param name="position">language</param>
        /// <returns>A <see cref="global::ApiSdk.Series.Item.Translations.Item.WithLanguageItemRequestBuilder"/></returns>
        public global::ApiSdk.Series.Item.Translations.Item.WithLanguageItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("language", position);
                return new global::ApiSdk.Series.Item.Translations.Item.WithLanguageItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Item.Translations.TranslationsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public TranslationsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/{id}/translations", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Item.Translations.TranslationsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public TranslationsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/{id}/translations", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
