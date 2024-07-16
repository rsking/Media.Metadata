// <auto-generated/>
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace ApiSdk.Series.Item.Episodes.Item.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \series\{id}\episodes\{season-type}\{lang}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    public partial class WithLangItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithLangItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/{id}/episodes/{season%2Dtype}/{lang}?page={page}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithLangItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/{id}/episodes/{season%2Dtype}/{lang}?page={page}", rawUrl)
        {
        }
        /// <summary>
        /// Returns series base record with episodes from the specified season type and language. Default returns the episodes in the series default season type.
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangGetResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangGetResponse?> GetAsync(Action<RequestConfiguration<global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder.WithLangItemRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangGetResponse> GetAsync(Action<RequestConfiguration<global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder.WithLangItemRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangGetResponse>(requestInfo, global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangGetResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Returns series base record with episodes from the specified season type and language. Default returns the episodes in the series default season type.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder.WithLangItemRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder.WithLangItemRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Returns series base record with episodes from the specified season type and language. Default returns the episodes in the series default season type.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
        public partial class WithLangItemRequestBuilderGetQueryParameters 
        {
            [QueryParameter("page")]
            public int? Page { get; set; }
        }
    }
}
