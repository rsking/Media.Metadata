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
    /// <returns>The result of <see cref="RestClientExtensions.ExecuteGetAsync"/> for <paramref name="client"/>.</returns>
    /// <exception cref="InvalidOperationException">The request was not successful.</exception>
    public static async Task<T> GetResponseOrThrow<T>(this RestClient client, RestRequest request, CancellationToken cancellationToken)
    {
        var response = await client.ExecuteGetAsync<T>(request, cancellationToken).ConfigureAwait(false);

        if (response.IsSuccessful && response.Data is not null)
        {
            return response.Data;
        }

        if (response.ErrorException is not null)
        {
            throw response.ErrorException;
        }

        throw new InvalidOperationException();
    }
}