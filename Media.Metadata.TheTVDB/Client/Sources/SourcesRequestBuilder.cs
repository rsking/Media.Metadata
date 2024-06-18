// <auto-generated/>
using ApiSdk.Sources.Types;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Sources
{
    /// <summary>
    /// Builds and executes requests for operations under \sources
    /// </summary>
    public class SourcesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The types property</summary>
        public ApiSdk.Sources.Types.TypesRequestBuilder Types
        {
            get => new ApiSdk.Sources.Types.TypesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Sources.SourcesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SourcesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/sources", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Sources.SourcesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SourcesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/sources", rawUrl)
        {
        }
    }
}
