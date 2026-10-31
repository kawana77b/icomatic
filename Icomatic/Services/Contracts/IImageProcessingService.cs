using Icomatic.Core.Domain.Models;

namespace Icomatic.Services.Contracts
{
    internal interface IImageProcessingService
    {
        /// <summary>
        /// Creates a copy of the source image with transparent rounded corners.
        /// </summary>
        /// <param name="source">The image to process.</param>
        /// <param name="cornerRadius">The corner radius as a fraction of the shorter image dimension.</param>
        /// <returns>A new PNG image with rounded corners; the caller must dispose it.</returns>
        Image ApplyRoundedCorners(Image source, double cornerRadius = 0.2);

        /// <summary>
        /// Creates a copy of the source image with a centered circular mask.
        /// </summary>
        /// <param name="source">The image to process.</param>
        /// <returns>A new PNG image with transparency outside the circle; the caller must dispose it.</returns>
        Image ApplyCircularCrop(Image source);

        /// <summary>
        /// Applies the requested icon style to the source image.
        /// </summary>
        /// <param name="source">The image to process.</param>
        /// <param name="style">The icon style to apply.</param>
        /// <param name="cornerRadius">The corner radius as a fraction of the shorter dimension, used only for the rounded style.</param>
        /// <returns>A new image for rounded or circular styles, or the source instance for the plain style or an unrecognized value.</returns>
        /// <remarks>The caller owns any new image returned. The source image is not modified.</remarks>
        Image ApplyStyle(Image source, IconStyle.Style style, double cornerRadius = 0.2);

        /// <summary>
        /// Creates a resized copy with exact dimensions, ignoring the original aspect ratio.
        /// </summary>
        /// <param name="source">The image to resize.</param>
        /// <param name="width">The target width in pixels.</param>
        /// <param name="height">The target height in pixels.</param>
        /// <returns>A new resized image; the caller must dispose it.</returns>
        Image ResizeImage(Image source, uint width, uint height);
    }
}