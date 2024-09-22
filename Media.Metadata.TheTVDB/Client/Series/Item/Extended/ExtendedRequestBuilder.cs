// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace ApiSdk.Series.Item.Extended
{
    /// <summary>
    /// Builds and executes requests for operations under \series\{id}\extended
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    internal partial class ExtendedRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Item.Extended.ExtendedRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ExtendedRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/{id}/extended{?meta*,short*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Item.Extended.ExtendedRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ExtendedRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/{id}/extended{?meta*,short*}", rawUrl)
        {
        }
        /// <summary>
        /// Returns series extended record
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Series.Item.Extended.ExtendedGetResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::ApiSdk.Series.Item.Extended.ExtendedGetResponse?> GetAsync(Action<RequestConfiguration<global::ApiSdk.Series.Item.Extended.ExtendedRequestBuilder.ExtendedRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::ApiSdk.Series.Item.Extended.ExtendedGetResponse> GetAsync(Action<RequestConfiguration<global::ApiSdk.Series.Item.Extended.ExtendedRequestBuilder.ExtendedRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::ApiSdk.Series.Item.Extended.ExtendedGetResponse>(requestInfo, global::ApiSdk.Series.Item.Extended.ExtendedGetResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Returns series extended record
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::ApiSdk.Series.Item.Extended.ExtendedRequestBuilder.ExtendedRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::ApiSdk.Series.Item.Extended.ExtendedRequestBuilder.ExtendedRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Series.Item.Extended.ExtendedRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::ApiSdk.Series.Item.Extended.ExtendedRequestBuilder WithUrl(string rawUrl)
        {
            return new global::ApiSdk.Series.Item.Extended.ExtendedRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Returns series extended record
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
        internal partial class ExtendedRequestBuilderGetQueryParameters 
        {
            /// <summary>meta</summary>
            [QueryParameter("meta")]
            public global::ApiSdk.Series.Item.Extended.GetMetaQueryParameterType? Meta { get; set; }
            /// <summary>reduce the payload and returns the short version of this record without characters and artworks</summary>
            [QueryParameter("short")]
            public bool? Short { get; set; }
        }
    }
}
#pragma warning restore CS0618
