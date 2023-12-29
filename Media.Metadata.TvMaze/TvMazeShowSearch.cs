// -----------------------------------------------------------------------
// <copyright file="TvMazeShowSearch.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.TvMaze;

using RestSharp;

/// <summary>
/// The TV Maze show search.
/// </summary>
/// <remarks>
/// Initialises a new instance of the <see cref="TvMazeShowSearch"/> class.
/// </remarks>
/// <param name="restClient">The rest client.</param>
/// <param name="options">The options.</param>
public class TvMazeShowSearch(RestClient restClient, Microsoft.Extensions.Options.IOptions<TvMazeOptions> options) : IShowSearch
{
    private readonly RestClient client = restClient;

    private readonly Uri baseUrl = new(options.Value.Url);

    /// <inheritdoc/>
    public async IAsyncEnumerable<Series> SearchAsync(string name, int year = 0, string country = "AU", [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var series in this.SearchSeriesAsync(name, cancellationToken).ConfigureAwait(false))
        {
            if (series.Show is null)
            {
                continue;
            }

            var castLock = new NeoSmart.AsyncLock.AsyncLock();
            ICollection<Cast>? cast = default;

            var crewLock = new NeoSmart.AsyncLock.AsyncLock();
            ICollection<Crew>? crew = default;

            yield return new RemoteSeries(series.Show.Name, GetSeasons(series.Show, this.client, this.baseUrl, GetCast, GetCrew, cancellationToken).ToEnumerable())
            {
                ImageUri = series.Show.Image?.Original,
            };

            static async IAsyncEnumerable<RemoteSeason> GetSeasons(Show show, RestClient client, Uri baseUrl, Func<Task<ICollection<Cast>>> getCast, Func<Task<ICollection<Crew>>> getCrew, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
            {
                var request = new RestRequest(CreateUri(baseUrl, "shows/{id}/seasons"))
                    .AddUrlSegment("id", show.Id);

                var response = await client.ExecuteGetAsync<ICollection<Season>>(request, cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessful && response.Data is not null)
                {
                    foreach (var season in response.Data)
                    {
                        yield return new RemoteSeason(season.Number, GetEpisodes(show, season, client, baseUrl, getCast, getCrew, cancellationToken).ToEnumerable())
                        {
                            ImageUri = season.Image?.Original,
                        };
                    }
                }
                else if (response.ErrorException is not null)
                {
                    throw response.ErrorException;
                }

                static async IAsyncEnumerable<RemoteEpisode> GetEpisodes(Show show, Season season, RestClient client, Uri baseUrl, Func<Task<ICollection<Cast>>> getCast, Func<Task<ICollection<Crew>>> getCrew, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
                {
                    var request = new RestRequest(CreateUri(baseUrl, "seasons/{id}/episodes"))
                        .AddUrlSegment("id", season.Id);

                    var response = await client.ExecuteGetAsync<ICollection<Episode>>(request, cancellationToken).ConfigureAwait(false);

                    if (response.IsSuccessful && response.Data is not null)
                    {
                        foreach (var episode in response.Data)
                        {
                            if (episode.Number.HasValue)
                            {
                                var cast = await getCast().ConfigureAwait(false);
                                var crew = await getCrew().ConfigureAwait(false);

                                yield return new RemoteEpisode(episode.Name, episode.Summary)
                                {
                                    Season = season.Number,
                                    Number = episode.Number.Value,
                                    Show = show.Name,
                                    ImageUri = episode.Image?.Original,
                                    Cast = cast.Where(c => c.Person is not null).Select(c => c.Person!.Name),
                                    Composers = crew.Where(c => c.Person is not null && string.Equals(c.Type, "composer", StringComparison.OrdinalIgnoreCase)).Select(c => c.Person!.Name),
                                    Directors = crew.Where(c => c.Person is not null && string.Equals(c.Type, "director", StringComparison.OrdinalIgnoreCase)).Select(c => c.Person!.Name),
                                    Producers = crew.Where(c => c.Person is not null && string.Equals(c.Type, "producer", StringComparison.OrdinalIgnoreCase)).Select(c => c.Person!.Name),
                                    ScreenWriters = crew.Where(c => c.Person is not null && string.Equals(c.Type, "screenwriter", StringComparison.OrdinalIgnoreCase)).Select(c => c.Person!.Name),
                                };
                            }
                        }
                    }
                    else if (response.ErrorException is not null)
                    {
                        throw response.ErrorException;
                    }
                }
            }

            async Task<ICollection<Cast>> GetCast()
            {
                using (await castLock.LockAsync(cancellationToken).ConfigureAwait(false))
                {
                    if (cast is not null)
                    {
                        return cast;
                    }

                    var request = new RestRequest(this.CreateUri("shows/{id}/cast"))
                        .AddUrlSegment("id", series.Show.Id);

                    return cast = await this.client.GetResponseOrThrow<ICollection<Cast>>(request, cancellationToken).ConfigureAwait(false);
                }
            }

            async Task<ICollection<Crew>> GetCrew()
            {
                using (await crewLock.LockAsync(cancellationToken).ConfigureAwait(false))
                {
                    if (crew is not null)
                    {
                        return crew;
                    }

                    var request = new RestRequest(this.CreateUri("shows/{id}/crew"))
                        .AddUrlSegment("id", series.Show.Id);

                    return crew = await this.client.GetResponseOrThrow<ICollection<Crew>>(request, cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }

    private static Uri CreateUri(Uri baseUrl, string? resource) => baseUrl.MergeBaseUrlAndResource(resource);

    private async IAsyncEnumerable<SearchResult> SearchSeriesAsync(string name, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(this.CreateUri("search/shows"))
            .AddQueryParameter("q", name);

        var response = await this.client.ExecuteGetAsync<ICollection<SearchResult>>(request, cancellationToken).ConfigureAwait(false);

        if (response.IsSuccessful && response.Data is not null)
        {
            foreach (var searchResult in response.Data)
            {
                yield return searchResult;
            }
        }
        else if (response.ErrorException is not null)
        {
            throw response.ErrorException;
        }
    }

    private Uri CreateUri(string? resource) => CreateUri(this.baseUrl, resource);

#pragma warning disable S1144, S3459
    private sealed record class SearchResult
    {
        public double Score { get; set; }

        public Show? Show { get; set; }
    }

    private sealed record class Show
    {
        public int Id { get; set; }

        public Uri? Url { get; set; }

        public string Name { get; set; } = default!;

        public string? Type { get; set; }

        public string? Languages { get; set; }

        public ICollection<string>? Genres { get; init; }

        public string? Status { get; set; }

        public int? Runtime { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("averageRuntime")]
        public int? AverageRuntime { get; set; }

        public DateTime? Premiered { get; set; }

        public DateTime? Ended { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("officialSite")]
        public Uri? OfficialSite { get; set; }

        public Schedule? Schedule { get; set; }

        public Rating? Rating { get; set; }

        public int? Weight { get; set; }

        public Network? Network { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("webChannel")]
        public WebChannel? WebChannel { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("dvdCountry")]
        public string? DvdCountry { get; set; }

        public Externals? Externals { get; set; }

        public Image? Image { get; set; }

        public string? Summary { get; set; }

        public int Updated { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("_links")]
        public Links? Links { get; set; }
    }

    private sealed record class Schedule
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(TimeConverter))]
        public TimeSpan Time { get; set; }

        public ICollection<string>? Days { get; init; }
    }

    private sealed record class Rating
    {
        public double? Average { get; set; }
    }

    private sealed record class Network
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public Country? Country { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("officialSite")]
        public Uri? OfficialSite { get; set; }
    }

    private sealed record class Country
    {
        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? TimeZone { get; set; }
    }

    private sealed record class Externals
    {
        public int? TvRage { get; set; }

        public int? TheTVDb { get; set; }

        public string? IMDb { get; set; }
    }

    private sealed record class Image
    {
        public Uri? Medium { get; set; }

        public Uri? Original { get; set; }
    }

    private sealed record class Links
    {
        public Link Self { get; set; } = default!;

        public Link? PreviousEpisode { get; set; }
    }

    private sealed record class Link
    {
        public Uri? Href { get; set; }
    }

    private sealed record class WebChannel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public Country? Country { get; set; }
    }

    private sealed record class Season
    {
        public int Id { get; set; }

        public Uri? Url { get; set; }

        public int Number { get; set; }

        public string Name { get; set; } = default!;

        [System.Text.Json.Serialization.JsonPropertyName("episodeOrder")]
        public int EpisodeOrder { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("premiereDate")]
        public DateTime? PremiereDate { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }

        public Network? Network { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("webChannel")]
        public WebChannel? WebChannel { get; set; }

        public Image? Image { get; set; }

        public string? Summary { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("_links")]
        public Links? Links { get; set; }
    }

    private sealed record class Episode
    {
        public int Id { get; set; }

        public Uri? Url { get; set; }

        public string Name { get; set; } = default!;

        public int Season { get; set; }

        public int? Number { get; set; }

        public string? Type { get; set; }

        public DateTime? AirDate { get; set; }

        [System.Text.Json.Serialization.JsonConverter(typeof(TimeConverter))]
        public TimeSpan? AirTime { get; set; }

        // "airstamp":"1996-05-11T01:00:00+00:00"
        public DateTime AirStamp { get; set; }

        public int? Runtime { get; set; }

        public Rating? Rating { get; set; }

        public Image? Image { get; set; }

        public string? Summary { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("_links")]
        public Links? Links { get; set; }
    }

    private sealed record class Cast
    {
        public Person? Person { get; set; }

        public Character? Character { get; set; }

        public bool Self { get; set; }

        public bool Voice { get; set; }
    }

    private sealed record class Person
    {
        public int Id { get; set; }

        public Uri? Url { get; set; }

        public string Name { get; set; } = default!;

        public Country? Country { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? Deathday { get; set; }

        public string? Gender { get; set; }

        public Image? Image { get; set; }

        public int Update { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("_links")]
        public Links? Links { get; set; }
    }

    private sealed record class Character
    {
        public int Id { get; set; }

        public Uri? Url { get; set; }

        public string Name { get; set; } = default!;

        public Image? Image { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("_links")]
        public Links? Links { get; set; }
    }

    private sealed record class Crew
    {
        public string? Type { get; set; }

        public Person? Person { get; set; }
    }
#pragma warning restore S1144, S3459

    private sealed class TimeConverter : System.Text.Json.Serialization.JsonConverter<TimeSpan>
    {
        private const string Format = "hh\\:mm";

        public override TimeSpan Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options) => TimeSpan.TryParseExact(reader.GetString(), Format, System.Globalization.CultureInfo.InvariantCulture, out var timeSpan)
            ? timeSpan
            : default;

        public override void Write(System.Text.Json.Utf8JsonWriter writer, TimeSpan value, System.Text.Json.JsonSerializerOptions options) => writer.WriteStringValue(value.ToString(Format, System.Globalization.CultureInfo.InvariantCulture));
    }
}