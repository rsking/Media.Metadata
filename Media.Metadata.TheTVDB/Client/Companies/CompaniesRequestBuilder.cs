// <auto-generated/>
using ApiSdk.Companies.Item;
using ApiSdk.Companies.Types;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace ApiSdk.Companies
{
    /// <summary>
    /// Builds and executes requests for operations under \companies
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    public partial class CompaniesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The types property</summary>
        public global::ApiSdk.Companies.Types.TypesRequestBuilder Types
        {
            get => new global::ApiSdk.Companies.Types.TypesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the ApiSdk.companies.item collection</summary>
        /// <param name="position">id</param>
        /// <returns>A <see cref="global::ApiSdk.Companies.Item.CompaniesItemRequestBuilder"/></returns>
        public global::ApiSdk.Companies.Item.CompaniesItemRequestBuilder this[double position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("id", position);
                return new global::ApiSdk.Companies.Item.CompaniesItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Companies.CompaniesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CompaniesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/companies{?page*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Companies.CompaniesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CompaniesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/companies{?page*}", rawUrl)
        {
        }
        /// <summary>
        /// returns a paginated list of company records
        /// </summary>
        /// <returns>A <see cref="global::ApiSdk.Companies.CompaniesGetResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::ApiSdk.Companies.CompaniesGetResponse?> GetAsync(Action<RequestConfiguration<global::ApiSdk.Companies.CompaniesRequestBuilder.CompaniesRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::ApiSdk.Companies.CompaniesGetResponse> GetAsync(Action<RequestConfiguration<global::ApiSdk.Companies.CompaniesRequestBuilder.CompaniesRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::ApiSdk.Companies.CompaniesGetResponse>(requestInfo, global::ApiSdk.Companies.CompaniesGetResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// returns a paginated list of company records
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::ApiSdk.Companies.CompaniesRequestBuilder.CompaniesRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::ApiSdk.Companies.CompaniesRequestBuilder.CompaniesRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::ApiSdk.Companies.CompaniesRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::ApiSdk.Companies.CompaniesRequestBuilder WithUrl(string rawUrl)
        {
            return new global::ApiSdk.Companies.CompaniesRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// returns a paginated list of company records
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
        public partial class CompaniesRequestBuilderGetQueryParameters 
        {
            /// <summary>name</summary>
            [QueryParameter("page")]
            public double? Page { get; set; }
        }
    }
}
