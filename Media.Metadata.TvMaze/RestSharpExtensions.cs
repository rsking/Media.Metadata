// -----------------------------------------------------------------------
// <copyright file="RestSharpExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.TvMaze;

using RestSharp;

/// <summary>
/// <see cref="RestSharp"/> extensions.
/// </summary>
internal static class RestSharpExtensions
{
    /// <summary>
    /// Gets the response, or throws an error.
    /// </summary>
    /// <typeparam name="T">The type of response.</typeparam>
    /// <param name="client">The <see cref="RestClient"/>.</param>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of <see cref="RestClientExtensions.ExecuteGetAsync(IRestClient, RestRequest, CancellationToken)"/> for <paramref name="client"/>.</returns>
    /// <exception cref="InvalidOperationException">The request was not successful.</exception>
    public static async Task<T> GetResponseOrThrow<T>(this IRestClient client, RestRequest request, CancellationToken cancellationToken) => await client.ExecuteGetAsync<T>(request, cancellationToken).ConfigureAwait(false) switch
    {
        { IsSuccessful: true, Data: { } data } => data,
        { ErrorException: { } ex } => throw ex,
        _ => throw new InvalidOperationException(),
    };
}