// <auto-generated/>
#pragma warning disable CS0618
using ApiSdk.Characters.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Characters
{
    /// <summary>
    /// Builds and executes requests for operations under \characters
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.19.0")]
    internal partial class CharactersRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the ApiSdk.characters.item collection</summary>
        /// <param name="position">id</param>
        /// <returns>A <see cref="global::ApiSdk.Characters.Item.CharactersItemRequestBuilder"/></returns>
        public global::ApiSdk.Characters.Item.CharactersItemRequestBuilder this[double position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("id", position);
                return new global::ApiSdk.Characters.Item.CharactersItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Characters.CharactersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CharactersRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/characters", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::ApiSdk.Characters.CharactersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CharactersRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/characters", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
