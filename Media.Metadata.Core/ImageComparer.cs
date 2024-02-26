// -----------------------------------------------------------------------
// <copyright file="ImageComparer.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;

/// <summary>
/// The image comparer.
/// </summary>
public static class ImageComparer
{
    /// <summary>
    /// Compares two images.
    /// </summary>
    /// <param name="actualImage">The actual image.</param>
    /// <param name="expectedImage">The expected image.</param>
    /// <returns><see langword="true"/> if images are the same; otherwise <see langword="false"/>.</returns>
    public static bool Compare(Image actualImage, Image expectedImage) => Compare(actualImage, expectedImage, new ColorDifference());

    /// <summary>
    /// Compares two images.
    /// </summary>
    /// <param name="actualImage">The actual image.</param>
    /// <param name="expectedImage">The expected image.</param>
    /// <param name="argbTolerance">The ARGB tolerance.</param>
    /// <returns><see langword="true"/> if images are the same; otherwise <see langword="false"/>.</returns>
    public static bool Compare(Image actualImage, Image expectedImage, ColorDifference argbTolerance) => CompareInternal(actualImage, expectedImage, argbTolerance, out _, createOutImage: false);

    /// <summary>
    /// Compares two images.
    /// </summary>
    /// <param name="actualImage">The actual image.</param>
    /// <param name="expectedImage">The expected image.</param>
    /// <param name="diffImage">The output difference image.</param>
    /// <returns><see langword="true"/> if images are the same; otherwise <see langword="false"/>.</returns>
    public static bool Compare(Image actualImage, Image expectedImage, out Image? diffImage) => Compare(actualImage, expectedImage, new ColorDifference(), out diffImage);

    /// <summary>
    /// Compares two images.
    /// </summary>
    /// <param name="actualImage">The actual image.</param>
    /// <param name="expectedImage">The expected image.</param>
    /// <param name="argbTolerance">The ARGB tolerance.</param>
    /// <param name="diffImage">The output difference image.</param>
    /// <returns><see langword="true"/> if images are the same; otherwise <see langword="false"/>.</returns>
    public static bool Compare(Image actualImage, Image expectedImage, ColorDifference argbTolerance, out Image? diffImage) => CompareInternal(actualImage, expectedImage, argbTolerance, out diffImage, createOutImage: true);

    /// <summary>
    /// Compares two images.
    /// </summary>
    /// <param name="actualImage">The actual image.</param>
    /// <param name="expectedImage">The expected image.</param>
    /// <param name="rectangleList">The rectangle list.</param>
    /// <returns><see langword="true"/> if images are the same; otherwise <see langword="false"/>.</returns>
    public static bool Compare(Image actualImage, Image expectedImage, IList<ToleranceRectangle> rectangleList) => CompareInternal(actualImage, expectedImage, rectangleList, out _, createOutImage: false);

    /// <summary>
    /// Compares two images.
    /// </summary>
    /// <param name="actualImage">The actual image.</param>
    /// <param name="expectedImage">The expected image.</param>
    /// <param name="rectangleList">The rectangle list.</param>
    /// <param name="diffImage">The output difference image.</param>
    /// <returns><see langword="true"/> if images are the same; otherwise <see langword="false"/>.</returns>
    public static bool Compare(Image actualImage, Image expectedImage, IList<ToleranceRectangle> rectangleList, out Image? diffImage) => CompareInternal(actualImage, expectedImage, rectangleList, out diffImage, createOutImage: true);

    private static ColorDifference Compare(Color color1, Color color2)
    {
        var first = (System.Numerics.Vector4)color1;
        var second = (System.Numerics.Vector4)color2;
        return new()
        {
            Alpha = (byte)Math.Abs(first.W - second.W),
            Red = (byte)Math.Abs(first.X - second.X),
            Green = (byte)Math.Abs(first.Y - second.Y),
            Blue = (byte)Math.Abs(first.Z - second.Z),
        };
    }

    private static bool CompareInternal(Image actualImage, Image expectedImage, IList<ToleranceRectangle> rectangleList, out Image? diffImage, bool createOutImage)
    {
        if (actualImage is null)
        {
            throw new ArgumentNullException(nameof(actualImage));
        }

        if (expectedImage is null)
        {
            throw new ArgumentNullException(nameof(expectedImage));
        }

        if (rectangleList is null)
        {
            throw new ArgumentNullException(nameof(rectangleList));
        }

        if (actualImage.Width != expectedImage.Width
            || actualImage.Height != expectedImage.Height
            || actualImage.PixelType != expectedImage.PixelType)
        {
            diffImage = default;
            return false;
        }

        var actualSnapshot = Snapshot.FromImage(actualImage);
        var expectedSnapshot = Snapshot.FromImage(expectedImage);
        if (actualSnapshot.Width != expectedSnapshot.Width || actualSnapshot.Height != expectedSnapshot.Height)
        {
            throw new InvalidOperationException(Properties.Resources.ImageSizesNotEqual);
        }

        var toleranceMap = CreateToleranceMap(rectangleList, actualSnapshot.Height, actualSnapshot.Width);
        return CompareInternal(actualSnapshot, expectedSnapshot, toleranceMap, out diffImage, createOutImage);
    }

    private static bool CompareInternal(Image actualImage, Image expectedImage, ColorDifference argbTolerance, out Image? diffImage, bool createOutImage)
    {
        if (actualImage is null)
        {
            throw new ArgumentNullException(nameof(actualImage));
        }

        if (expectedImage is null)
        {
            throw new ArgumentNullException(nameof(expectedImage));
        }

        if (argbTolerance is null)
        {
            throw new ArgumentNullException(nameof(argbTolerance));
        }

        if (actualImage.Width != expectedImage.Width
            || actualImage.Height != expectedImage.Height
            || actualImage.PixelType != expectedImage.PixelType)
        {
            diffImage = default;
            return false;
        }

        var actualSnapshot = Snapshot.FromImage(actualImage);
        var expectedSnapshot = Snapshot.FromImage(expectedImage);
        if (actualSnapshot.Width != expectedSnapshot.Width || actualSnapshot.Height != expectedSnapshot.Height)
        {
            throw new InvalidOperationException(Properties.Resources.ImageSizesNotEqual);
        }

        var toleranceMap = new SingleValueToleranceMap(Color.FromRgba(argbTolerance.Red, argbTolerance.Green, argbTolerance.Blue, argbTolerance.Alpha));
        return CompareInternal(actualSnapshot, expectedSnapshot, toleranceMap, out diffImage, createOutImage);
    }

    private static bool CompareInternal(Snapshot actualSnapshot, Snapshot expectedSnapshot, Snapshot toleranceMap, out Image? diffImage, bool createOutImage)
    {
        var result = true;
        Snapshot? snapshot = null;
        diffImage = null;
        if (actualSnapshot.Width != expectedSnapshot.Width || actualSnapshot.Height != expectedSnapshot.Height)
        {
            throw new InvalidOperationException(Properties.Resources.ImageSizesNotEqual);
        }

        if (createOutImage)
        {
            snapshot = new Snapshot(actualSnapshot.Height, actualSnapshot.Width);
            snapshot.SetAllPixels(Color.FromRgba(0, 0, 0, 255));
        }

        for (var i = 0; i < actualSnapshot.Height; i++)
        {
            for (var j = 0; j < actualSnapshot.Width; j++)
            {
                var colorDifference = Compare(actualSnapshot[i, j], expectedSnapshot[i, j]);
                if (!colorDifference.MeetsTolerance(new ColorDifference(toleranceMap[i, j])))
                {
                    result = false;
                    if (snapshot is not null)
                    {
                        snapshot[i, j] = colorDifference.CalculateMargin(new ColorDifference(toleranceMap[i, j]));
                    }
                    else
                    {
                        return result;
                    }
                }
            }
        }

        if (snapshot is not null)
        {
            diffImage = snapshot.ToImage();
        }

        return result;
    }

    private static Snapshot CreateToleranceMap(IList<ToleranceRectangle> rectangleList, int height, int width)
    {
        var snapshot = new Snapshot(height, width);
        snapshot.SetAllPixels(Color.White);
        for (var i = 0; i < rectangleList.Count; i++)
        {
            if (rectangleList[i].Rectangle.Left < 0 || rectangleList[i].Rectangle.Top < 0 || rectangleList[i].Rectangle.Right > width || rectangleList[i].Rectangle.Bottom > height)
            {
                throw new InvalidOperationException(string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.RectangleNotInRange, i));
            }

            for (var j = rectangleList[i].Rectangle.Top; j < rectangleList[i].Rectangle.Bottom; j++)
            {
                for (var k = rectangleList[i].Rectangle.Left; k < rectangleList[i].Rectangle.Right; k++)
                {
                    snapshot[j, k] = Color.FromRgba(rectangleList[i].Difference.Red, rectangleList[i].Difference.Green, rectangleList[i].Difference.Blue, rectangleList[i].Difference.Alpha);
                }
            }
        }

        return snapshot;
    }
}