// -----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

/// <summary>
/// Interaction logic for <c>App.xaml</c>.
/// </summary>
public partial class App : Application
{
    private readonly IHost host;

    /// <summary>
    /// Initialises a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        this.host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<MvvmDialogs.IDialogService, MvvmDialogs.DialogService>();
                services.AddTransient<ViewModels.MainViewModel>();
                services.AddTransient<MainWindow>();
            })
            .Build();

        Ioc.Default.ConfigureServices(this.host.Services);
    }

    /// <inheritdoc/>
    protected override async void OnStartup(StartupEventArgs e)
    {
        await this.host.StartAsync().ConfigureAwait(true);

        this.host.Services.GetRequiredService<MainWindow>().Show();

        base.OnStartup(e);
    }

    /// <inheritdoc/>
    protected override async void OnExit(ExitEventArgs e)
    {
        using (this.host)
        {
            await this.host.StopAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(true);
        }

        base.OnExit(e);
    }
}