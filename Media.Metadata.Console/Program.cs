// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using Media.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Hosting;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var searchCommand = new Command("search")
{
    CreateSearchMovie(),
    CreateSearchShow(),
};

var readCommand = new Command("read")
{
    CreateReadMovie(),
    CreateReadEpisode(),
};

var langOption = new Option<string[]>(new[] { "--lang", "-l" }, "`[tkID=]LAN` Set the language. LAN is the ISO 639 code (eng, swe, ...). If no track ID is given, sets language to all tracks")
{
    Arity = ArgumentArity.OneOrMore,
};

var updateCommand = new Command("update")
{
    CreateUpdateMovie(langOption),
    CreateUpdateEpisode(langOption),
    CreateUpdateVideo(langOption),
};

updateCommand.AddGlobalOption(langOption);

var commandBuilder = new CommandLineBuilder(new RootCommand
{
    searchCommand,
    readCommand,
    updateCommand,
});

await commandBuilder
    .UseDefaults()
    .UseHost(
        Host.CreateDefaultBuilder,
        configure => configure.ConfigureServices((builder, services) =>
        {
            services
                .AddTMDb()
                .AddTheTVDB(builder.Configuration)
                .AddMp4v2(Path.PathSeparator)
                .AddTagLib();

            services
                .Configure<InvocationLifetimeOptions>(options => options.SuppressStatusMessages = true);
        }))
    .CancelOnProcessTermination()
    .Build()
    .InvokeAsync(args)
    .ConfigureAwait(true);

static Command CreateSearchMovie()
{
    var nameArgument = new Argument<string>("name");
    var yearOption = new Option<int?>(new[] { "--year", "-y" }, "The movie year");
    var command = new Command("movie")
    {
        nameArgument,
        yearOption,
    };

    command.SetHandler(
        async (IHost host, string name, int? year, CancellationToken cancellationToken) =>
        {
            foreach (var search in host.Services.GetServices<IMovieSearch>())
            {
                await foreach (var movie in search.SearchAsync(name, year ?? 0, cancellationToken: cancellationToken).ConfigureAwait(false))
                {
                    Console.WriteLine("{0} - {1:yyyy-MM-dd}", movie.Name, movie.Release);
                    Console.WriteLine(movie.ToString());

                    if (string.Equals(movie.Name, name, StringComparison.Ordinal)
                        && (year == 0 || (movie.Release.HasValue && movie.Release.Value.Year == year)))
                    {
                        break;
                    }
                }
            }
        },
        nameArgument,
        yearOption);

    return command;
}

static Command CreateReadMovie()
{
    var pathArgument = new Argument<FileInfo>("path").ExistingOnly();
    var command = new Command("movie")
    {
        pathArgument,
    };

    command.SetHandler(
        (IHost host, FileInfo path) =>
        {
            if (!path.Exists)
            {
                return;
            }

            var reader = host.Services.GetRequiredService<IReader>();
            var movie = reader.ReadMovie(path.FullName);
            Console.WriteLine(movie.Name);
        },
        pathArgument);

    return command;
}

static Command CreateReadEpisode()
{
    var pathArgument = new Argument<FileInfo>("path").ExistingOnly();
    var command = new Command("episode")
    {
        pathArgument,
    };

    command.SetHandler(
        (IHost host, FileInfo path) =>
        {
            if (!path.Exists)
            {
                return;
            }

            var reader = host.Services.GetRequiredService<IReader>();
            var episode = reader.ReadEpisode(path.FullName);
            Console.WriteLine(episode.Name);
        },
        pathArgument);

    return command;
}

static Command CreateSearchShow()
{
    var nameArgument = new Argument<string>("name");
    var yearOption = new Option<int>(new[] { "--year", "-y" }, "The show year");
    var command = new Command("show")
    {
        nameArgument,
        yearOption,
    };

    command.SetHandler(
        async (IHost host, string name, int year, CancellationToken cancellationToken) =>
        {
            foreach (var search in host.Services.GetServices<IShowSearch>())
            {
                await foreach (var show in search.SearchAsync(name, year, cancellationToken: cancellationToken).ConfigureAwait(false))
                {
                    Console.WriteLine("{0}", show.Name);
                    foreach (var season in show.Seasons.OrderBy(season => season.Number))
                    {
                        Console.WriteLine("\tSeason {0}", season.Number);
                        if (season.Episodes is null)
                        {
                            continue;
                        }

                        foreach (var episode in season.Episodes)
                        {
                            Console.WriteLine("\t\t{0}: {1}", episode.Name, episode.Description);
                        }
                    }
                }
            }
        },
        nameArgument,
        yearOption);

    return command;
}

static Command CreateUpdateMovie(System.CommandLine.Binding.IValueDescriptor langOption)
{
    var pathArgument = new Argument<FileInfo>("path").ExistingOnly();
    var nameArgument = new Argument<string>("name");
    var yearOption = new Option<int>(new[] { "--year", "-y" }, "The movie year");
    var command = new Command("movie")
    {
        pathArgument,
        nameArgument,
        yearOption,
    };

    command.SetHandler(
        async (IConsole console, IHost host, FileInfo path, string name, int year, string[]? lang, CancellationToken cancellationToken) =>
        {
            if (!path.Exists)
            {
                return;
            }

            foreach (var search in host.Services.GetServices<IMovieSearch>())
            {
                await foreach (var movie in search.SearchAsync(name, year, cancellationToken: cancellationToken).ConfigureAwait(false))
                {
                    if (string.Equals(movie.Name, name, StringComparison.OrdinalIgnoreCase) && (year == 0 || (movie.Release.HasValue && movie.Release.Value.Year == year)))
                    {
                        console.Out.WriteLine($"Found Movie {movie.Name} ({movie.Release?.Year})");
                        var updater = host.Services.GetRequiredService<IUpdater>();
                        console.Out.WriteLine($"Saving {path.Name}");
                        updater.UpdateMovie(path.FullName, movie, GetLanguages(lang));
                        console.Out.WriteLine($"Saved {path.Name}");
                        break;
                    }
                }
            }
        },
        pathArgument,
        nameArgument,
        yearOption,
        langOption);

    return command;
}

static Command CreateUpdateEpisode(System.CommandLine.Binding.IValueDescriptor langOption)
{
    var pathArgument = new Argument<FileInfo[]>("path", ParseFileInfo);
    var nameOption = new Option<string>(new[] { "--name", "-n" }, "The series name") { IsRequired = true };
    var yearOption = new Option<int>(new[] { "--year", "-y" }, () => -1, "The series year");
    var seasonOption = new Option<int>(new[] { "--season", "-s" }, () => -1, "The season number");
    var episodeOption = new Option<int>(new[] { "--episode", "-e" }, () => -1, "The episode number");
    var ignoreOption = new Option<bool>(new[] { "--ignore", "-i" }, "Ignore files that already have a valid episode");

    var command = new Command("episode")
    {
        pathArgument,
        nameOption,
        yearOption,
        seasonOption,
        episodeOption,
        ignoreOption,
    };

    command.SetHandler(
        async (IConsole console, IHost host, FileInfo[] paths, string name, int year, int season, int episode, bool ignore, string[]? lang, CancellationToken cancellationToken) =>
        {
            var regex = new[]
            {
                new System.Text.RegularExpressions.Regex("s(?<season>\\d{2})e(?<episode>\\d{2})", System.Text.RegularExpressions.RegexOptions.None, TimeSpan.FromSeconds(1)),
                new System.Text.RegularExpressions.Regex("S(?<season>\\d+) Ep(?<episode>\\d+)", System.Text.RegularExpressions.RegexOptions.None, TimeSpan.FromSeconds(1)),
            };

            var reader = host.Services.GetRequiredService<IReader>();
            var pathList = paths
                .Where(path => ShouldProcess(path, reader, ignore))
                .ToList();
            var updater = host.Services.GetRequiredService<IUpdater>();

            foreach (var search in host.Services.GetServices<IShowSearch>())
            {
                await foreach (var series in search.SearchAsync(name, year, cancellationToken: cancellationToken).ConfigureAwait(false))
                {
                    var seasons = pathList
                        .Where(path => path.Exists)
                        .GroupBy(
                            path => season switch
                            {
                                -1 when GetMatch(path.Name) is System.Text.RegularExpressions.Match match => int.Parse(match.Groups["season"].Value, System.Globalization.CultureInfo.CurrentCulture),
                                _ => season,
                            },
                            path => path)
                        .OrderBy(group => group.Key)
                        .ToList();

                    if (seasons.Count == 0)
                    {
                        return;
                    }

                    console.Out.WriteLine($"Found Series  {series.Name}");
                    var seasonEnumerator = series.Seasons.GetEnumerator();
                    if (!seasonEnumerator.MoveNext())
                    {
                        continue;
                    }

                    foreach (var seasonGroup in seasons)
                    {
                        while (seasonEnumerator.Current is not null && seasonEnumerator.Current.Number < seasonGroup.Key && seasonEnumerator.MoveNext())
                        {
                            // loop until we have the correct season.
                        }

                        if (seasonEnumerator.Current is not null && seasonEnumerator.Current.Number == seasonGroup.Key)
                        {
                            var s = seasonEnumerator.Current;
                            console.Out.WriteLine(FormattableString.CurrentCulture($"Found Season  {series.Name}:{s.Number}"));

                            var episoides = seasonGroup
                                .Select(path => episode switch
                                {
                                    -1 when GetMatch(path.Name) is System.Text.RegularExpressions.Match match => (Episode: int.Parse(match.Groups["episode"].Value, System.Globalization.CultureInfo.CurrentCulture), Path: path),
                                    _ => (Episode: episode, Path: path),
                                })
                                .OrderBy(ep => ep.Episode)
                                .ToList();

                            var episodeEnumerator = s.Episodes.GetEnumerator();

                            if (!episodeEnumerator.MoveNext())
                            {
                                continue;
                            }

                            foreach (var ep in episoides.Where(ep => ep.Path.Exists))
                            {
                                console.Out.WriteLine(FormattableString.CurrentCulture($"Processing {ep.Path.Name}"));

                                while (episodeEnumerator.Current is not null && episodeEnumerator.Current.Number < ep.Episode && episodeEnumerator.MoveNext())
                                {
                                    // loop until we have the correct episode.
                                }

                                var e = episodeEnumerator.Current;
                                if (e is not null && e.Number == ep.Episode)
                                {
                                    console.Out.WriteLine(FormattableString.CurrentCulture($"Found Episode {series.Name}:{s.Number}:{e.Name}"));
                                    updater.UpdateEpisode(ep.Path.FullName, e with { Image = s.Image ?? series.Image ?? e.Image }, GetLanguages(lang));

                                    // remove this from the list of paths
                                    pathList.Remove(ep.Path);
                                }
                            }
                        }
                    }
                }
            }

            System.Text.RegularExpressions.Match? GetMatch(string input)
            {
                foreach (var r in regex)
                {
                    if (r.Match(input) is System.Text.RegularExpressions.Match { Success: true } match)
                    {
                        return match;
                    }
                }

                return default;
            }

            static bool ShouldProcess(FileInfo path, IReader reader, bool ignore)
            {
                if (!path.Exists)
                {
                    return false;
                }

                if (!ignore)
                {
                    return true;
                }

                // read the file to see if it has an episode
                using var f = reader.ReadEpisode(path.FullName);
                return f.Show is null || f.Season < 0 || f.Number < 0;
            }
        },
        pathArgument,
        nameOption,
        yearOption,
        seasonOption,
        episodeOption,
        ignoreOption,
        langOption);

    return command;
}

static Command CreateUpdateVideo(System.CommandLine.Binding.IValueDescriptor langOption)
{
    var pathArgument = new Argument<FileInfo[]>("path", ParseFileInfo).ExistingOnly();
    var command = new Command("video")
    {
        pathArgument,
    };

    command.SetHandler(
        (IConsole console, IHost host, FileInfo[] path, string[]? lang) =>
        {
            var reader = host.Services.GetRequiredService<IReader>();
            var updater = host.Services.GetRequiredService<IUpdater>();
            var languages = GetLanguages(lang);
            foreach (var p in path.Where(p => p.Exists).Select(p => p.FullName))
            {
                var video = reader.ReadVideo(p);
                console.Out.WriteLine(FormattableString.CurrentCulture($"Updating {video.Name}"));
                updater.UpdateVideo(p, video, languages);
            }
        },
        pathArgument,
        langOption);

    return command;
}

static IDictionary<MediaTrackType, string>? GetLanguages(string[]? lang)
{
    return lang is null
        ? default(IDictionary<MediaTrackType, string>)
        : lang.Select(val => val.Split('=')).ToDictionary(GetId, GetLang);

    static MediaTrackType GetId(string[] val)
    {
        if (val.Length > 1)
        {
            var value = val[0].ToLowerInvariant();
            return value switch
            {
                "a" => MediaTrackType.Audio,
                "v" => MediaTrackType.Video,
                _ when int.TryParse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture, out var intValue) => (MediaTrackType)intValue,
                _ => MediaTrackType.Unknown,
            };
        }

        return MediaTrackType.All;
    }

    static string GetLang(string[] val)
    {
        return val[val.Length > 1 ? 1 : 0].ToLowerInvariant();
    }
}

static FileInfo[] ParseFileInfo(ArgumentResult argumentResult)
{
    return Process(argumentResult.Tokens.Select(token => token.Value)).SelectMany(results => results.Select(file => new System.IO.FileInfo(file))).ToArray();

    static IEnumerable<IEnumerable<string>> Process(IEnumerable<string> tokens)
    {
        var normalisedTokens = tokens.Select(token => token.Replace(Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar)).ToArray();
        var rooted = normalisedTokens.Where(Path.IsPathRooted).ToArray();
        foreach (var root in rooted)
        {
            yield return GetRooted(root);
        }

        var matcher = new Matcher(StringComparison.CurrentCulture);
        matcher.AddIncludePatterns(normalisedTokens.Except(rooted, StringComparer.Ordinal));
        yield return matcher.GetResultsInFullPath(Directory.GetCurrentDirectory());

        static IEnumerable<string> GetRooted(string root)
        {
            var matcher = new Matcher(StringComparison.CurrentCulture);

            // separate the root directory from the globbing
            var glob = Path.GetFileName(root);
            var directory = Path.GetDirectoryName(root);
            while (directory?.Contains('*', StringComparison.OrdinalIgnoreCase) == true)
            {
                glob = Path.GetFileName(directory) + Path.AltDirectorySeparatorChar + glob;
                directory = Path.GetDirectoryName(directory);
            }

            matcher.AddInclude(glob);
            return matcher.GetResultsInFullPath(directory);
        }
    }
}