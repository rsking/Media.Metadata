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

    private readonly RestClient client;

    private readonly string pin;

    private readonly Uri baseUrl;

    private TokenResponse? tokenResponse;

    /// <summary>
    /// Initialises a new instance of the <see cref="TheTVDbShowSearch"/> class.
    /// </summary>
    /// <param name="restClient">The rest client.</param>
    /// <param name="options">The options.</param>
    public TheTVDbShowSearch(RestClient restClient, Microsoft.Extensions.Options.IOptions<TheTVDbOptions> options)
    {
        this.client = restClient;
        this.baseUrl = new Uri(options.Value.Url);
        this.pin = options.Value.Pin ?? throw new ArgumentNullException(nameof(options), "Pin cannot be null");
    }

    /// <inheritdoc/>
    async IAsyncEnumerable<Series> IShowSearch.SearchAsync(string name, int year, string country, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var series in this
            .SearchSeriesAsync(name, year, cancellationToken)
            .ConfigureAwait(false))
        {
            if (series is null || series.Name is null)
            {
                continue;
            }

            if (series.Translations?.TryGetValue("eng", out var englishName) != true)
            {
                englishName = series.Name;
            }

            yield return new RemoteSeries(englishName, GetSeasons(this.client, this.baseUrl, series.TvDbId, englishName, country, cancellationToken).ToEnumerable())
            {
                ImageUri = GetUri(series.ImageUrl),
            };
        }

        static Uri? GetUri(string? imageUrl)
        {
            return imageUrl is not null && Uri.TryCreate(imageUrl, UriKind.Absolute, out var uri)
                ? uri
                : default;
        }

        static async IAsyncEnumerable<RemoteSeason> GetSeasons(RestClient client, Uri baseUrl, string? id, string? name, string country, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var request = new RestRequest(CreateUri(baseUrl, "series/{id}/extended"));
            if (id is not null)
            {
                _ = request.AddUrlSegment("id", id);
            }

            var seriesResponse = await client.ExecuteGetAsync<Response<SeriesExtendedRecord>>(request, cancellationToken).ConfigureAwait(false);

            if (!seriesResponse.IsSuccessful
                || seriesResponse.Data is null
                || seriesResponse.Data.Data is null
                || seriesResponse.Data.Data.Seasons is null)
            {
                yield break;
            }

            var series = seriesResponse.Data.Data;

            // only official, aired dates, ordered by the season number
            foreach (var season in series.Seasons
                .Where(s => s.Type?.Id == 1)
                .OrderBy(s => s.Number))
            {
                if (season.Type?.Id != 1)
                {
                    continue;
                }

                yield return new RemoteSeason(
                    season.Number,
                    GetEpisodes(
                        client,
                        baseUrl,
                        name,
                        season.Id,
                        country,
                        seriesResponse.Data.Data.Characters ?? Array.Empty<Character>(),
                        seriesResponse.Data.Data.Companies ?? Array.Empty<Company>(),
                        cancellationToken).ToEnumerable())
                {
                    ImageUri = GetUri(season.Image),
                };
            }

            static async IAsyncEnumerable<RemoteEpisode> GetEpisodes(RestClient client, Uri baseUrl, string? name, int seasonId, string country, IEnumerable<Character> characters, IEnumerable<Company> companies, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
            {
                var request = new RestRequest(CreateUri(baseUrl, "seasons/{id}/extended"))
                    .AddUrlSegment("id", seasonId);

                var seasonResponse = await client.ExecuteGetAsync<Response<SeasonExtendedRecord>>(request, cancellationToken).ConfigureAwait(false);

                if (!seasonResponse.IsSuccessful
                    || seasonResponse.Data?.Data?.Episodes is null)
                {
                    yield break;
                }

                var season = seasonResponse.Data.Data;

                var extendedEpisodes = season.Episodes
                    .OrderBy(ebr => ebr.Number)
                    .ToAsyncEnumerable()
                    .SelectAwait(async episode =>
                    {
                        var request = new RestRequest(CreateUri(baseUrl, "episodes/{id}/extended"))
                            .AddUrlSegment("id", episode.Id);

                        var episodeResponse = await client.ExecuteGetAsync<Response<EpisodeExtendedRecord>>(request, cancellationToken).ConfigureAwait(false);

                        if (episodeResponse.Data?.Data is EpisodeExtendedRecord extendedEpisode)
                        {
                            extendedEpisode = extendedEpisode with { Number = episode.Number, SeasonNumber = episode.SeasonNumber };
                            request = new RestRequest(CreateUri(baseUrl, "episodes/{id}/translations/{language}"))
                                .AddUrlSegment("id", episode.Id)
                                .AddUrlSegment("language", "eng");

                            var translationResponse = await client.ExecuteGetAsync<Response<Translation>>(request, cancellationToken).ConfigureAwait(false);
                            return translationResponse.IsSuccessful && translationResponse.Data?.Data is not null
                                ? extendedEpisode with { Name = translationResponse.Data.Data.Name, Overview = translationResponse.Data.Data.Overview }
                                : extendedEpisode;
                        }

                        return default;
                    })
                    .Where(episode => episode is not null)
                    .Select(episode => episode!);

                await foreach (var episode in extendedEpisodes
                    .Select(episode =>
                    {
                        var fullCharacters = episode.Characters is null
                            ? characters.ToList()
                            : episode.Characters.Concat(characters).ToList();

                        var fullCompanies = episode.Companies is null
                            ? companies.ToList()
                            : episode.Companies.Concat(companies).ToList();

                        return new RemoteEpisode(episode.Name, episode.Overview)
                        {
                            Season = episode.SeasonNumber,
                            Number = episode.Number,
                            Id = episode.ProductionCode,
                            Show = name,
                            ScreenWriters = GetWriters(fullCharacters, episode.Id),
                            Cast = GetCast(fullCharacters, episode.Id),
                            Directors = GetDirectors(fullCharacters, episode.Id),
                            Release = GetReleaseDate(episode.Aired),
                            Network = GetNetwork(fullCompanies),
                            Rating = GetRating(episode.ContentRatings, country),
                            ImageUri = GetImageUri(episode.Image),
                        };

                        static IEnumerable<string>? GetWriters(IEnumerable<Character>? characters, int episodeId)
                        {
                            return GetCharacters(characters, episodeId, "Writer").ToList();
                        }

                        static IEnumerable<string>? GetDirectors(IEnumerable<Character>? characters, int episodeId)
                        {
                            return GetCharacters(characters, episodeId, "Director").ToList();
                        }

                        static IEnumerable<string>? GetCast(IEnumerable<Character>? characters, int episodeId)
                        {
                            return GetCharacters(characters, episodeId, "Actor", "Guest Star");
                        }

                        static IEnumerable<string>? GetCharacters(IEnumerable<Character>? characters, int episodeId, params string[] peopleTypes)
                        {
                            return characters switch
                            {
                                null => default,
                                _ => characters
                                    .Where(character => Array.Exists(peopleTypes, peopleType =>
                                        string.Equals(character.PeopleType, peopleType, StringComparison.Ordinal))
                                        && character.PersonName is not null
                                        && (!character.EpisodeId.HasValue || character.EpisodeId.Value == episodeId))
                                    .Select(character => character.PersonName!)
                                    .Distinct(StringComparer.OrdinalIgnoreCase),
                            };
                        }

                        static string? GetNetwork(ICollection<Company> companies)
                        {
                            var company = companies.FirstOrDefault(company => string.Equals(company.CompanyType?.CompanyTypeName, "Network", StringComparison.Ordinal));
                            return company?.Name;
                        }

                        static Rating? GetRating(ICollection<ContentRating>? contentRatings, string? country)
                        {
                            if (contentRatings == null)
                            {
                                return default;
                            }

                            country = GetCountry(country);

                            var contentRating = contentRatings.FirstOrDefault(contentRating => string.Equals(contentRating.Country, country, StringComparison.OrdinalIgnoreCase));
                            return contentRating is null || contentRating.Name is null || contentRating.Country is null
                                ? default
                                : Rating.FindBest(contentRating.Name, GetCountry(contentRating.Country));

                            static string GetCountry(string? country)
                            {
                                return country switch
                                {
                                    "aus" => "AU",
                                    "AU" => "aus",
                                    "us" => "US",
                                    "US" => "us",
                                    null => "AU",
                                    _ => country,
                                };
                            }
                        }

                        static Uri? GetImageUri(string? imageUri)
                        {
                            return imageUri is null ? default : new Uri(imageUri);
                        }

                        static DateTime? GetReleaseDate(string? aired)
                        {
                            return aired switch
                            {
                                not null when DateTime.TryParse(aired, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var releaseDate) => releaseDate,
                                not null when DateTime.TryParse(aired, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out var releaseDate) => releaseDate,
                                _ => default,
                            };
                        }
                    })
                    .WithCancellation(cancellationToken)
                    .ConfigureAwait(false))
                {
                    yield return episode;
                }
            }
        }
    }

    private static async Task<TokenResponse?> GetTokenAsync(RestClient client, Uri baseUrl, string? pin, bool force, CancellationToken cancellationToken = default)
    {
        if (!force && await GetTokenFromFile(cancellationToken: cancellationToken).ConfigureAwait(false) is TokenResponse tokenFromFile)
        {
            return tokenFromFile;
        }

        if (await RequestToken(client, baseUrl, pin, cancellationToken).ConfigureAwait(false) is TokenResponse tokenFromWeb)
        {
            // write this to the local cache
            await WriteTokenToFile(tokenFromWeb, cancellationToken: cancellationToken).ConfigureAwait(false);
            return tokenFromWeb;
        }

        return default;

        static async Task<TokenResponse?> RequestToken(RestClient client, Uri baseUrl, string? pin, CancellationToken cancellationToken)
        {
            var tokenRequest = new RestRequest(CreateUri(baseUrl, "login"), Method.Post)
                .AddJsonBody(new TokenRequest { ApiKey = TheTVDbHelpers.ApiKey, Pin = pin });

            var response = await client.ExecutePostAsync<Response<TokenResponse>>(tokenRequest, cancellationToken).ConfigureAwait(false);
            return response.IsSuccessful ? response.Data?.Data : default;
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
            _ = Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            using var stream = File.OpenWrite(fileName);
            await System.Text.Json.JsonSerializer.SerializeAsync(stream, tokenResponse, Options, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        static string GenerateFileName()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Media.Metadata", "TheTVDb.token.json");
        }
    }

    private static bool IsAuthorizationParameter(Parameter parameter) => parameter.Type == ParameterType.HttpHeader && string.Equals(parameter.Name, KnownHeaders.Authorization, StringComparison.Ordinal);

    private static Uri CreateUri(Uri baseUrl, string? resource) => baseUrl.MergeBaseUrlAndResource(resource);

    private async Task AddTokenAsync(RestClient client, Uri baseUrl, string pin, CancellationToken cancellationToken = default)
    {
        if (!client.DefaultParameters
            .Any(IsAuthorizationParameter))
        {
            this.tokenResponse ??= await GetTokenAsync(client, baseUrl, pin, force: false, cancellationToken).ConfigureAwait(false);
            if (this.tokenResponse is null)
            {
                throw new InvalidOperationException(Properties.Resources.FailedToGetTokenResponse);
            }

            _ = client.AddDefaultHeader(KnownHeaders.Authorization, $"Bearer {this.tokenResponse.Token}");
        }
    }

    private async Task UpdateTokenAsync(RestClient client, Uri baseUrl, string pin, CancellationToken cancellationToken = default)
    {
        this.tokenResponse = await GetTokenAsync(client, baseUrl, pin, force: true, cancellationToken).ConfigureAwait(false);
        if (this.tokenResponse is null)
        {
            throw new InvalidOperationException(Properties.Resources.FailedToGetTokenResponse);
        }

        // see if we need to
        var value = $"Bearer {this.tokenResponse.Token}";
        var parameters = client.DefaultParameters.Where(IsAuthorizationParameter).ToList();
        if (parameters.Exists(parameter => string.Equals(parameter.Value?.ToString(), value, StringComparison.Ordinal)))
        {
            return;
        }

        foreach (var parameter in parameters)
        {
            client.DefaultParameters.RemoveParameter(parameter);
        }

        _ = client.DefaultParameters.AddParameters(parameters.Select(parameter => Parameter.CreateParameter(parameter.Name, value, parameter.Type, parameter.Encode)));
    }

    private Uri CreateUri(string? resource) => CreateUri(this.baseUrl, resource);

    private async IAsyncEnumerable<SearchResult> SearchSeriesAsync(string name, int year, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await this.AddTokenAsync(this.client, this.baseUrl, this.pin, cancellationToken).ConfigureAwait(false);

        var request = new RestRequest(this.CreateUri("search"))
            .AddQueryParameter("query", name)
            .AddQueryParameter("type", "series");

        if (year > 0)
        {
            _ = request.AddQueryParameter("year", year.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        var response = await this.client.ExecuteGetAsync<Response<ICollection<SearchResult>>>(request, cancellationToken).ConfigureAwait(false);
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            await this.UpdateTokenAsync(this.client, this.baseUrl, this.pin, cancellationToken).ConfigureAwait(false);
            response = await this.client.ExecuteGetAsync<Response<ICollection<SearchResult>>>(request, cancellationToken).ConfigureAwait(false);
        }

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
    private sealed record class Response<T>
    {
        public T? Data { get; init; }

        public string? Status { get; init; }
    }

    private sealed record class TokenRequest
    {
        public string? ApiKey { get; init; }

        public string? Pin { get; init; }
    }

    private sealed record class TokenResponse
    {
        public string? Token { get; init; }
    }

    private sealed record class TranslationSimple
    {
        public string? Language { get; init; }

        public string? Description { get; init; }
    }

    private sealed record class RemoteId
    {
        public string? Id { get; init; }

        public long Type { get; init; }

        public string? SourceName { get; init; }
    }

    private sealed record class SearchResult
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

        public IDictionary<string, string>? Translations { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("first_air_time")]
        public string? FirstAirTime { get; init; }

        public string? Overview { get; init; }

        public IDictionary<string, string>? Overviews { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("overview_translated")]
        public ICollection<string>? OverviewTranslated { get; init; }

        public string? Primary_language { get; init; }

        public string? Status { get; init; }

        public string? Type { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("tvdb_id")]
        public string? TvDbId { get; init; }

        public string? Year { get; init; }

        public string? Slug { get; init; }

        public string? Network { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("remote_ids")]
        public ICollection<RemoteId>? RemoteIds { get; init; }

        public string? Thumbnail { get; init; }
    }

    private sealed record class SeriesExtendedRecord
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

        public int? AverageRuntime { get; init; }

        public ICollection<SeasonBaseRecord>? Seasons { get; init; }

        public ICollection<Character>? Characters { get; init; }

        public ICollection<Company>? Companies { get; init; }
    }

    private sealed record class Alias
    {
        public string? Language { get; init; }

        public string? Name { get; init; }
    }

    private sealed record class Status
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public string? RecordType { get; init; }

        public bool KeepUpdated { get; init; }
    }

    private record class SeasonBaseRecord
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

    private sealed record class SeasonType
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public string? Type { get; init; }
    }

    private sealed record class SeasonExtendedRecord : SeasonBaseRecord
    {
        public ICollection<EpisodeBaseRecord>? Episodes { get; init; }
    }

    private record EpisodeBaseRecord
    {
        public int Id { get; init; }

        public int SeriesId { get; init; }

        public string? Aired { get; init; }

        public int? Runtime { get; init; }

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

    private sealed record EpisodeExtendedRecord : EpisodeBaseRecord
    {
        public int? AirsAfterSeason { get; init; }

        public int? AirsBeforeEpisode { get; init; }

        public int? AirsBeforeSeason { get; init; }

        ////public ICollection<AwardBaseRecord> Awards { get; init; }

        public ICollection<Character>? Characters { get; init; }

        public ICollection<ContentRating>? ContentRatings { get; init; }

        public ICollection<Company>? Companies { get; init; }

        public string? ProductionCode { get; init; }

        public ICollection<RemoteId>? RemoteIds { get; init; }

        public ICollection<SeasonBaseRecord>? Seasons { get; init; }

        ////public ICollection<TagOption> TagOptions { get; init; }

        ////public ICollection<Trailer> Trailers { get; init; }
    }

    private sealed record Character
    {
        public ICollection<string>? Aliases { get; init; }

        public int? EpisodeId { get; init; }

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

    private sealed record Company
    {
        public int Id { get; init; }

        public string? Slug { get; init; }

        public string? Name { get; init; }

        public ICollection<string>? NameTranslations { get; init; }

        public string? Overview { get; init; }

        public ICollection<string>? OverviewTranslations { get; init; }

        public ICollection<Alias>? Aliases { get; init; }

        public string? Country { get; init; }

        public int PrimaryCompanyType { get; init; }

        public string? ActiveDate { get; init; }

        public string? InactiveDate { get; init; }

        public CompanyType? CompanyType { get; init; }
    }

    private sealed record CompanyType
    {
        public int CompanyTypeId { get; init; }

        public string? CompanyTypeName { get; init; }
    }

    private sealed record ContentRating
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public string? Country { get; init; }

        public string? ContentType { get; init; }

        public int Order { get; init; }

        public string? FullName { get; init; }
    }

    private sealed record Translation
    {
        public ICollection<string>? Aliases { get; init; }

        public bool IsAlias { get; init; }

        public bool IsPrimary { get; init; }

        public string? Language { get; init; }

        public string? Name { get; init; }

        public string? Overview { get; init; }

        public string? Tagline { get; init; }
    }
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore S3459 // Unassigned members should be removed

    private sealed class LowerCaseJsonNamingPolicy : System.Text.Json.JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLowerInvariant();
    }
}