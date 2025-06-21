using Icomatic.Commons;
using Icomatic.Core.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Icomatic.Core.Domain.Templates
{
    internal static class TemplateDefinitions
    {
        internal enum Category
        {
            [Display(Name = "pwa", Description = "Progressive Web App icons")]
            PWA,

            [Display(Name = "ios", Description = "iOS app icons for various devices and contexts")]
            iOS,

            [Display(Name = "android", Description = "Android app icons for different screen densities")]
            Android,

            [Display(Name = "desktop", Description = "Desktop wallpapers for various resolutions")]
            Desktop,

            [Display(Name = "mobile", Description = "Mobile wallpapers for different device sizes")]
            Mobile,

            [Display(Name = "social", Description = "Social media platform images and profiles")]
            Social,

            [Display(Name = "windows", Description = "Windows desktop application icons")]
            Windows,

            [Display(Name = "windows-tiles", Description = "Windows Start Menu tile icons")]
            WindowsTiles,

            [Display(Name = "web", Description = "Web favicon and touch icons")]
            Web,
        }

        public record TemplateInfo
        {
            public Format.SupportedFormat Format { get; init; } = Models.Format.SupportedFormat.PNG;
            public Size Size { get; init; } = new Size(1, 1);
            public string Name { get; set; } = string.Empty;

            public string Filename(string prefix = "") => $"{WithPrefixName(prefix)}{Format.GetDefaultExtension()}";
            private string WithPrefixName(string prefix = "") => string.IsNullOrWhiteSpace(prefix) ? Name : $"{prefix}-{Name}";
        }

        public static readonly TemplateInfo[] PWA =
        [
            new TemplateInfo()
            {
                Size = new Size(16, 16),
                Name = "favicon-16x16",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(32, 32),
                Name = "favicon-32x32",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(48, 48),
                Name = "favicon-48x48",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(64, 64),
                Name = "pwa-64x64"
            },
            new TemplateInfo()
            {
                Size = new Size(192, 192),
                Name = "pwa-192x192"
            },
            new TemplateInfo()
            {
                Size = new Size(512, 512),
                Name = "pwa-512x512"
            },
        ];

        public static readonly TemplateInfo[] iOS =
        [
            new TemplateInfo()
            {
                Size = new Size(20, 20),
                Name = "ios-20x20"
            },
            new TemplateInfo()
            {
                Size = new Size(29, 29),
                Name = "ios-29x29"
            },
            new TemplateInfo()
            {
                Size = new Size(40, 40),
                Name = "ios-40x40"
            },
            new TemplateInfo()
            {
                Size = new Size(58, 58),
                Name = "ios-58x58"
            },
            new TemplateInfo()
            {
                Size = new Size(60, 60),
                Name = "ios-60x60"
            },
            new TemplateInfo()
            {
                Size = new Size(76, 76),
                Name = "ios-76x76"
            },
            new TemplateInfo()
            {
                Size = new Size(80, 80),
                Name = "ios-80x80"
            },
            new TemplateInfo()
            {
                Size = new Size(87, 87),
                Name = "ios-87x87"
            },
            new TemplateInfo()
            {
                Size = new Size(120, 120),
                Name = "ios-120x120"
            },
            new TemplateInfo()
            {
                Size = new Size(152, 152),
                Name = "ios-152x152"
            },
            new TemplateInfo()
            {
                Size = new Size(167, 167),
                Name = "ios-167x167"
            },
            new TemplateInfo()
            {
                Size = new Size(180, 180),
                Name = "ios-180x180"
            },
            new TemplateInfo()
            {
                Size = new Size(1024, 1024),
                Name = "ios-1024x1024"
            },
        ];

        public static readonly TemplateInfo[] Android =
        [
            new TemplateInfo()
            {
                Size = new Size(36, 36),
                Name = "android-ldpi"
            },
            new TemplateInfo()
            {
                Size = new Size(48, 48),
                Name = "android-mdpi"
            },
            new TemplateInfo()
            {
                Size = new Size(72, 72),
                Name = "android-hdpi"
            },
            new TemplateInfo()
            {
                Size = new Size(96, 96),
                Name = "android-xhdpi"
            },
            new TemplateInfo()
            {
                Size = new Size(144, 144),
                Name = "android-xxhdpi"
            },
            new TemplateInfo()
            {
                Size = new Size(192, 192),
                Name = "android-xxxhdpi"
            },
            new TemplateInfo()
            {
                Size = new Size(512, 512),
                Name = "android-playstore"
            },
        ];

        // Desktop Wallpapers
        public static readonly TemplateInfo[] Desktop =
        [
            new TemplateInfo()
            {
                Size = new Size(1920, 1080),
                Name = "desktop-fhd",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(2560, 1440),
                Name = "desktop-qhd",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(3840, 2160),
                Name = "desktop-4k",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(5120, 2880),
                Name = "desktop-5k",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(1366, 768),
                Name = "desktop-hd",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(1440, 900),
                Name = "desktop-wxga",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(1680, 1050),
                Name = "desktop-wsxga",
                Format = Format.SupportedFormat.JPEG,
            },
        ];

        // Mobile Wallpapers
        public static readonly TemplateInfo[] Mobile =
        [
            new TemplateInfo()
            {
                Size = new Size(1080, 1920),
                Name = "mobile-fhd",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(1440, 2560),
                Name = "mobile-qhd",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(1125, 2436),
                Name = "mobile-iphone-x",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(1242, 2688),
                Name = "mobile-iphone-xs-max",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(828, 1792),
                Name = "mobile-iphone-xr",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(1170, 2532),
                Name = "mobile-iphone-12-pro",
                Format = Format.SupportedFormat.JPEG,
            },
        ];

        public static readonly TemplateInfo[] Social =
        [
            new TemplateInfo()
            {
                Size = new Size(1200, 630),
                Name = "social-og-image",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(1024, 512),
                Name = "social-twitter-header",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(400, 400),
                Name = "social-twitter-profile"
            },
            new TemplateInfo()
            {
                Size = new Size(1200, 1200),
                Name = "social-facebook-post",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(820, 312),
                Name = "social-facebook-cover",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(1080, 1080),
                Name = "social-instagram-post",
                Format = Format.SupportedFormat.JPEG,
            },
            new TemplateInfo()
            {
                Size = new Size(1080, 1920),
                Name = "social-instagram-story",
                Format = Format.SupportedFormat.JPEG,
            },
        ];

        public static readonly TemplateInfo[] Windows =
        [
            new TemplateInfo()
            {
                Size = new Size(16, 16),
                Name = "windows-16x16",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(32, 32),
                Name = "windows-32x32",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(48, 48),
                Name = "windows-48x48",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(64, 64),
                Name = "windows-64x64",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(128, 128),
                Name = "windows-128x128",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(256, 256),
                Name = "windows-256x256",
                Format = Format.SupportedFormat.ICO
            },
        ];

        public static readonly TemplateInfo[] WindowsTiles = [
            new TemplateInfo()
            {
                Size = new Size(70, 70),
                Name = "windows-tile-small"
            },
            new TemplateInfo()
            {
                Size = new Size(150, 150),
                Name = "windows-tile-medium"
            },
            new TemplateInfo()
            {
                Size = new Size(310, 150),
                Name = "windows-tile-wide"
            },
            new TemplateInfo()
            {
                Size = new Size(310, 310),
                Name = "windows-tile-large"
            },
        ];

        public static readonly TemplateInfo[] Web =
        [
            new TemplateInfo()
            {
                Size = new Size(16, 16),
                Name = "favicon-16x16",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(32, 32),
                Name = "favicon-32x32",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(48, 48),
                Name = "favicon-48x48",
                Format = Format.SupportedFormat.ICO
            },
            new TemplateInfo()
            {
                Size = new Size(57, 57),
                Name = "apple-touch-icon-57x57"
            },
            new TemplateInfo()
            {
                Size = new Size(60, 60),
                Name = "apple-touch-icon-60x60"
            },
            new TemplateInfo()
            {
                Size = new Size(72, 72),
                Name = "apple-touch-icon-72x72"
            },
            new TemplateInfo()
            {
                Size = new Size(76, 76),
                Name = "apple-touch-icon-76x76"
            },
            new TemplateInfo()
            {
                Size = new Size(114, 114),
                Name = "apple-touch-icon-114x114"
            },
            new TemplateInfo()
            {
                Size = new Size(120, 120),
                Name = "apple-touch-icon-120x120"
            },
            new TemplateInfo()
            {
                Size = new Size(144, 144),
                Name = "apple-touch-icon-144x144"
            },
            new TemplateInfo()
            {
                Size = new Size(152, 152),
                Name = "apple-touch-icon-152x152"
            },
            new TemplateInfo()
            {
                Size = new Size(180, 180),
                Name = "apple-touch-icon-180x180"
            },
            new TemplateInfo()
            {
                Size = new Size(192, 192),
                Name = "android-chrome-192x192"
            },
            new TemplateInfo()
            {
                Size = new Size(512, 512),
                Name = "android-chrome-512x512"
            },
        ];

        public static ReadOnlyDictionary<Category, IEnumerable<TemplateInfo>> GetAvailableTemplates()
        {
            var dict = new Dictionary<Category, IEnumerable<TemplateInfo>>
            {
                { Category.PWA, PWA },
                { Category.iOS,  iOS },
                { Category.Android,  Android },
                { Category.Desktop,  Desktop },
                { Category.Mobile,  Mobile },
                { Category.Social,  Social },
                { Category.Windows,  Windows },
                { Category.WindowsTiles,  WindowsTiles },
                { Category.Web,  Web },
            };
            return new ReadOnlyDictionary<Category, IEnumerable<TemplateInfo>>(dict);
        }

        public static ReadOnlyDictionary<string, IEnumerable<TemplateInfo>> GetTemplatesByName()
        {
            var dict = new Dictionary<string, IEnumerable<TemplateInfo>>();
            foreach (var item in GetAvailableTemplates())
                dict.Add(item.Key.GetName(), item.Value);

            return new ReadOnlyDictionary<string, IEnumerable<TemplateInfo>>(dict);
        }

        public static string[] GetTemplateNames()
        {
            return [.. GetTemplatesByName().Keys.OrderBy(x => x)];
        }
    }

    internal static class CategoryExtensions
    {
        public static string GetName(this TemplateDefinitions.Category @this) => EnumUtilities.ToDisplayName(@this);

        public static string GetDescription(this TemplateDefinitions.Category @this) => EnumUtilities.ToDisplayDescription(@this);
    }
}