// <auto-generated/>
#pragma warning disable CS0618
using ApiSdk.Awards.Categories.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Awards.Categories
{
    /// <summary>
    /// Builds and executes requests for operations under \awards\categories
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    internal partial class CategoriesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the ApiSdk.awards.categories.item collection</summary>
        /// <param name="position">id</param>
        /// <returns>A <see cref="global::ApiSdk.Awards.Categories.Item.CategoriesItemRequestBuilder"/></returns>
        public global::ApiSdk.Awards.Categories.Item.CategoriesItemRequestBuilder this[double position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("id", position);
                return new global::ApiSdk.Awards.Categories.Item.CategoriesItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Awards.Categories.CategoriesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CategoriesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/awards/categories", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Awards.Categories.CategoriesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CategoriesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/awards/categories", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
