// <auto-generated/>
using ApiSdk.Series.Item.Episodes.Item;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Series.Item.Episodes
{
    /// <summary>
    /// Builds and executes requests for operations under \series\{id}\episodes
    /// </summary>
    public class EpisodesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the ApiSdk.series.item.episodes.item collection</summary>
        /// <param name="position">season-type</param>
        /// <returns>A <see cref="ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder"/></returns>
        public ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("season%2Dtype", position);
                return new ApiSdk.Series.Item.Episodes.Item.SeasonTypeItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Series.Item.Episodes.EpisodesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public EpisodesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/{id}/episodes", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Series.Item.Episodes.EpisodesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public EpisodesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/series/{id}/episodes", rawUrl)
        {
        }
    }
}
