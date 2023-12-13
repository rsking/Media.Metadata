// -----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Microsoft.UI.Xaml.Application
{
    private readonly IHost host;

    private Microsoft.UI.Xaml.Window? window;

    /// <summary>
    /// Initialises a new instance of the <see cref="App"/> class as a singleton application object.
    /// This is the first line of authored code executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        this.InitializeComponent();

        this.host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                _ = services.AddTransient<ViewModels.MainViewModel>();
                _ = services.AddTransient<MainWindow>();

                _ = services
                    .AddTMDb();

                _ = services
                    .AddMp4v2()
                    .AddTagLib();

                _ = services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
            })
            .Build();

        CommunityToolkit.Mvvm.DependencyInjection.Ioc.Default.ConfigureServices(this.host.Services);

        Current = this;
    }

    /// <inheritdoc cref="Application.Current" />
    internal static new App? Current { get; private set; }

    /// <summary>
    /// Gets the window.
    /// </summary>
    /// <returns>The main window.</returns>
    internal Microsoft.UI.Xaml.Window? GetWindow() => this.window;

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        await this.host.StartAsync().ConfigureAwait(true);

        this.window = this.host.Services.GetRequiredService<MainWindow>();
        this.window.Title = "Media Metadata Updater";
        this.window.ExtendsContentIntoTitleBar = true;
        SetTitleBarIcon(this.window);
        this.window.Activate();

        this.window.Closed += async (_, __) =>
        {
            using (this.host)
            {
                await this.host.StopAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(true);
            }
        };

        static void SetTitleBarIcon(Microsoft.UI.Xaml.Window window)
        {
            // get the icon from the application
            var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
            if (windowHandle != default
                && Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle) is { Value: > 0UL } windowId
                && Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId) is { Id.Value: > 0UL } appWindow
                && Vanara.PInvoke.Shell32.ExtractIcon(Vanara.PInvoke.HINSTANCE.NULL, typeof(App).Assembly.Location, 0) is { IsInvalid: false } iconHandle)
            {
                appWindow.SetIcon(Microsoft.UI.Win32Interop.GetIconIdFromIcon((nint)(Vanara.PInvoke.HICON)iconHandle));
            }
        }
    }
}