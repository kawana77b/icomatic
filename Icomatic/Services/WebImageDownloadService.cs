using Icomatic.Services.Contracts;
using ImageMagick;

namespace Icomatic.Services
{
    public class WebImageDownloadService : IWebImageDownloadService
    {
        private readonly HttpClient _httpClient;

        public WebImageDownloadService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "IconGenerator/1.0");
        }

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

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}