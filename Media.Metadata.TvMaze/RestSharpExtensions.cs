// -----------------------------------------------------------------------
// <copyright file="RestSharpExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.TvMaze;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

internal static class RestSharpExtensions
{
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
