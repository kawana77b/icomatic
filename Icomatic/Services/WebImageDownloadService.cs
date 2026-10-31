using Icomatic.Services.Contracts;
using ImageMagick;

namespace Icomatic.Services
{
    public class WebImageDownloadService : IWebImageDownloadService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes an image downloader with its own HTTP client.
        /// </summary>
        public WebImageDownloadService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "IconGenerator/1.0");
        }

        /// <inheritdoc />
        public async Task<MagickImage> DownloadImageAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                return new MagickImage(imageBytes);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to download image from URL: {url}", ex);
            }
        }

        /// <summary>
        /// Releases the HTTP client used to download images.
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}