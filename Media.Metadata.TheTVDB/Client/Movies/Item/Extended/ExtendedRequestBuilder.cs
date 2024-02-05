// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace ApiSdk.Movies.Item.Extended {
    /// <summary>
    /// Builds and executes requests for operations under \movies\{id}\extended
    /// </summary>
    public class ExtendedRequestBuilder : BaseRequestBuilder {
        /// <summary>
        /// Instantiates a new ExtendedRequestBuilder and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ExtendedRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/movies/{id}/extended{?meta*,short*}", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new ExtendedRequestBuilder and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ExtendedRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/movies/{id}/extended{?meta*,short*}", rawUrl) {
        }
        /// <summary>
        /// Returns movie extended record
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<ExtendedGetResponse?> GetAsync(Action<RequestConfiguration<ExtendedRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default) {
#nullable restore
#else
        public async Task<ExtendedGetResponse> GetAsync(Action<RequestConfiguration<ExtendedRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default) {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<ExtendedGetResponse>(requestInfo, ExtendedGetResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Returns movie extended record
        /// </summary>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<ExtendedRequestBuilderGetQueryParameters>>? requestConfiguration = default) {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<ExtendedRequestBuilderGetQueryParameters>> requestConfiguration = default) {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public ExtendedRequestBuilder WithUrl(string rawUrl) {
            return new ExtendedRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Returns movie extended record
        /// </summary>
        public class ExtendedRequestBuilderGetQueryParameters {
            /// <summary>meta</summary>
            [QueryParameter("meta")]
            public GetMetaQueryParameterType? Meta { get; set; }
            /// <summary>reduce the payload and returns the short version of this record without characters, artworks and trailers.</summary>
            [QueryParameter("short")]
            public bool? Short { get; set; }
        }
    }
}
