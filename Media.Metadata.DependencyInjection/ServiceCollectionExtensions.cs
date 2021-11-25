// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the TVDB services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The input services.</returns>
    public static IServiceCollection AddTheTVDB(this IServiceCollection services, IConfiguration configuration) =>
        services
            .Configure<Media.Metadata.TheTVDB.TheTVDbOptions>(configuration.GetSection("TheTVDB"))
            .AddTransient<Media.Metadata.IShowSearch, Media.Metadata.TheTVDB.TheTVDbShowSearch>();

    /// <summary>
    /// Adds the TMDb services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The input services.</returns>
    public static IServiceCollection AddTMDb(this IServiceCollection services) => services.AddTransient<Media.Metadata.IMovieSearch, Media.Metadata.TMDb.TMDbMovieSearch>();

    /// <summary>
    /// Adds the <see cref="TagLib"/> services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The input services.</returns>
    public static IServiceCollection AddTagLib(this IServiceCollection services)
    {
        services.TryAddTransient<Media.Metadata.IReader, Media.Metadata.TagLibReader>();
        return services;
    }

    /// <summary>
    /// Adds the MP4 v2 services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="pathSeparator">The path separator.</param>
    /// <returns>The input services.</returns>
    public static IServiceCollection AddMp4v2(this IServiceCollection services, char pathSeparator = ';')
    {
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            AddFileUpdaterImpl(services, pathSeparator);
        }

        return services;

        static void AddFileUpdaterImpl(IServiceCollection services, char pathSeparator)
        {
            services
                .AddTransient<Media.Metadata.IReader, Media.Metadata.Mp4Reader>()
                .AddTransient<Media.Metadata.IUpdater, Media.Metadata.Mp4Writer>();

            AppDomain.CurrentDomain.AddNativePath(new string(pathSeparator, 1));
        }
    }
}