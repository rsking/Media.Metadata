// -----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private readonly IHost host;

    private Window? window;

    /// <summary>
    /// Initialises a new instance of the <see cref="App"/> class as a singleton application object.
    /// This is the first line of authored code executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();

        this.host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<ViewModels.MainViewModel>();
                services.AddTransient<MainWindow>();

                services.AddMp4v2(Path.PathSeparator);

                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
            })
            .Build();

        Ioc.Default.ConfigureServices(this.host.Services);
    }

    /// <summary>
    /// Gets them main window handle.
    /// </summary>
    public System.IntPtr MainWindowWindowHandle { get; private set; }

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        await this.host.StartAsync().ConfigureAwait(true);

        this.window = this.host.Services.GetRequiredService<MainWindow>();
        this.window.Activate();
        this.MainWindowWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this.window);

        this.window.Closed += async (_, __) =>
        {
            using (this.host)
            {
                await this.host.StopAsync(System.TimeSpan.FromSeconds(5)).ConfigureAwait(true);
            }
        };
    }
}