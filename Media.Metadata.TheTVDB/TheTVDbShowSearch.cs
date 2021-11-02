// -----------------------------------------------------------------------
// <copyright file="TheTVDbShowSearch.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.TheTVDB;

using RestSharp;

/// <summary>
/// The TVDb show search.
/// </summary>
public sealed class TheTVDbShowSearch : IShowSearch
{
    private static readonly System.Text.Json.JsonSerializerOptions Options = new() { PropertyNamingPolicy = new LowerCaseJsonNamingPolicy() };

    private readonly IRestClient client;

    private readonly string pin;

    private TokenResponse? tokenResponse;

    /// <summary>
    /// Initialises a new instance of the <see cref="TheTVDbShowSearch"/> class.
    /// </summary>
    /// <param name="restClient">The rest client.</param>
    /// <param name="options">The options.</param>
    public TheTVDbShowSearch(IRestClient restClient, Microsoft.Extensions.Options.IOptions<TheTVDbOptions> options)
    {
        this.client = restClient;
        this.client.BaseUrl = new Uri("https://api4.thetvdb.com/v4");
        this.pin = options.Value.Pin;
    }

    /// <inheritdoc/>
    async IAsyncEnumerable<Series> IShowSearch.SearchAsync(string name, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        this.tokenResponse ??= await GetTokenAsync(this.client, this.pin, cancellationToken).ConfigureAwait(false);
        if (this.tokenResponse is null)
        {
            throw new InvalidOperationException(Properties.Resources.FailedToGetTokenResponse);
        }

        this.client.AddDefaultHeader("Authorization", $"Bearer {this.tokenResponse.Token}");

        await foreach (var series in this
            .SearchSeriesAsync(name, cancellationToken)
            .ConfigureAwait(false))
        {
            if (series is null || series.Name is null)
            {
                continue;
            }

            yield return new Series(series.Name, GetSeasons(this.client, series.TvDbId, cancellationToken).ToEnumerable());
        }

        static async IAsyncEnumerable<Season> GetSeasons(IRestClient client, string? id, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var request = new RestRequest("/series/{id}/extended");
            request.AddUrlSegment("id", id);

            var series = await client.ExecuteGetTaskAsync<Response<SeriesExtendedRecord>>(request, cancellationToken).ConfigureAwait(false);

            if (!series.IsSuccessful || series.Data.Data is null || series.Data.Data.Seasons is null)
            {
                yield break;
            }

            foreach (var season in series.Data.Data.Seasons)
            {
                if (season.Type?.Id != 1)
                {
                    continue;
                }

                yield return new Season(season.Number, GetEpisodes(client, season.Id, cancellationToken).ToEnumerable());
            }

            static async IAsyncEnumerable<Episode> GetEpisodes(IRestClient client, int seasonId, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
            {
                var request = new RestRequest("/seasons/{id}/extended");
                request.AddUrlSegment("id", seasonId);

                var season = await client.ExecuteGetTaskAsync<Response<SeasonExtendedRecord>>(request, cancellationToken).ConfigureAwait(false);

                if (!season.IsSuccessful || season.Data.Data is null || season.Data.Data.Episodes is null)
                {
                    yield break;
                }

                foreach (var episode in season.Data.Data.Episodes)
                {
                    yield return new Episode(episode.Name, episode.Overview);
                }
            }
        }

        async static Task<TokenResponse?> GetTokenAsync(IRestClient client, string? pin, CancellationToken cancellationToken = default)
        {
            if (await GetTokenFromFile(cancellationToken: cancellationToken).ConfigureAwait(false) is TokenResponse tokenFromFile)
            {
                return tokenFromFile;
            }

            if (await RequestToken(client, pin, cancellationToken).ConfigureAwait(false) is TokenResponse tokenFromWeb)
            {
                // write this to the local cache
                await WriteTokenToFile(tokenFromWeb, cancellationToken: cancellationToken).ConfigureAwait(false);
                return tokenFromWeb;
            }

            return default;

            static async Task<TokenResponse?> RequestToken(IRestClient client, string? pin, CancellationToken cancellationToken)
            {
                var tokenRequest = new RestRequest("login", RestSharp.Method.POST)
                    .AddJsonBody(new TokenRequest { ApiKey = TheTVDbHelpers.ApiKey, Pin = pin });

                var response = await client.ExecutePostTaskAsync<Response<TokenResponse>>(tokenRequest, cancellationToken).ConfigureAwait(false);
                return response.IsSuccessful ? response.Data.Data : default;
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

    private async IAsyncEnumerable<SearchResult> SearchSeriesAsync(string name, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var request = new RestRequest("search")
            .AddQueryParameter("q", name)
            .AddQueryParameter("type", "series");

        var response = await this.client.ExecuteGetTaskAsync<Response<ICollection<SearchResult>>>(request, cancellationToken).ConfigureAwait(false);
        if (response.IsSuccessful && response.Data?.Data is not null)
        {
            foreach (var series in response.Data.Data)
            {
                yield return series;
            }
        }
    }

#pragma warning disable S3459 // Unassigned members should be removed
#pragma warning disable S1144 // Unused private types or members should be removed
#pragma warning disable SA1600 // Elements should be documented
    private sealed record Response<T>
    {
        public T? Data { get; init; }

        public string? Status { get; init; }
    }

    private sealed record TokenRequest
    {
        public string? ApiKey { get; init; }

        public string? Pin { get; init; }
    }

    private sealed record TokenResponse
    {
        public string? Token { get; init; }
    }

    public sealed record TranslationSimple
    {
        public string? Language { get; init; }

        public string? Description { get; init; }
    }

    public sealed record RemoteId
    {
        public string? Id { get; init; }

        public long Type { get; init; }

        public string? SourceName { get; init; }
    }

    private sealed record SearchResult
    {
        public string? ObjectId { get; init; }

        public ICollection<string>? Aliases { get; init; }

        public string? Country { get; init; }

        public string? Id { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("image_url")]
        public string? ImageUrl { get; init; }

        public string? Name { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("name_translated")]
        public string? NameTranslated { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("first_air_time")]
        public string? FirstAirTime { get; init; }

        public string? Overview { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("overview_translated")]
        public ICollection<string>? OverviewTranslated { get; init; }

        public string? Primary_language { get; init; }

        public string? Status { get; init; }

        public string? Type { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("tvdb_id")]
        public string? TvDbId { get; init; }

        public string? Year { get; init; }

        public string? Slug { get; init; }

        public IDictionary<string, string>? Overviews { get; init; }

        public IDictionary<string, string>? Translations { get; init; }

        public string? Network { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("remote_ids")]
        public ICollection<RemoteId>? RemoteIds { get; init; }

        public string? Thumbnail { get; init; }
    }

    private sealed record SeriesExtendedRecord
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public string? Slug { get; init; }

        public string? Image { get; init; }

        public ICollection<string>? NameTranslations { get; init; }

        public ICollection<string>? OverviewTranslations { get; init; }

        public ICollection<Alias>? Aliases { get; init; }

        public string? FirstAired { get; init; }

        public string? LastAired { get; init; }

        public string? NextAired { get; init; }

        public float? Score { get; init; }

        public Status? Status { get; init; }

        public string? OriginalCountry { get; init; }

        public string? OriginalLanguage { get; init; }

        public int DefaultSeasonType { get; init; }

        public bool IsOrderRandomized { get; init; }

        public string? LastUpdated { get; init; }

        public int AverageRuntime { get; init; }

        public ICollection<SeasonBaseRecord>? Seasons { get; init; }
    }

    private sealed record Alias
    {
        public string? Language { get; init; }

        public string? Name { get; init; }
    }

    public sealed record Status
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public string? RecordType { get; init; }

        public bool KeepUpdated { get; init; }
    }

    public record SeasonBaseRecord
    {
        public int Id { get; init; }

        public int SeriesId { get; init; }

        public SeasonType? Type { get; init; }

        public string? Name { get; init; }

        public int Number { get; init; }

        public ICollection<string>? NameTranslations { get; init; }

        public ICollection<string>? OverviewTranslations { get; init; }

        public string? Image { get; init; }

        public int? ImageType { get; init; }

        public IDictionary<string, System.Collections.IEnumerable>? Companies { get; init; }
    }

    public sealed record SeasonType
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public string? Type { get; init; }
    }

    public sealed record SeasonExtendedRecord : SeasonBaseRecord
    {
        public ICollection<EpisodeBaseRecord>? Episodes { get; init; }
    }

    public sealed record EpisodeBaseRecord
    {
        public int Id { get; init; }

        public int SeriesId { get; init; }

        public string? Name { get; init; }

        public string? Aired { get; init; }

        public int Runtime { get; init; }

        public ICollection<string>? NameTranslations { get; init; }

        public string? Overview { get; init; }

        public ICollection<string>? OverviewTranslations { get; init; }

        public string? Image { get; init; }

        public int? ImageType { get; init; }

        public int IsMovie { get; init; }

        public int Number { get; init; }

        public int SeasonNumber { get; init; }

        public string? LastUpdated { get; init; }
    }
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore S3459 // Unassigned members should be removed

    private sealed class LowerCaseJsonNamingPolicy : System.Text.Json.JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLowerInvariant();
    }
}