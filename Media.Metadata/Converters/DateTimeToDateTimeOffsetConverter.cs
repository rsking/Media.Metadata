// -----------------------------------------------------------------------
// <copyright file="DateTimeToDateTimeOffsetConverter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Converters;

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