// -----------------------------------------------------------------------
// <copyright file="AppDomainExtensions.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Extensions.DependencyInjection;

using System.Runtime.InteropServices;

/// <summary>
/// Extension methods for <see cref="AppDomain"/>.
/// </summary>
public static class AppDomainExtensions
{
    /// <summary>
    /// Adds the native path.
    /// </summary>
    /// <param name="appDomain">The application domain.</param>
    /// <param name="pathSeparator">The path separator.</param>
    /// <exception cref="InvalidOperationException">Failed to detemine native path.</exception>
    public static void AddNativePath(this AppDomain appDomain, string pathSeparator = ";")
    {
        var potentialPaths = GetPotentialPaths()
            .Select(path => path.Replace('/', Path.DirectorySeparatorChar));

        var probingDirectories = ((string?)appDomain.GetData("PROBING_DIRECTORIES"))?.Split(Path.PathSeparator) ?? new[] { appDomain.BaseDirectory };
        var directories = probingDirectories
            .Select(RootedPath)
            .SelectMany(directory => potentialPaths.Select(path => Path.Combine(directory, path)))
            .Where(Directory.Exists);

        var path = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
        path = path is null
            ? string.Join(pathSeparator, directories)
            : string.Concat(string.Join(pathSeparator, directories), Path.PathSeparator, path);

        Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process);

        string RootedPath(string path)
        {
            return Path.IsPathRooted(path) ? path : Path.Combine(appDomain.BaseDirectory, path);
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
                Architecture.Arm64 when IntPtr.Size == 4 => Arm,
                Architecture.Arm64 => Arm64,
                Architecture.X86 => X86,
                Architecture.X64 when IntPtr.Size == 4 => X86,
                Architecture.X64 => X64,
                _ => throw new InvalidOperationException(),
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

                throw new InvalidOperationException();
            }
        }
    }
}