using Icomatic.Core.Domain.Models;
using Icomatic.Services.Contracts;

namespace Icomatic.Services
{
    internal class ImageProcessingService : IImageProcessingService
    {
        private const double DefaultCornerRadius = 0.2;

        public Image ApplyRoundedCorners(Image source, double cornerRadius = DefaultCornerRadius)
        {
            return source.WithRoundCorners(cornerRadius);
        }

        public Image ApplyCircularCrop(Image source)
        {
            return source.WithCircularCrop();
        }

        public Image ApplyStyle(Image source, IconStyle.Style style, double cornerRadius = DefaultCornerRadius)
        {
            return style switch
            {
                IconStyle.Style.Round => ApplyRoundedCorners(source, cornerRadius),
                IconStyle.Style.Circle => ApplyCircularCrop(source),
                IconStyle.Style.Plane => source,
                _ => source
            };
        }

        public Image ResizeImage(Image source, uint width, uint height)
        {
            return source.Resize(width, height);
        }
    }
}