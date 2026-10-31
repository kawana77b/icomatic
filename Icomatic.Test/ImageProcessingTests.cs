using Icomatic.Core.Processing;
using ImageMagick;
using Image = Icomatic.Core.Domain.Models.Image;

namespace Icomatic.Test;

public class ImageProcessingTests
{
    [Fact]
    public void Resize_UsesExactDimensionsAndLeavesSourceUnchanged()
    {
        using var pixels = new MagickImage(MagickColors.Red, 80, 40) { Format = MagickFormat.Png };
        using var source = new Image(pixels.ToByteArray());
        var originalBytes = source.ToBytes();

        using var resized = source.Resize(16, 24);

        Assert.Equal(16u, resized.Width);
        Assert.Equal(24u, resized.Height);
        Assert.Equal(originalBytes, source.ToBytes());
    }

    [Theory]
    [InlineData(false, 64, 64)]
    [InlineData(true, 64, 64)]
    [InlineData(false, 96, 64)]
    [InlineData(true, 96, 64)]
    public void Mask_MakesCornersTransparentAndPreservesCenterAndSource(bool circular, int width, int height)
    {
        using var source = new MagickImage(MagickColors.Red, (uint)width, (uint)height)
        {
            Format = MagickFormat.Jpg
        };

        using var result = circular
            ? ImageProcessor.CircularCrop(source)
            : ImageProcessor.RoundedCorners(source);
        using var resultPixels = result.GetPixels();
        using var sourcePixels = source.GetPixels();

        Assert.Equal(source.Width, result.Width);
        Assert.Equal(source.Height, result.Height);
        Assert.Equal(MagickFormat.Png, result.Format);
        Assert.Equal(MagickColors.Transparent.A, resultPixels.GetPixel(0, 0).ToColor()!.A);
        Assert.Equal(MagickColors.Red, resultPixels.GetPixel(width / 2, height / 2).ToColor());
        Assert.Equal(MagickFormat.Jpg, source.Format);
        Assert.Equal(MagickColors.Red, sourcePixels.GetPixel(0, 0).ToColor());

        if (circular && width > height)
        {
            // A rectangular image must use the shorter dimension for the circle diameter.
            Assert.Equal(MagickColors.Transparent.A, resultPixels.GetPixel(0, height / 2).ToColor()!.A);
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Mask_PreservesExistingTransparencyAtCenter(bool circular)
    {
        var color = new MagickColor("#ff000080");
        using var source = new MagickImage(color, 64, 64);

        using var result = circular
            ? ImageProcessor.CircularCrop(source)
            : ImageProcessor.RoundedCorners(source);
        using var pixels = result.GetPixels();

        Assert.Equal(color.A, pixels.GetPixel(32, 32).ToColor()!.A);
    }
}
