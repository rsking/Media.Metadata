// -----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        this.InitializeComponent();

        this.host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<ViewModels.MainViewModel>();
                services.AddTransient<MainWindow>();

                services
                    .AddTMDb();

                services
                    .AddMp4v2(Path.PathSeparator)
                    .AddTagLib();

                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
            })
            .Build();

        Ioc.Default.ConfigureServices(this.host.Services);

        Current = this;
    }

    /// <summary>
    /// Gets them main window handle.
    /// </summary>
    public System.IntPtr MainWindowWindowHandle { get; private set; }

    /// <inheritdoc cref="Application.Current" />
    internal static new App? Current { get; private set; }

    /// <summary>
    /// Gets the window.
    /// </summary>
    /// <returns>The main window.</returns>
    internal Window? GetWindow() => this.window;

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        await this.host.StartAsync().ConfigureAwait(true);

        this.window = this.host.Services.GetRequiredService<MainWindow>();
        this.window.Title = "Media Metadata Updater";
        this.window.ExtendsContentIntoTitleBar = true;
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