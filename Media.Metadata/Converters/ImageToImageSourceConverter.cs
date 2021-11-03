// -----------------------------------------------------------------------
// <copyright file="ImageToImageSourceConverter.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata.Converters;

/// <summary>
/// The image to image source converter.
/// </summary>
[System.Windows.Data.ValueConversion(typeof(System.Drawing.Image), typeof(System.Windows.Media.ImageSource))]
public sealed class ImageToImageSourceConverter : System.Windows.Data.IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        // empty images are empty…
        if (value is null)
        {
            return null;
        }

        if (value is System.Drawing.Image image)
        {
            using var stream = new System.IO.MemoryStream();
            using (var bitmap = new System.Drawing.Bitmap(image))
            {
                bitmap.Save(stream, image.RawFormat);
            }

            stream.Seek(0, System.IO.SeekOrigin.Begin);

            var bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        return System.Windows.Data.Binding.DoNothing;
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotSupportedException();
}