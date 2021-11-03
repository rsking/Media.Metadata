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
        this.pin = options.Value.Pin ?? throw new ArgumentNullException(nameof(options), "Pin cannot be null");
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

            var seriesResponse = await client.ExecuteGetTaskAsync<Response<SeriesExtendedRecord>>(request, cancellationToken).ConfigureAwait(false);

            if (!seriesResponse.IsSuccessful || seriesResponse.Data.Data is null || seriesResponse.Data.Data.Seasons is null)
            {
                yield break;
            }

            var series = seriesResponse.Data.Data;

            foreach (var season in series.Seasons)
            {
                if (season.Type?.Id != 1)
                {
                    continue;
                }

                yield return new Season(season.Number, GetEpisodes(client, series, season.Id, cancellationToken).ToEnumerable());
            }

            static async IAsyncEnumerable<Episode> GetEpisodes(IRestClient client, SeriesExtendedRecord series, int seasonId, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
            {
                var request = new RestRequest("/seasons/{id}/extended");
                request.AddUrlSegment("id", seasonId);

                var seasonResponse = await client.ExecuteGetTaskAsync<Response<SeasonExtendedRecord>>(request, cancellationToken).ConfigureAwait(false);

                if (!seasonResponse.IsSuccessful || seasonResponse.Data.Data is null || seasonResponse.Data.Data.Episodes is null)
                {
                    yield break;
                }

                var season = seasonResponse.Data.Data;

                var extendedEpisodes = season.Episodes.ToAsyncEnumerable().SelectAwait(async episode =>
                {
                    var request = new RestRequest("/episodes/{id}/extended");
                    request.AddUrlSegment("id", episode.Id);

                    var episodeResponse = await client.ExecuteGetTaskAsync<Response<EpisodeExtendedRecord>>(request, cancellationToken).ConfigureAwait(false);

                    var extendedEpisode = episodeResponse.Data.Data;

                    if (extendedEpisode is not null && extendedEpisode.Overview is null)
                    {
                        request = new RestRequest("/episodes/{id}/translations/{language}");
                        request.AddUrlSegment("id", episode.Id)
                            .AddUrlSegment("language", "eng");

                        var translationResponse = await client.ExecuteGetTaskAsync<Response<Translation>>(request, cancellationToken).ConfigureAwait(false);
                        if (translationResponse.IsSuccessful && translationResponse.Data.Data is not null)
                        {
                            return extendedEpisode with
                            {
                                Name = translationResponse.Data.Data.Name,
                                Overview = translationResponse.Data.Data.Overview,
                            };
                        }
                    }

                    return extendedEpisode;
                });

                await foreach (var episode in extendedEpisodes
                    .Where(episode => episode is not null)
                    .Select(episode =>
                    {
                        return new RemoteEpisode(episode!.Name, episode.Overview)
                        {
                            Season = episode.SeasonNumber,
                            Number = episode.Number,
                            Id = episode.ProductionCode,
                            Show = series.Name,
                            ScreenWriters = GetWriters(episode.Characters),
                            Cast = GetCast(episode.Characters),
                            Directors = GetDirectors(episode.Characters),
                        };

                        static IEnumerable<string>? GetWriters(IEnumerable<Character>? characters)
                        {
                            return GetCharacters(characters, "Writer");
                        }

                        static IEnumerable<string>? GetDirectors(IEnumerable<Character>? characters)
                        {
                            return GetCharacters(characters, "Director");
                        }

                        static IEnumerable<string>? GetCast(IEnumerable<Character>? characters)
                        {
                            return GetCharacters(characters, "Guest Star");
                        }

                        static IEnumerable<string>? GetCharacters(IEnumerable<Character>? characters, string peopleType)
                        {
                            if (characters is null)
                            {
                                return default;
                            }

                            return characters
                                .Where(character => string.Equals(character.PeopleType, peopleType, StringComparison.Ordinal) && character.PersonName is not null)
                                .Select(character => character.PersonName!);
                        }
                    })
                    .Select(episode => seasonResponse.Data.Data.Image switch
                    {
                        not null => episode with { ImageUri = new Uri(seasonResponse.Data.Data.Image) },
                        _ => episode,
                    })
                    .WithCancellation(cancellationToken)
                    .ConfigureAwait(false))
                {
                    yield return episode;
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

    public record EpisodeBaseRecord
    {
        public int Id { get; init; }

        public int SeriesId { get; init; }

        public string? Aired { get; init; }

        public int Runtime { get; init; }

        public string? Name { get; init; }

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

    public sealed record EpisodeExtendedRecord : EpisodeBaseRecord
    {
        public int? AirsAfterSeason { get; init; }

        public int? AirsBeforeEpisode { get; init; }

        public int? AirsBeforeSeason { get; init; }

        //public ICollection<AwardBaseRecord> Awards { get; init; }

        public ICollection<Character>? Characters { get; init; }

        public ICollection<ContentRating>? ContentRatings { get; init; }

        public ICollection<Company>? Networks { get; init; }

        public string? ProductionCode { get; init; }

        public ICollection<RemoteId>? RemoteIds { get; init; }

        public ICollection<SeasonBaseRecord>? Seasons { get; init; }

        //public ICollection<TagOption> TagOptions { get; init; }

        //public ICollection<Trailer> Trailers { get; init; }
    }

    public sealed record Character
    {
        public ICollection<string>? Aliases { get; init; }

        public int EpisodeId { get; init; }

        public int Id { get; init; }

        public string? Image { get; init; }

        public bool IsFeatured { get; init; }

        public int? PeopleId { get; init; }

        public int? SeriesId { get; init; }

        public int? MovieId { get; init; }

        public string? Name { get; init; }

        public ICollection<string>? NameTranslations { get; init; }

        public string? Overview { get; init; }

        public ICollection<string>? OverviewTranslations { get; init; }

        public int Sort { get; init; }

        public int Type { get; init; }

        public string? Url { get; init; }

        public string? PeopleType { get; init; }

        public string? PersonName { get; init; }
    }

    public sealed record Company
    {
        public int Id { get; init; }

        public string? Slug { get; init; }

        public string? Name { get; init; }

        public ICollection<string>? NameTranslations { get; init; }

        public string? Overview { get; init; }

        public ICollection<string>? OverviewTranslations { get; init; }

        public ICollection<string>? Aliases { get; init; }

        public string? Country { get; init; }

        public int PrimaryCompanyType { get; init; }

        public string? ActiveDate { get; init; }

        public string? InactiveDate { get; init; }

        public CompanyType? CompanyType { get; init; }
    }

    public sealed record CompanyType
    {
        public int CompanyTypeId { get; init; }

        public string? CompanyTypeName { get; init; }
    }

    public sealed record ContentRating
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public string? Country { get; init; }

        public string? ContentType { get; init; }

        public int Order { get; init; }

        public string? FullName { get; init; }
    }

    public sealed record Translation
    {
        public ICollection<string>? Aliases { get; init; }

        public bool IsAlias { get; init; }

        public bool IsPrimary { get; init; }

        public string? Language { get; init; }

        public string? Name { get; init; }

        public string? Overview { get; init; }
        
        public string? Tagline { get; init; }
    }
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore S3459 // Unassigned members should be removed

    private sealed class LowerCaseJsonNamingPolicy : System.Text.Json.JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLowerInvariant();
    }
}