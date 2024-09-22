// <auto-generated/>
#pragma warning disable CS0618
using ApiSdk.Inspiration.Types;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Inspiration
{
    /// <summary>
    /// Builds and executes requests for operations under \inspiration
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    internal partial class InspirationRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The types property</summary>
        public global::ApiSdk.Inspiration.Types.TypesRequestBuilder Types
        {
            get => new global::ApiSdk.Inspiration.Types.TypesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Inspiration.InspirationRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public InspirationRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/inspiration", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Inspiration.InspirationRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public InspirationRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/inspiration", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
