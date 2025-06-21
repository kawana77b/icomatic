using Icomatic.Commons;
using System.ComponentModel.DataAnnotations;
using ImageMagick;

namespace Icomatic.Core.Domain.Models
{
    internal class Format
    {
        public enum SupportedFormat
        {
            [Display(Name = "png")]
            PNG,

            [Display(Name = "jpg")]
            JPEG,

            [Display(Name = "webp")]
            WEBP,

            [Display(Name = "bmp")]
            BMP,

            [Display(Name = "ico")]
            ICO,

            [Display(Name = "svg")]
            SVG,

            [Display(Name = "gif")]
            GIF,

            [Display(Name = "tiff")]
            TIFF,
        }

        public static readonly SupportedFormat PNG = SupportedFormat.PNG;
        public static readonly SupportedFormat JPEG = SupportedFormat.JPEG;
        public static readonly SupportedFormat WEBP = SupportedFormat.WEBP;
        public static readonly SupportedFormat BMP = SupportedFormat.BMP;
        public static readonly SupportedFormat ICO = SupportedFormat.ICO;
        public static readonly SupportedFormat SVG = SupportedFormat.SVG;
        public static readonly SupportedFormat GIF = SupportedFormat.GIF;
        public static readonly SupportedFormat TIFF = SupportedFormat.TIFF;

        public static SupportedFormat? Get(string path)
        {
            var extension = Path.GetExtension(path).ToLowerInvariant();
            return extension switch
            {
                ".png" => PNG,
                ".jpg" or ".jpeg" => JPEG,
                ".webp" => WEBP,
                ".bmp" => BMP,
                ".ico" => ICO,
                ".svg" => SVG,
                ".gif" => GIF,
                ".tif" or ".tiff" => TIFF,
                _ => null
            };
        }
    }

    internal static partial class FormatExtensions
    {
        public static string GetDefaultExtension(this Format.SupportedFormat @this)
        {
            return @this switch
            {
                Format.SupportedFormat.PNG => ".png",
                Format.SupportedFormat.JPEG => ".jpg",
                Format.SupportedFormat.WEBP => ".webp",
                Format.SupportedFormat.BMP => ".bmp",
                Format.SupportedFormat.ICO => ".ico",
                Format.SupportedFormat.SVG => ".svg",
                Format.SupportedFormat.GIF => ".gif",
                Format.SupportedFormat.TIFF => ".tiff",
                _ => throw new ArgumentOutOfRangeException(nameof(@this), @this, null)
            };
        }

        public static string GetName(this Format.SupportedFormat @this) => EnumUtilities.ToDisplayName(@this);

        public static MagickFormat ToMagickFormat(this Format.SupportedFormat @this)
        {
            return @this switch
            {
                Format.SupportedFormat.PNG => MagickFormat.Png,
                Format.SupportedFormat.JPEG => MagickFormat.Jpg,
                Format.SupportedFormat.ICO => MagickFormat.Ico,
                Format.SupportedFormat.BMP => MagickFormat.Bmp,
                Format.SupportedFormat.WEBP => MagickFormat.WebP,
                Format.SupportedFormat.SVG => MagickFormat.Svg,
                Format.SupportedFormat.GIF => MagickFormat.Gif,
                Format.SupportedFormat.TIFF => MagickFormat.Tiff,
                _ => throw new NotSupportedException(),
            };
        }

        public static Format.SupportedFormat ToSupportedFormat(this MagickFormat @this)
        {
            return @this switch
            {
                MagickFormat.Png => Format.SupportedFormat.PNG,
                MagickFormat.Jpg => Format.SupportedFormat.JPEG,
                MagickFormat.Ico => Format.SupportedFormat.ICO,
                MagickFormat.Bmp => Format.SupportedFormat.BMP,
                MagickFormat.WebP => Format.SupportedFormat.WEBP,
                MagickFormat.Svg => Format.SupportedFormat.SVG,
                MagickFormat.Gif => Format.SupportedFormat.GIF,
                MagickFormat.Tiff => Format.SupportedFormat.TIFF,
                _ => throw new NotSupportedException(),
            };
        }
    }
}