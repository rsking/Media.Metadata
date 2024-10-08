// <auto-generated/>
#pragma warning disable CS0618
using ApiSdk.Series.Item.Episodes.Item.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace ApiSdk.Series.Item.Episodes.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \series\{id}\episodes\{season-type}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    internal partial class SeasonTypeItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the ApiSdk.series.item.episodes.item.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder"/></returns>
        public global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("lang", position);
                return new global::ApiSdk.Series.Item.Episodes.Item.Item.WithLangItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SeasonTypeItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/{id}/episodes/{season%2Dtype}?page={page}{&airDate*,episodeNumber*,season*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SeasonTypeItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/{id}/episodes/{season%2Dtype}?page={page}{&airDate*,episodeNumber*,season*}", rawUrl)
        {
        }
        /// <summary>
        /// Returns series episodes from the specified season type, default returns the episodes in the series default season type
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeGetResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeGetResponse?> GetAsync(Action<RequestConfiguration<global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder.SeasonTypeItemRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeGetResponse> GetAsync(Action<RequestConfiguration<global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder.SeasonTypeItemRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeGetResponse>(requestInfo, global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeGetResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Returns series episodes from the specified season type, default returns the episodes in the series default season type
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder.SeasonTypeItemRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder.SeasonTypeItemRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Returns series episodes from the specified season type, default returns the episodes in the series default season type
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
        internal partial class SeasonTypeItemRequestBuilderGetQueryParameters 
        {
            /// <summary>airDate of the episode, format is yyyy-mm-dd</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("airDate")]
            public string? AirDate { get; set; }
#nullable restore
#else
            [QueryParameter("airDate")]
            public string AirDate { get; set; }
#endif
            [QueryParameter("episodeNumber")]
            public int? EpisodeNumber { get; set; }
            [QueryParameter("page")]
            public int? Page { get; set; }
            [QueryParameter("season")]
            public int? Season { get; set; }
        }
    }
}
#pragma warning restore CS0618
