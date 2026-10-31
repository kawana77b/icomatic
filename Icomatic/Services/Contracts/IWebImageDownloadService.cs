using ImageMagick;

namespace Icomatic.Services.Contracts
{
    public interface IWebImageDownloadService
    {
        /// <summary>
        /// Downloads and decodes an image from the specified URL.
        /// </summary>
        /// <param name="url">The URL from which to download the image.</param>
        /// <returns>A task containing the downloaded image, which the caller must dispose.</returns>
        /// <exception cref="InvalidOperationException">The image could not be downloaded or decoded.</exception>
        Task<MagickImage> DownloadImageAsync(string url);
    }
}