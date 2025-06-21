using Icomatic.Core.Processing;
using ImageMagick;

namespace Icomatic.Core.Domain.Models
{
    internal class Image : IDisposable
    {
        private readonly MagickImage _image;
        private bool _disposed = false;

        public Image(byte[] buffer)
        {
            _image = new MagickImage(buffer)
            {
                Format = MagickFormat.Png
            };
        }

        public static Image Load(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty", nameof(path));

            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            var buffer = File.ReadAllBytes(path);
            return new Image(buffer);
        }

        public static Image Load(FileInfo fileinfo)
        {
            return Load(fileinfo.FullName);
        }

        public Size Size => new(_image.Width, _image.Height);
        public uint Width => _image.Width;
        public uint Height => _image.Height;
        public Format.SupportedFormat Format => _image.Format.ToSupportedFormat();

        /// <summary>
        /// Rounded corners
        /// </summary>
        /// <param name="cornerRadius"></param>
        /// <returns></returns>
        public Image WithRoundCorners(double cornerRadius = 0.2)
        {
            using var img = ImageProcessor.RoundedCorners(_image, cornerRadius);
            return new Image(img.ToByteArray());
        }

        /// <summary>
        /// Circular crop
        /// </summary>
        /// <returns></returns>
        public Image WithCircularCrop()
        {
            using var img = ImageProcessor.CircularCrop(_image);
            return new Image(img.ToByteArray());
        }

        /// <summary>
        /// Resize to the specified size with exact dimensions (ignoring aspect ratio)
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Image Resize(uint width, uint height)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);

            using var image = _image.Clone();
            image.Resize(new MagickGeometry(width, height) { IgnoreAspectRatio = true });
            return new Image(image.ToByteArray());
        }

        public void Save(string path)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _image.Write(path);
        }

        public void Save(FileInfo fileInfo) => Save(fileInfo.FullName);

        public void Save(string path, Format.SupportedFormat format)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            if (format == Models.Format.SupportedFormat.SVG)
            {
                throw new NotSupportedException("svg is not supported");
            }
            _image.Write(path, format.ToMagickFormat());
        }

        public byte[] ToBytes() => _image.ToByteArray();

        public string ToBase64() => _image.ToBase64();

        public Image Clone()
        {
            return new Image(_image.ToByteArray());
        }

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