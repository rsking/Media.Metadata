// <auto-generated/>
using ApiSdk.Characters.Item;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Characters
{
    /// <summary>
    /// Builds and executes requests for operations under \characters
    /// </summary>
    public class CharactersRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the ApiSdk.characters.item collection</summary>
        /// <param name="position">id</param>
        /// <returns>A <see cref="ApiSdk.Characters.Item.CharactersItemRequestBuilder"/></returns>
        public ApiSdk.Characters.Item.CharactersItemRequestBuilder this[double position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("id", position);
                return new ApiSdk.Characters.Item.CharactersItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Characters.CharactersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CharactersRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/characters", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Characters.CharactersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CharactersRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/characters", rawUrl)
        {
        }
    }
}
