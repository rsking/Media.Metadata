// -----------------------------------------------------------------------
// <copyright file="Iso639ToNameConverter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Iso639ToNameConverter : Microsoft.UI.Xaml.Data.IValueConverter
{
    private static IDictionary<string, string>? codeToName;

    private static IDictionary<string, string> CodeToName
    {
        get
        {
            if (codeToName is not null)
            {
                return codeToName;
            }

            var values = GetValues();
            codeToName = values.ToDictionary(value => value.Bibliographic, value => value.Name, StringComparer.Ordinal);
            return codeToName;
        }
    }

    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            string key when CodeToName.TryGetValue(key, out var name) => name,
            string key => key,
            _ => default,
        };
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();

    private static IEnumerable<Iso639> GetValues()
    {
        var stream = typeof(App).Assembly.GetManifestResourceStream(typeof(App), "ISO-639-2_utf-8.txt") ?? throw new InvalidOperationException();
        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: false);

        while (reader.ReadLine() is string line)
        {
            var split = line.Split('|');
            yield return new Iso639
            {
                Bibliographic = split[0],
                Terminologic = split[1],
                Name = split[3],
            };
        }
    }

    private readonly record struct Iso639
    {
        public string Bibliographic { get; init; }

        public string? Terminologic { get; init; }

        public string Name { get; init; }
    }
}