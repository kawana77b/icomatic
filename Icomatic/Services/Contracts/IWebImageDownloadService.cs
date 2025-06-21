using ImageMagick;

namespace Icomatic.Services.Contracts
{
    public interface IWebImageDownloadService
    {
        Task<MagickImage> DownloadImageAsync(string url);
    }
}