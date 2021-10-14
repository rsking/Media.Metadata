// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Builder;
using System.CommandLine.Collections;
using System.CommandLine.Help;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using System.CommandLine.Suggestions;
using Media.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp.Serializers.SystemTextJson;

var searchMovieCommand = new CommandBuilder(new Command("movie") { Handler = CommandHandler.Create<IHost, string, int>(SearchMovie) })
    .AddArgument(new Argument<string>("name"))
    .AddOption(new Option<int?>(new[] { "--year", "-y" }, "The movie year"));

var readMovieCommand = new CommandBuilder(new Command("movie") { Handler = CommandHandler.Create<IHost, FileInfo>(ReadMovie) })
    .AddArgument(new Argument<FileInfo>("path"));

var searchShowCommand = new CommandBuilder(new Command("show") { Handler = CommandHandler.Create<IHost, string>(SearchShow) })
    .AddArgument(new Argument<string>("name"));

var searchCommand = new CommandBuilder(new Command("search"))
    .AddCommand(searchMovieCommand.Command)
    .AddCommand(searchShowCommand.Command);

var readCommand = new CommandBuilder(new Command("read"))
    .AddCommand(readMovieCommand.Command);

var updateMovieCommand = new CommandBuilder(new Command("movie") { Handler = CommandHandler.Create<IHost, FileInfo, string, int>(UpdateMovie) })
    .AddArgument(new Argument<FileInfo>("path"))
    .AddArgument(new Argument<string>("name"))
    .AddOption(new Option<int?>(new[] { "--year", "-y" }, "The movie year"));

var updateCommand = new CommandBuilder(new Command("update"))
    .AddCommand(updateMovieCommand.Command);

var rootCommand = new CommandLineBuilder()
    .AddCommand(searchCommand.Command)
    .AddCommand(readCommand.Command)
    .AddCommand(updateCommand.Command);

await rootCommand
    .UseHost(
        Host.CreateDefaultBuilder,
        configure => configure.ConfigureServices(services =>
        {
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
        //services.AddTransient<IUpdater, Media.Metadata.Windows.FileUpdater>();
        services.AddTransient<IReader, Media.Metadata.Windows.Mp4Reader>();
    }

    return services;
}

static async Task SearchMovie(IHost host, string name, int year = 0)
{
    foreach (var search in host.Services.GetServices<IMovieSearch>())
    {
        await foreach (var movie in search.SearchAsync(name, year).ConfigureAwait(false))
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

static async Task UpdateMovie(IHost host, FileInfo path, string name, int year = 0)
{
    if (!path.Exists)
    {
        return;
    }

    foreach (var search in host.Services.GetServices<IMovieSearch>())
    {
        await foreach (var movie in search.SearchAsync(name, year).ConfigureAwait(false))
        {
            if (string.Equals(movie.Name, name, StringComparison.Ordinal) && (year == 0 || (movie.Release.HasValue && movie.Release.Value.Year == year)))
            {
                var updater = host.Services.GetRequiredService<IUpdater>();
                updater.UpdateMovie(path.FullName, movie);
                break;
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

static async Task SearchShow(IHost host, string name)
{
    foreach (var search in host.Services.GetServices<IShowSearch>())
    {
        await foreach (var show in search.SearchAsync(name).ConfigureAwait(false))
        {
            Console.WriteLine("{0}", show.Name);
        }
    }
}