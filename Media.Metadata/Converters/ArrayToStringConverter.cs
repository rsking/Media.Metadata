// -----------------------------------------------------------------------
// <copyright file="ArrayToStringConverter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Converters;

/// <summary>
/// The converter from a <see cref="IEnumerable{string}"/> to a <see cref="string"/>.
/// </summary>
public sealed class ArrayToStringConverter : Microsoft.UI.Xaml.Data.IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object value, System.Type targetType, object parameter, string language)
    {
        return value switch
        {
            IEnumerable<string> enumerable => string.Join("; ", enumerable),
            _ => default,
        };
    }

    /// <inheritdoc/>
    public object? ConvertBack(object value, System.Type targetType, object parameter, string language)
    {
        return value switch
        {
            string stringValue => stringValue.Split(';').Select(x => x.Trim()).ToList(),
            _ => default,
        };
    }
}