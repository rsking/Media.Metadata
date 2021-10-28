// -----------------------------------------------------------------------
// <copyright file="TMDbMovieSearch.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.TMDb;

/// <summary>
/// The Movie DB move search.
/// </summary>
public class TMDbMovieSearch : IMovieSearch
{
    /// <inheritdoc/>
    public async IAsyncEnumerable<Movie> SearchAsync(string name, int year = 0, string country = "AU", [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var client = new TMDbLib.Client.TMDbClient("b866acbf3519560a3879b55d2d9cb1cb");
        var movies = await client.SearchMovieAsync(name, year: year, cancellationToken: cancellationToken).ConfigureAwait(false);

        foreach (var result in movies.Results)
        {
            var movie = await client.GetMovieAsync(result.Id, TMDbLib.Objects.Movies.MovieMethods.Credits | TMDbLib.Objects.Movies.MovieMethods.Releases | TMDbLib.Objects.Movies.MovieMethods.Images, cancellationToken).ConfigureAwait(false);
            if (!client.HasConfig)
            {
                _ = await client.GetConfigAsync().ConfigureAwait(false);
            }

            var poster = movie.Images?.Posters?.FirstOrDefault();
            Rating? rating = default;
            Rating? invariantRating = default;
            Rating? usRating = default;

            foreach (var cty in movie.Releases.Countries)
            {
                if (!invariantRating.HasValue)
                {
                    invariantRating = Rating.FindBest(cty.Certification, cty.Iso_3166_1);
                }

                if (country != null && string.Equals(cty.Iso_3166_1, country, StringComparison.InvariantCultureIgnoreCase))
                {
                    rating = Rating.FindBest(cty.Certification, cty.Iso_3166_1);
                }

                if (string.Equals(cty.Iso_3166_1, "us", StringComparison.InvariantCultureIgnoreCase))
                {
                    usRating = Rating.FindBest(cty.Certification, cty.Iso_3166_1);
                }
            }

            yield return new Movie(
                movie.Title,
                movie.Overview,
                GetProducers(movie.Credits?.Crew),
                GetDirectors(movie.Credits?.Crew),
                movie.ProductionCompanies?.Select(productionCompany => productionCompany.Name) ?? Enumerable.Empty<string>(),
                movie.Genres?.Select(genre => genre.Name) ?? Enumerable.Empty<string>(),
                GetWriters(movie.Credits?.Crew),
                movie.Credits?.Cast?.Select(cast => cast.Name) ?? Enumerable.Empty<string>(),
                GetComposers(movie.Credits?.Crew))
            {
                Release = movie.ReleaseDate,
                Rating = rating ?? usRating ?? invariantRating,
                ImageUri = poster is null ? null : client.GetImageUrl(GetBestPosterSize(client.Config) ?? "original", poster.FilePath),
            };

            static IEnumerable<string> GetProducers(IEnumerable<TMDbLib.Objects.General.Crew>? crew)
            {
                return GetCrew(crew, crew => string.Equals(crew.Department, "Production", StringComparison.OrdinalIgnoreCase) && (string.Equals(crew.Job, "Producer", StringComparison.OrdinalIgnoreCase) || string.Equals(crew.Job, "Executive Producer", StringComparison.OrdinalIgnoreCase)));
            }

            static IEnumerable<string> GetDirectors(IEnumerable<TMDbLib.Objects.General.Crew>? crew)
            {
                return GetCrew(crew, crew => string.Equals(crew.Department, "Directing", StringComparison.OrdinalIgnoreCase) && string.Equals(crew.Job, "Director", StringComparison.OrdinalIgnoreCase));
            }

            static IEnumerable<string> GetWriters(IEnumerable<TMDbLib.Objects.General.Crew>? crew)
            {
                return GetCrew(crew, crew => string.Equals(crew.Department, "Writing", StringComparison.OrdinalIgnoreCase) && (string.Equals(crew.Job, "Screenplay", StringComparison.OrdinalIgnoreCase) || string.Equals(crew.Job, "Author", StringComparison.OrdinalIgnoreCase)));
            }

            static IEnumerable<string> GetComposers(IEnumerable<TMDbLib.Objects.General.Crew>? crew)
            {
                return GetCrew(crew, crew => string.Equals(crew.Department, "Sound", StringComparison.OrdinalIgnoreCase) && string.Equals(crew.Job, "Original Music Composer", StringComparison.OrdinalIgnoreCase));
            }

            static IEnumerable<string> GetCrew(IEnumerable<TMDbLib.Objects.General.Crew>? crew, Predicate<TMDbLib.Objects.General.Crew> predicate)
            {
                return crew is null
                    ? Enumerable.Empty<string>()
                    : crew.Where(crew => predicate(crew)).Select(crew => crew.Name);
            }
        }
    }

    private static string GetBestPosterSize(TMDbLib.Objects.General.TMDbConfig confguration)
    {
        return confguration.Images.PosterSizes.OrderByDescending(value => value, new SizeComparer()).FirstOrDefault();
    }

    private sealed class SizeComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (int.TryParse(x.TrimStart('w'), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var valueX))
            {
                if (int.TryParse(y.TrimStart('w'), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var valueY))
                {
                    return valueX.CompareTo(valueY);
                }

                // x is valid but y is not
                return 1;
            }

            if (int.TryParse(y.TrimStart('w'), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out _))
            {
                // y is valid but x is not
                return -1;
            }

            return 0;
        }
    }
}