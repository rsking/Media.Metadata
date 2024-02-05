// -----------------------------------------------------------------------
// <copyright file="PlexRenamer.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Plex;

public class PlexRenamer : IRenamer
{
    private static char[]? invalidFileNameChars;
    private static char[]? invalidPathChars;

    /// <summary>
    /// Initialises a new instance of the <see cref="PlexRenamer"/> class.
    /// </summary>
    public PlexRenamer()
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="PlexRenamer"/> class.
    /// </summary>
    /// <param name="path">The videos path.</param>
    public PlexRenamer(string path)
        : this(path, path)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="PlexRenamer"/> class.
    /// </summary>
    /// <param name="moviesPath">The movies path.</param>
    /// <param name="tvShowsPath">The TV shows path.</param>
    public PlexRenamer(string moviesPath, string tvShowsPath)
    {
        this.MoviesPath = moviesPath;
        this.TVShowsPath = tvShowsPath;
    }

    /// <summary>
    /// Gets or sets the movies path.
    /// </summary>
    public string MoviesPath { get; set; }

    /// <summary>
    /// Gets or sets the TV Shows path.
    /// </summary>
    public string TVShowsPath { get; set; }

    /// <inheritdoc/>
    public string? GetFileName(string current, Video video)
    {
        FileInfo? path = default;
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
            var name = $"{movie.Name.Sanitise()} ({movie.Release?.Year})";
            if (movie.Edition is { } edition)
            {
                name += " {edition-";
                name += edition;
                name += "}";
            }

            var directory = new DirectoryInfo(Path.Combine(this.MoviesPath, "Movies", name)).GetName(GetInvalidPathChars());

            if (movie.Work is { } work)
            {
                work = work.Trim();
                if (work.Length != 0)
                {
                    name += " - ";
                    name += work;
                }
            }

            path = new FileInfo(Path.Combine(directory, (name + Path.GetExtension(current)).ReplaceAll(GetInvalidFileNameChars())));
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

            var directory = new DirectoryInfo(Path.Combine(this.TVShowsPath, "TV Shows", showName, $"Season {seasonNumber:00}")).GetName(GetInvalidPathChars());
            var name = $"{showName} - s{seasonNumber:00}e{episodeNumber:00}";
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

            path = new FileInfo(Path.Combine(directory, (name + Path.GetExtension(current)).ReplaceAll(GetInvalidFileNameChars())));
        }

        return path?.FullName;
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