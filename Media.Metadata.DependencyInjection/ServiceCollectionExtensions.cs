namespace Microsoft.Extensions.DependencyInjection;

using System;
using Microsoft.Extensions.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTheTVDB(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<Media.Metadata.TheTVDB.TheTVDbOptions>(configuration.GetSection("TheTVDB"));

        services
            .AddTransient<Media.Metadata.IShowSearch, Media.Metadata.TheTVDB.TheTVDbShowSearch>();

        return services;
    }

    public static IServiceCollection AddTMDb(this IServiceCollection services)
    {
        services
            .AddTransient<Media.Metadata.IMovieSearch, Media.Metadata.TMDb.TMDbMovieSearch>();
        return services;
    }

    public static IServiceCollection AddMp4v2(this IServiceCollection services)
    {
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            AddFileUpdaterImpl(services);
        }

        return services;

        static void AddFileUpdaterImpl(IServiceCollection services)
        {
            services
                .AddTransient<Media.Metadata.IReader, Media.Metadata.Mp4Reader>()
                .AddTransient<Media.Metadata.IUpdater, Media.Metadata.Mp4Writer>();
        }
    }
}