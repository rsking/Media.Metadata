// -----------------------------------------------------------------------
// <copyright file="TheTVDbShowSearch.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.TheTVDB;

/// <summary>
/// The TVDb show search.
/// </summary>
public sealed class TheTVDbShowSearch : IShowSearch
{
    private static readonly System.Text.Json.JsonSerializerOptions Options = new() { PropertyNamingPolicy = new LowerCaseJsonNamingPolicy() };

    private readonly RestSharp.IRestClient client;

    private TokenResponse? tokenResponse;

    /// <summary>
    /// Initialises a new instance of the <see cref="TheTVDbShowSearch"/> class.
    /// </summary>
    /// <param name="restClient">The rest client.</param>
    public TheTVDbShowSearch(RestSharp.IRestClient restClient)
    {
        this.client = restClient;
        this.client.BaseUrl = new Uri("https://api.thetvdb.com");
    }

    /// <inheritdoc/>
    async IAsyncEnumerable<Show> IShowSearch.SearchAsync(string name, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var seriesName in this
            .SearchSeriesAsync(name, cancellationToken)
            .Select(series => series.SeriesName)
            .ConfigureAwait(false))
        {
            if (seriesName is null)
            {
                continue;
            }

            yield return new Show(seriesName);
        }
    }

    private async IAsyncEnumerable<Series> SearchSeriesAsync(string name, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        this.tokenResponse ??= await GetTokenAsync(this.client, cancellationToken).ConfigureAwait(false);
        if (this.tokenResponse is null)
        {
            throw new InvalidOperationException(Properties.Resources.FailedToGetTokenResponse);
        }

        var request = new RestSharp.RestRequest("search/series")
            .AddQueryParameter("name", name)
            .AddHeader("Authorization", $"Bearer {this.tokenResponse.Token}");

        var response = await this.client.ExecuteGetTaskAsync<SeriesList>(request, cancellationToken).ConfigureAwait(false);
        if (response.IsSuccessful && response.Data?.Series is not null)
        {
            foreach (var series in response.Data.Series)
            {
                var fullSeries = await GetSeriesAsync(series.Id, this.tokenResponse, cancellationToken).ConfigureAwait(false);
                if (fullSeries is not null)
                {
                    yield return fullSeries;
                }

                async Task<Series?> GetSeriesAsync(int id, TokenResponse tokenResponse, CancellationToken cancellationToken)
                {
                    var request = new RestSharp.RestRequest("/series/{id}")
                        .AddUrlSegment("id", id.ToString(System.Globalization.CultureInfo.InvariantCulture))
                        .AddHeader("Authorization", $"Bearer {tokenResponse.Token}");

                    var response = await this.client.ExecuteGetTaskAsync<ShowItem>(request, cancellationToken).ConfigureAwait(false);

                    return response.IsSuccessful ? response.Data.Series : default;
                }
            }
        }

        async static Task<TokenResponse?> GetTokenAsync(RestSharp.IRestClient client, CancellationToken cancellationToken = default)
        {
            if (await GetTokenFromFile(cancellationToken: cancellationToken).ConfigureAwait(false) is TokenResponse tokenFromFile)
            {
                return tokenFromFile;
            }

            if (await RequestToken(client, cancellationToken).ConfigureAwait(false) is TokenResponse tokenFromWeb)
            {
                // write this to the local cache
                await WriteTokenToFile(tokenFromWeb, cancellationToken: cancellationToken).ConfigureAwait(false);
                return tokenFromWeb;
            }

            return default;

            static async Task<TokenResponse?> RequestToken(RestSharp.IRestClient client, CancellationToken cancellationToken)
            {
                var tokenRequest = new RestSharp.RestRequest("login", RestSharp.Method.POST)
                    .AddJsonBody(new TokenRequest { ApiKey = TheTVDbHelpers.ApiKey });

                var response = await client.ExecutePostTaskAsync<TokenResponse>(tokenRequest, cancellationToken).ConfigureAwait(false);
                return response.IsSuccessful ? response.Data : default;
            }

            static async Task<TokenResponse?> GetTokenFromFile(string? fileName = null, CancellationToken cancellationToken = default)
            {
                fileName ??= GenerateFileName();
                if (File.Exists(fileName))
                {
                    using var stream = File.OpenRead(fileName);
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<TokenResponse>(stream, Options, cancellationToken).ConfigureAwait(false);
                }

                return default;
            }

            static async Task WriteTokenToFile(TokenResponse tokenResponse, string? fileName = null, CancellationToken cancellationToken = default)
            {
                fileName ??= GenerateFileName();
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                using var stream = File.OpenWrite(fileName);
                await System.Text.Json.JsonSerializer.SerializeAsync(stream, tokenResponse, Options, cancellationToken: cancellationToken).ConfigureAwait(false);
            }

            static string GenerateFileName()
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Media.Metadata", "TheTVDb.token.json");
            }
        }
    }

#pragma warning disable S3459 // Unassigned members should be removed
#pragma warning disable S1144 // Unused private types or members should be removed
    private sealed record TokenRequest
    {
        public string? ApiKey { get; init; }

        public string? UserKey { get; init; }

        public string? UserName { get; init; }
    }

    private sealed record TokenResponse
    {
        public string? Token { get; init; }
    }

    private sealed record SeriesList
    {
        [System.Text.Json.Serialization.JsonPropertyName("data")]
        public Series[] Series { get; init; } = Array.Empty<Series>();
    }

    private sealed record ShowItem
    {
        [System.Text.Json.Serialization.JsonPropertyName("data")]
        public Series? Series { get; init; }
    }

    private sealed record Series
    {
        public DateTime? Added { get; init; }

        public string? AirsDayOfWeek { get; init; }

        public string? AirsTime { get; init; }

        public string[] Aliases { get; init; } = Array.Empty<string>();

        public string? Banner { get; init; }

        public DateTime? FirstAired { get; init; }

        public string[] Genre { get; init; } = Array.Empty<string>();

        public int Id { get; init; }

        public string? ImdbId { get; init; }

        public string? Network { get; init; }

        public string? NetworkId { get; init; }

        public string? Overview { get; init; }

        public string? Rating { get; init; }

        public string? Runtime { get; init; }

        public string? SeriesId { get; init; }

        public string? SeriesName { get; init; }

        public float? SiteRating { get; init; }

        public int? SiteRatingCount { get; init; }

        public string? Slug { get; init; }

        public string? Status { get; init; }

        public string? Zap2ItId { get; init; }
    }
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore S3459 // Unassigned members should be removed

    private sealed class LowerCaseJsonNamingPolicy : System.Text.Json.JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLowerInvariant();
    }
}