// <auto-generated/>
using ApiSdk.Series.Slug.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Series.Slug
{
    /// <summary>
    /// Builds and executes requests for operations under \series\slug
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    public partial class SlugRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the ApiSdk.series.slug.item collection</summary>
        /// <param name="position">slug</param>
        /// <returns>A <see cref="global::ApiSdk.Series.Slug.Item.WithSlugItemRequestBuilder"/></returns>
        public global::ApiSdk.Series.Slug.Item.WithSlugItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("slug", position);
                return new global::ApiSdk.Series.Slug.Item.WithSlugItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Slug.SlugRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SlugRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/slug", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Slug.SlugRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SlugRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/slug", rawUrl)
        {
        }
    }
}
