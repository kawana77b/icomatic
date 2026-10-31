using ImageMagick;
using ImageMagick.Drawing;

namespace Icomatic.Core.Processing
{
    internal static class ImageProcessor
    {
        /// <summary>
        /// Creates a PNG copy with transparent rounded corners.
        /// </summary>
        /// <param name="image">The source image, which is left unchanged.</param>
        /// <param name="cornerRadius">The corner radius as a fraction of the shorter dimension, with a minimum of one pixel.</param>
        /// <returns>A new image that the caller must dispose.</returns>
        public static MagickImage RoundedCorners(MagickImage image, double cornerRadius = 0.2)
        {
            var result = image.Clone()!;

            // Force PNG format to support alpha channel
            result.Format = MagickFormat.Png;

            var width = result.Width;
            var height = result.Height;

            int radius = Math.Max(1, (int)(Math.Min(width, height) * cornerRadius));

            // Create a transparent mask with white rounded rectangle
            using var mask = new MagickImage(MagickColors.Transparent, width, height);
            var drawables = new Drawables()
                .FillColor(MagickColors.White)
                .StrokeColor(MagickColors.None)
                .RoundRectangle(0, 0, width - 1, height - 1, radius, radius);

            mask.Draw(drawables);

            // Enable alpha channel and apply mask
            result.Alpha(AlphaAction.Set);
            result.Composite(mask, CompositeOperator.DstIn);

            return (MagickImage)result;
        }

        /// <summary>
        /// Creates a PNG copy with transparency outside a centered circle, preserving the image dimensions.
        /// </summary>
        /// <param name="image">The source image, which is left unchanged.</param>
        /// <returns>A new image that the caller must dispose.</returns>
        public static MagickImage CircularCrop(MagickImage image)
        {
            var result = image.Clone()!;

            // Force PNG format to support alpha channel
            result.Format = MagickFormat.Png;

            var width = result.Width;
            var height = result.Height;
            var radius = Math.Min(width, height) / 2;

            // Create a transparent mask with white circle
            using var mask = new MagickImage(MagickColors.Transparent, width, height);
            var drawables = new Drawables()
                .FillColor(MagickColors.White)
                .StrokeColor(MagickColors.None)
                .Circle(width / 2, height / 2, width / 2, height / 2 - radius);

            mask.Draw(drawables);

            // Enable alpha channel and apply mask
            result.Alpha(AlphaAction.Set);
            result.Composite(mask, CompositeOperator.DstIn);

            return (MagickImage)result;
        }
    }
}
