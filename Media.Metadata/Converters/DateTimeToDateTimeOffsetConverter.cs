// -----------------------------------------------------------------------
// <copyright file="DateTimeToDateTimeOffsetConverter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Converters;

/// <summary>
/// <see cref="Microsoft.UI.Xaml.Data.IValueConverter"/> to convert <see cref="System.DateTime"/> to <see cref="System.DateTimeOffset"/> instances.
/// </summary>
internal class DateTimeToDateTimeOffsetConverter : Microsoft.UI.Xaml.Data.IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object value, System.Type targetType, object parameter, string language)
    {
        return value switch
        {
            System.DateTime dateTime => new System.DateTimeOffset(dateTime),
            System.DateTimeOffset dateTimeOffset => dateTimeOffset,
            _ => default,
        };
    }

    /// <inheritdoc/>
    public object? ConvertBack(object value, System.Type targetType, object parameter, string language)
    {
        return value switch
        {
            System.DateTime dateTime => dateTime,
            System.DateTimeOffset dateTimeOffset => dateTimeOffset.DateTime,
            _ => default,
        };
    }
}