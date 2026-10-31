using Icomatic.Core.Processing;
using ImageMagick;

namespace Icomatic.Core.Domain.Models
{
    internal class Image : IDisposable
    {
        private readonly MagickImage _image;
        private bool _disposed = false;

        /// <summary>
        /// Decodes image bytes and sets the internal image format to PNG.
        /// </summary>
        /// <param name="buffer">The encoded image data.</param>
        public Image(byte[] buffer)
        {
            _image = new MagickImage(buffer)
            {
                Format = MagickFormat.Png
            };
        }

        /// <summary>
        /// Loads an image from a local file and sets its internal format to PNG.
        /// </summary>
        /// <param name="path">The path of the image file.</param>
        /// <returns>A new image that the caller must dispose.</returns>
        /// <exception cref="ArgumentException">The path is null or empty.</exception>
        /// <exception cref="FileNotFoundException">The file does not exist.</exception>
        public static Image Load(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty", nameof(path));

            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            var buffer = File.ReadAllBytes(path);
            return new Image(buffer);
        }

        /// <summary>
        /// Loads an image from the specified file.
        /// </summary>
        /// <param name="fileinfo">The image file to load.</param>
        /// <returns>A new image in PNG format that the caller must dispose.</returns>
        public static Image Load(FileInfo fileinfo)
        {
            return Load(fileinfo.FullName);
        }

        public Size Size => new(_image.Width, _image.Height);
        public uint Width => _image.Width;
        public uint Height => _image.Height;
        public Format.SupportedFormat Format => _image.Format.ToSupportedFormat();

        /// <summary>
        /// Creates a copy with transparent rounded corners.
        /// </summary>
        /// <param name="cornerRadius">The corner radius as a fraction of the shorter image dimension.</param>
        /// <returns>A new PNG image that the caller must dispose.</returns>
        public Image WithRoundCorners(double cornerRadius = 0.2)
        {
            using var img = ImageProcessor.RoundedCorners(_image, cornerRadius);
            return new Image(img.ToByteArray());
        }

        /// <summary>
        /// Creates a copy with a centered circular mask and transparency outside the circle.
        /// </summary>
        /// <returns>A new PNG image that the caller must dispose.</returns>
        public Image WithCircularCrop()
        {
            using var img = ImageProcessor.CircularCrop(_image);
            return new Image(img.ToByteArray());
        }

        /// <summary>
        /// Creates a resized copy with exact dimensions, ignoring the original aspect ratio.
        /// </summary>
        /// <param name="width">The target width in pixels.</param>
        /// <param name="height">The target height in pixels.</param>
        /// <returns>A new PNG image that the caller must dispose.</returns>
        public Image Resize(uint width, uint height)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);

            using var image = _image.Clone();
            image.Resize(new MagickGeometry(width, height) { IgnoreAspectRatio = true });
            return new Image(image.ToByteArray());
        }

        /// <summary>
        /// Writes the image to a file using its current format.
        /// </summary>
        /// <param name="path">The output file path.</param>
        /// <exception cref="ObjectDisposedException">The image has been disposed.</exception>
        public void Save(string path)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _image.Write(path);
        }

        /// <summary>
        /// Writes the image to the specified file using its current format.
        /// </summary>
        /// <param name="fileInfo">The output file.</param>
        public void Save(FileInfo fileInfo) => Save(fileInfo.FullName);

        /// <summary>
        /// Writes the image to a file using the specified output format.
        /// </summary>
        /// <param name="path">The output file path.</param>
        /// <param name="format">The output image format; SVG output is not supported.</param>
        /// <exception cref="NotSupportedException">The requested output format is SVG.</exception>
        /// <exception cref="ObjectDisposedException">The image has been disposed.</exception>
        public void Save(string path, Format.SupportedFormat format)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            if (format == Models.Format.SupportedFormat.SVG)
            {
                throw new NotSupportedException("svg is not supported");
            }
            _image.Write(path, format.ToMagickFormat());
        }

        /// <summary>
        /// Encodes the image in its current format.
        /// </summary>
        /// <returns>The encoded image bytes.</returns>
        public byte[] ToBytes() => _image.ToByteArray();

        /// <summary>
        /// Encodes the image as a Base64 string in its current format.
        /// </summary>
        /// <returns>The Base64-encoded image data.</returns>
        public string ToBase64() => _image.ToBase64();

        /// <summary>
        /// Creates an independent copy of the image with its internal format set to PNG.
        /// </summary>
        /// <returns>A new image that the caller must dispose.</returns>
        public Image Clone()
        {
            return new Image(_image.ToByteArray());
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _image?.Dispose();
                }
                _disposed = true;
            }
        }

        ~Image()
        {
            Dispose(false);
        }
    }
}