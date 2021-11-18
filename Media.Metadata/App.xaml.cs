// -----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
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

                services.AddMp4v2();

                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
            })
            .Build();

        Ioc.Default.ConfigureServices(this.host.Services);

        var potentialPaths = GetPotentialPaths()
            .Select(path => path.Replace('/', Path.DirectorySeparatorChar));

        var probingDirectories = ((string?)System.AppDomain.CurrentDomain.GetData("PROBING_DIRECTORIES"))?.Split(Path.PathSeparator) ?? new[] { System.AppDomain.CurrentDomain.BaseDirectory };
        var directories = probingDirectories
            .Select(RootedPath)
            .SelectMany(directory => potentialPaths.Select(path => Path.Combine(directory, path)))
            .Where(Directory.Exists);

        var path = System.Environment.GetEnvironmentVariable("PATH", System.EnvironmentVariableTarget.Process);
        path = path is null
            ? string.Join(Path.PathSeparator, directories)
            : string.Concat(string.Join(Path.PathSeparator, directories), Path.PathSeparator, path);

        System.Environment.SetEnvironmentVariable("PATH", path, System.EnvironmentVariableTarget.Process);

        static string RootedPath(string path)
        {
            return Path.IsPathRooted(path) ? path : Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, path);
        }

        // get the path to the runtimes
        static IEnumerable<string> GetPotentialPaths()
        {
            const string Arm = "arm";
            const string Arm64 = "arm64";
            const string X64 = "x64";
            const string X86 = "x86";

            var architecture = RuntimeInformation.OSArchitecture switch
            {
                Architecture.Arm => Arm,
                Architecture.Arm64 when System.IntPtr.Size == 4 => Arm,
                Architecture.Arm64 => Arm64,
                Architecture.X86 => X86,
                Architecture.X64 when System.IntPtr.Size == 4 => X86,
                Architecture.X64 => X64,
                _ => throw new System.InvalidOperationException(),
            };

            yield return $"runtimes/{GetRidFront()}-{architecture}/native";
            yield return architecture;

            static string GetRidFront()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return "win";
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return "linux";
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return "osx";
                }

                throw new System.InvalidOperationException();
            }
        }
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