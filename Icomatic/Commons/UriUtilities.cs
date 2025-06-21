namespace Icomatic.Commons
{
    internal static class UriUtilities
    {
        /// <summary>
        /// Checks if the input string is a valid HTTP or HTTPS URL.
        /// </summary>
        /// <param name="input">The string to validate</param>
        /// <returns>True if the input is a valid HTTP/HTTPS URL, false otherwise</returns>
        public static bool IsHttpUrl(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Uri.TryCreate(input, UriKind.Absolute, out var uri) &&
                   (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Checks if the input string is a valid URL with any scheme.
        /// </summary>
        /// <param name="input">The string to validate</param>
        /// <returns>True if the input is a valid URL, false otherwise</returns>
        public static bool IsValidUrl(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Uri.TryCreate(input, UriKind.Absolute, out _);
        }

        /// <summary>
        /// Checks if the URL appears to point to an image based on its extension.
        /// </summary>
        /// <param name="url">The URL to check</param>
        /// <returns>True if the URL appears to be an image URL, false otherwise</returns>
        public static bool IsImageUrl(string url)
        {
            var supportedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg" };

            if (!IsValidUrl(url))
                return false;

            try
            {
                var uri = new Uri(url);
                var path = uri.AbsolutePath.ToLowerInvariant();

                // Check if URL ends with supported image extension
                foreach (var ext in supportedExtensions)
                {
                    if (path.EndsWith(ext))
                        return true;
                }

                // If no extension found, assume it might be a valid image URL
                // (some URLs don't have extensions but serve images)
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Extracts the file name from a URL.
        /// </summary>
        /// <param name="url">The URL to extract from</param>
        /// <returns>The file name or empty string if extraction fails</returns>
        public static string ExtractFileName(string url)
        {
            if (!IsValidUrl(url))
                return string.Empty;

            try
            {
                var uri = new Uri(url);
                return Path.GetFileName(uri.LocalPath);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}