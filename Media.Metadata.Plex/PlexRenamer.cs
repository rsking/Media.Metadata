// -----------------------------------------------------------------------
// <copyright file="PlexRenamer.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Plex;

/// <summary>
/// The <c>PLEX</c> <see cref="IRenamer"/>.
/// </summary>
public class PlexRenamer : IRenamer
{
    private static char[]? invalidFileNameChars;
    private static char[]? invalidPathChars;

    /// <inheritdoc/>
    public string? GetFileName(string current, Video video)
    {
        return video switch
        {
            Movie movie => GetMovieName(current, movie),
            Episode episode => GetEpisodeName(current, episode),
            _ => default,
        };

        static string AddPart(string name, string part)
        {
            return $"{name} - {part}";
        }

        static string GetMovieName(string current, Movie movie)
        {
            return movie switch
            {
                { Name: null } => throw new InvalidOperationException("Cannot rename movie with a null name."),
                { Release: null } => throw new InvalidOperationException($"Cannot rename movie with no release year. {movie.Name}"),
                _ => GetMovieNameCore(current, movie, movie.Name, movie.Release.Value),
            };

            static string GetMovieNameCore(string current, Movie movie, string movieName, DateTime release)
            {
                // copy this as a movie
                var name = FormattableString.Invariant($"{movieName.Sanitise()} ({release.Year})");
                if (movie is { Edition: { } edition })
                {
                    name += $" {{edition-{edition}}}";
                }

                if (movie is { Work: { } work } && work.Trim() is { Length: not 0 } trimmedWork)
                {
                    name = AddPart(name, trimmedWork);
                }

                return Path.Combine(Path.Combine("Movies", name).ReplaceAll(GetInvalidPathChars()), (name + Path.GetExtension(current)).ReplaceAll(GetInvalidFileNameChars()));
            }
        }

        static string GetEpisodeName(string current, Episode episode)
        {
            return episode switch
            {
                { Show: null } => throw new InvalidOperationException("Cannot rename episode with no show name."),
                { Season: null } => throw new InvalidOperationException("Cannot rename episode with no season."),
                { Number: null } => throw new InvalidOperationException("Cannot rename episode with no number"),
                _ => GetEpisodeNameCore(current, episode, episode.Show.Sanitise(), episode.Season.Value, episode.Number.Value),
            };

            static string GetEpisodeNameCore(string current, Episode episode, string showName, int seasonNumber, int episodeNumber)
            {
                var name = FormattableString.Invariant($"{showName} - s{seasonNumber:00}e{episodeNumber:00}");
                name = episode switch
                {
                    { Part: { } part } => AddPart(name, FormattableString.Invariant($"{nameof(part)}{part}")),
                    { Name: { } episodeName } => AddPart(name, episodeName),
                    _ => throw new InvalidOperationException("Cannot rename episode with no name or part"),
                };

                if (episode.Work is { } work)
                {
                    name = AddPart(name, work);
                }

                return Path.Combine(Path.Combine("TV Shows", showName, FormattableString.Invariant($"Season {seasonNumber:00}")).ReplaceAll(GetInvalidPathChars()), (name + Path.GetExtension(current)).ReplaceAll(GetInvalidFileNameChars()));
            }
        }
    }

    private static char[] GetInvalidFileNameChars()
    {
        return invalidFileNameChars ??=
        [
            '\"',
            '<',
            '>',
            '|',
            '\0',
            (char)1,
            (char)2,
            (char)3,
            (char)4,
            (char)5,
            (char)6,
            (char)7,
            (char)8,
            (char)9,
            (char)10,
            (char)11,
            (char)12,
            (char)13,
            (char)14,
            (char)15,
            (char)16,
            (char)17,
            (char)18,
            (char)19,
            (char)20,
            (char)21,
            (char)22,
            (char)23,
            (char)24,
            (char)25,
            (char)26,
            (char)27,
            (char)28,
            (char)29,
            (char)30,
            (char)31,
            ':',
            '*',
            '?',
            '\\',
            '/',
        ];
    }

    private static char[] GetInvalidPathChars()
    {
        return invalidPathChars ??=
            [
                '\"',
                '<',
                '>',
                '|',
                '\0',
                (char)1,
                (char)2,
                (char)3,
                (char)4,
                (char)5,
                (char)6,
                (char)7,
                (char)8,
                (char)9,
                (char)10,
                (char)11,
                (char)12,
                (char)13,
                (char)14,
                (char)15,
                (char)16,
                (char)17,
                (char)18,
                (char)19,
                (char)20,
                (char)21,
                (char)22,
                (char)23,
                (char)24,
                (char)25,
                (char)26,
                (char)27,
                (char)28,
                (char)29,
                (char)30,
                (char)31,
                ':',
                '*',
                '?',
            ];
    }
}