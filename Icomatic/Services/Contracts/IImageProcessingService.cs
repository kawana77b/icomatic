using Icomatic.Core.Domain.Models;

namespace Icomatic.Services.Contracts
{
    internal interface IImageProcessingService
    {
        Image ApplyRoundedCorners(Image source, double cornerRadius = 0.2);

        Image ApplyCircularCrop(Image source);

        Image ApplyStyle(Image source, IconStyle.Style style, double cornerRadius = 0.2);

        Image ResizeImage(Image source, uint width, uint height);
    }
}