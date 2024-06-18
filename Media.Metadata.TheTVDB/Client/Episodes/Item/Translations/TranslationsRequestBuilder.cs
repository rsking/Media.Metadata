// <auto-generated/>
using ApiSdk.Episodes.Item.Translations.Item;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Episodes.Item.Translations
{
    /// <summary>
    /// Builds and executes requests for operations under \episodes\{id}\translations
    /// </summary>
    public class TranslationsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the ApiSdk.episodes.item.translations.item collection</summary>
        /// <param name="position">language</param>
        /// <returns>A <see cref="ApiSdk.Episodes.Item.Translations.Item.WithLanguageItemRequestBuilder"/></returns>
        public ApiSdk.Episodes.Item.Translations.Item.WithLanguageItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("language", position);
                return new ApiSdk.Episodes.Item.Translations.Item.WithLanguageItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Episodes.Item.Translations.TranslationsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public TranslationsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/episodes/{id}/translations", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Episodes.Item.Translations.TranslationsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public TranslationsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/episodes/{id}/translations", rawUrl)
        {
        }
    }
}
