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
        if (video is Movie movie)
        {
            if (movie.Name is null)
            {
                throw new InvalidOperationException("Cannot rename movie with a null name.");
            }

            if (movie.Release is null)
            {
                throw new InvalidOperationException($"Cannot rename movie with not release year. {movie.Name}");
            }

            // copy this as a movie
            var name = FormattableString.Invariant($"{movie.Name.Sanitise()} ({movie.Release?.Year})");
            if (movie.Edition is { } edition)
            {
                name += " {edition-";
                name += edition;
                name += "}";
            }

            var directory = new DirectoryInfo(Path.Combine("Movies", name)).GetName(GetInvalidPathChars());

            if (movie.Work is { } work)
            {
                work = work.Trim();
                if (work.Length != 0)
                {
                    name += " - ";
                    name += work;
                }
            }

            return Path.Combine(directory, (name + Path.GetExtension(current)).ReplaceAll(GetInvalidFileNameChars()));
        }
        else if (video is Episode episode)
        {
            if (episode.Show is null)
            {
                throw new InvalidOperationException("Cannot rename episode with no show name.");
            }

            if (episode.Season is null)
            {
                throw new InvalidOperationException("Cannot rename episode with no season.");
            }

            if (episode.Number is null)
            {
                throw new InvalidOperationException("Cannot rename episode with no number");
            }

            var showName = episode.Show.Sanitise();
            var seasonNumber = episode.Season;
            var episodeNumber = episode.Number;

            var directory = Path.Combine("TV Shows", showName, FormattableString.Invariant($"Season {seasonNumber:00}")).ReplaceAll(GetInvalidPathChars());
            var name = FormattableString.Invariant($"{showName} - s{seasonNumber:00}e{episodeNumber:00}");
            if (episode.Part is { } part)
            {
                // This is part of an episode
                name += " - ";
                name += nameof(part);
                name += part;
            }
            else if (episode.Name is { } episodeName)
            {
                // This is a single episode
                name += " - ";
                name += episodeName.Sanitise();
            }
            else
            {
                throw new InvalidOperationException("Cannot rename episode with no name or part");
            }

            if (episode.Work is { } work)
            {
                name += " - ";
                name += work;
            }

            return Path.Combine(directory, (name + Path.GetExtension(current)).ReplaceAll(GetInvalidFileNameChars()));
        }

        return default;
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