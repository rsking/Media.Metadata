// <auto-generated/>
#pragma warning disable CS0618
using ApiSdk.Episodes.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace ApiSdk.Episodes
{
    /// <summary>
    /// Builds and executes requests for operations under \episodes
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    internal partial class EpisodesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the ApiSdk.episodes.item collection</summary>
        /// <param name="position">id</param>
        /// <returns>A <see cref="global::ApiSdk.Episodes.Item.EpisodesItemRequestBuilder"/></returns>
        public global::ApiSdk.Episodes.Item.EpisodesItemRequestBuilder this[double position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("id", position);
                return new global::ApiSdk.Episodes.Item.EpisodesItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Episodes.EpisodesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public EpisodesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/episodes{?page*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Episodes.EpisodesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public EpisodesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/episodes{?page*}", rawUrl)
        {
        }
        /// <summary>
        /// Returns a list of episodes base records with the basic attributes.&lt;br&gt; Note that all episodes are returned, even those that may not be included in a series&apos; default season order.
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Episodes.EpisodesGetResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::ApiSdk.Episodes.EpisodesGetResponse?> GetAsync(Action<RequestConfiguration<global::ApiSdk.Episodes.EpisodesRequestBuilder.EpisodesRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::ApiSdk.Episodes.EpisodesGetResponse> GetAsync(Action<RequestConfiguration<global::ApiSdk.Episodes.EpisodesRequestBuilder.EpisodesRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::ApiSdk.Episodes.EpisodesGetResponse>(requestInfo, global::ApiSdk.Episodes.EpisodesGetResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Returns a list of episodes base records with the basic attributes.&lt;br&gt; Note that all episodes are returned, even those that may not be included in a series&apos; default season order.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::ApiSdk.Episodes.EpisodesRequestBuilder.EpisodesRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::ApiSdk.Episodes.EpisodesRequestBuilder.EpisodesRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::ApiSdk.Episodes.EpisodesRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::ApiSdk.Episodes.EpisodesRequestBuilder WithUrl(string rawUrl)
        {
            return new global::ApiSdk.Episodes.EpisodesRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Returns a list of episodes base records with the basic attributes.&lt;br&gt; Note that all episodes are returned, even those that may not be included in a series&apos; default season order.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
        internal partial class EpisodesRequestBuilderGetQueryParameters 
        {
            /// <summary>page number</summary>
            [QueryParameter("page")]
            public double? Page { get; set; }
        }
    }
}
#pragma warning restore CS0618
