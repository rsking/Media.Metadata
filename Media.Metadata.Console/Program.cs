// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using System.CommandLine.Rendering;
using Media.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Hosting;
using RestSharp.Serializers.SystemTextJson;

var searchMovieCommand = new CommandBuilder(new Command("movie") { Handler = CommandHandler.Create(SearchMovie) })
    .AddArgument(new Argument<string>("name"))
    .AddOption(new Option<int?>(new[] { "--year", "-y" }, "The movie year"));

var readMovieCommand = new CommandBuilder(new Command("movie") { Handler = CommandHandler.Create(ReadMovie) })
    .AddArgument(new Argument<FileInfo>("path").ExistingOnly());

var searchShowCommand = new CommandBuilder(new Command("show") { Handler = CommandHandler.Create(SearchShow) })
    .AddArgument(new Argument<string>("name"));

var searchCommand = new CommandBuilder(new Command("search"))
    .AddCommand(searchMovieCommand.Command)
    .AddCommand(searchShowCommand.Command);

var readCommand = new CommandBuilder(new Command("read"))
    .AddCommand(readMovieCommand.Command);

var updateMovieCommand = new CommandBuilder(new Command("movie") { Handler = CommandHandler.Create(UpdateMovie) })
    .AddArgument(new Argument<FileInfo>("path").ExistingOnly())
    .AddArgument(new Argument<string>("name"))
    .AddOption(new Option<int?>(new[] { "--year", "-y" }, "The movie year"));

var updateEpisodeCommand = new CommandBuilder(new Command("episode") { Handler = CommandHandler.Create(UpdateEpisode) })
    .AddArgument(new Argument<FileInfo>("path").ExistingOnly())
    .AddOption(new Option<string>(new[] { "--name", "-n" }, "The series name") { IsRequired = true })
    .AddOption(new Option<int>(new[] { "--season", "-s" }, "The season number"))
    .AddOption(new Option<int>(new[] { "--episode", "-e" }, "The episode number"));

var updateVideoCommand = new CommandBuilder(new Command("video") { Handler = CommandHandler.Create(UpdateVideo) })
    .AddArgument(new Argument<FileInfo[]>("path", ParseFileInfo).ExistingOnly());

var updateCommand = new CommandBuilder(new Command("update"))
    .AddCommand(updateMovieCommand.Command)
    .AddCommand(updateEpisodeCommand.Command)
    .AddCommand(updateVideoCommand.Command)
    .AddGlobalOption(new Option<IList<string>>(new[] { "--lang", "-l" }, "`[tkID=]LAN` Set the language. LAN is the ISO 639 code (eng, swe, ...). If no track ID is given, sets language to all tracks"));

var rootCommand = new CommandLineBuilder()
    .AddCommand(searchCommand.Command)
    .AddCommand(readCommand.Command)
    .AddCommand(updateCommand.Command);

await rootCommand
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
                .AddTransient<RestSharp.IRestClient>(_ =>
                    new RestSharp.RestClient().UseSystemTextJson(new System.Text.Json.JsonSerializerOptions
                    {
                        Converters = { new JsonDateConverter() },
                        PropertyNameCaseInsensitive = true,
                    }));

            services
                .Configure<InvocationLifetimeOptions>(options => options.SuppressStatusMessages = true);
        }))
    .CancelOnProcessTermination()
    .UseAnsiTerminalWhenAvailable()
    .Build()
    .InvokeAsync(args)
    .ConfigureAwait(true);

static async Task SearchMovie(IHost host, string name, int year = 0, CancellationToken cancellationToken = default)
{
    foreach (var search in host.Services.GetServices<IMovieSearch>())
    {
        await foreach (var movie in search.SearchAsync(name, year, cancellationToken: cancellationToken).ConfigureAwait(false))
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
}

static async Task UpdateMovie(IConsole console, IHost host, FileInfo path, string name, int year = 0, string[]? lang = default, CancellationToken cancellationToken = default)
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
}

static async Task UpdateEpisode(IConsole console, IHost host, FileInfo path, string name, int season, int episode, string[]? lang = default, CancellationToken cancellationToken = default)
{
    if (!path.Exists)
    {
        return;
    }

    foreach (var search in host.Services.GetServices<IShowSearch>())
    {
        await foreach (var series in search.SearchAsync(name, cancellationToken: cancellationToken).ConfigureAwait(false))
        {
            console.Out.WriteLine($"Found Series {series.Name}");
            foreach (var s in series.Seasons.Where(s => s.Number == season))
            {
                console.Out.WriteLine(FormattableString.CurrentCulture($"Found Season {series.Name}:{s.Number}"));
                var e = s.Episodes.FirstOrDefault(e => e.Number == episode);
                if (e is not null)
                {
                    console.Out.WriteLine(FormattableString.CurrentCulture($"Found Episode {series.Name}:{s.Number}:{e.Name}"));
                    var updater = host.Services.GetRequiredService<IUpdater>();
                    updater.UpdateEpisode(path.FullName, e with { Image = s.Image ?? series.Image ?? e.Image }, GetLanguages(lang));
                    return;
                }
            }
        }
    }
}

static void UpdateVideo(IConsole console, IHost host, FileInfo[] path, string[]? lang = default)
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
}

static IDictionary<string, string>? GetLanguages(string[]? lang)
{
    return lang is null
        ? default
        : (IDictionary<string, string>)lang
            .Select(val => val.Split('='))
            .ToDictionary(GetId, GetLang);

    static string GetId(string[] val)
    {
        return val.Length > 1 ? val[0] : string.Empty;
    }

    static string GetLang(string[] val)
    {
        return val[val.Length > 1 ? 1 : 0];
    }
}

static void ReadMovie(IHost host, FileInfo path)
{
    if (!path.Exists)
    {
        return;
    }

    var reader = host.Services.GetRequiredService<IReader>();
    var movie = reader.ReadMovie(path.FullName);
    Console.WriteLine(movie.Name);
}

static async Task SearchShow(IHost host, string name, CancellationToken cancellationToken = default)
{
    foreach (var search in host.Services.GetServices<IShowSearch>())
    {
        await foreach (var show in search.SearchAsync(name, cancellationToken: cancellationToken).ConfigureAwait(false))
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