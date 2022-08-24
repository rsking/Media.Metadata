// -----------------------------------------------------------------------
// <copyright file="Iso639ToNameConverter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Converters;

/// <summary>
/// The ISO639 to Name converter.
/// </summary>
internal class Iso639ToNameConverter : Microsoft.UI.Xaml.Data.IValueConverter
{
    private static readonly object LoadingLock = new();

    private static IDictionary<string, string>? bibliographicToName;

    private static IDictionary<string, string>? terminologicToName;

    /// <inheritdoc/>
    public object? Convert(object? value, System.Type targetType, object parameter, string language)
    {
        EnsureDictionaries();
        return value switch
        {
            string key when bibliographicToName.TryGetValue(key, out var name) => name,
            string key when terminologicToName.TryGetValue(key, out var name) => name,
            string key => key,
            _ => default,
        };
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, System.Type targetType, object parameter, string language) => throw new System.NotSupportedException();

    [System.Diagnostics.CodeAnalysis.MemberNotNull(nameof(bibliographicToName), nameof(terminologicToName))]
    private static void EnsureDictionaries()
    {
        if (bibliographicToName is not null && terminologicToName is not null)
        {
            return;
        }

        lock (LoadingLock)
        {
            if (bibliographicToName is not null && terminologicToName is not null)
            {
                return;
            }

            var values = GetValues().ToList();
            bibliographicToName = values.ToDictionary(value => value.Bibliographic, value => value.Name, System.StringComparer.Ordinal);
            terminologicToName = values.Where(value => value.Terminologic is not null).ToDictionary(value => value.Terminologic!, value => value.Name, System.StringComparer.Ordinal);
        }
    }

    private static IEnumerable<Iso639> GetValues()
    {
        var stream = typeof(App).Assembly.GetManifestResourceStream(typeof(App), "ISO-639-2_utf-8.txt") ?? throw new System.InvalidOperationException();
        using var reader = new StreamReader(stream, System.Text.Encoding.UTF8, leaveOpen: false);

        while (reader.ReadLine() is string line)
        {
            var split = line.Split('|');
            yield return new Iso639
            {
                Bibliographic = split[0],
                Terminologic = SomethingOrNull(split[1]),
                Name = split[3],
            };

            static string? SomethingOrNull(string value)
            {
                return string.IsNullOrEmpty(value) ? null : value;
            }
        }
    }

    private readonly record struct Iso639
    {
        public string Bibliographic { get; init; }

        public string? Terminologic { get; init; }

        public string Name { get; init; }
    }
}