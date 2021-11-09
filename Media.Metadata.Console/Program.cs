// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using Media.Metadata;
using Microsoft.Extensions.DependencyInjection;
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

var updateCommand = new CommandBuilder(new Command("update"))
    .AddCommand(updateMovieCommand.Command)
    .AddCommand(updateEpisodeCommand.Command);

var rootCommand = new CommandLineBuilder()
    .AddCommand(searchCommand.Command)
    .AddCommand(readCommand.Command)
    .AddCommand(updateCommand.Command);

await rootCommand
    .UseHost(
        Host.CreateDefaultBuilder,
        configure => configure.ConfigureServices((builder, services) =>
        {
            services
                .Configure<Media.Metadata.TheTVDB.TheTVDbOptions>(builder.Configuration.GetSection("TheTVDB"));

            services
                .AddTransient<IMovieSearch, Media.Metadata.TMDb.TMDbMovieSearch>()
                .AddTransient<IShowSearch, Media.Metadata.TheTVDB.TheTVDbShowSearch>();

            AddFileUpdater(services);

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
    .Build()
    .InvokeAsync(args)
    .ConfigureAwait(true);

static IServiceCollection AddFileUpdater(IServiceCollection services)
{
    if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
    {
        AddFileUpdaterImpl(services);
    }

    static void AddFileUpdaterImpl(IServiceCollection services)
    {
        services.AddTransient<IReader, Mp4Reader>();
        services.AddTransient<IUpdater, Mp4Writer>();
    }

    return services;
}

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

static async Task UpdateMovie(IHost host, FileInfo path, string name, int year = 0, CancellationToken cancellationToken = default)
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
                var updater = host.Services.GetRequiredService<IUpdater>();
                updater.UpdateMovie(path.FullName, movie);
                break;
            }
        }
    }
}

static async Task UpdateEpisode(IHost host, FileInfo path, string name, int season, int episode, CancellationToken cancellationToken = default)
{
    if (!path.Exists)
    {
        return;
    }

    foreach (var search in host.Services.GetServices<IShowSearch>())
    {
        await foreach (var series in search.SearchAsync(name, cancellationToken: cancellationToken).ConfigureAwait(false))
        {
            foreach (var s in series.Seasons.Where(s => s.Number == season))
            {
                var e = s.Episodes.FirstOrDefault(e => e.Number == episode);
                if (e is not null)
                {
                    var updater = host.Services.GetRequiredService<IUpdater>();
                    updater.UpdateEpisode(path.FullName, e);
                    return;
                }
            }
        }
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