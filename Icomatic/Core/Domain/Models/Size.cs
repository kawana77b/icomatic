namespace Icomatic.Core.Domain.Models
{
    internal record Size
    {
        public uint Width { get; init; }

        public uint Height { get; init; }

        /// <summary>
        /// Initializes a size with positive pixel dimensions.
        /// </summary>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <exception cref="ArgumentException">Either dimension is zero.</exception>
        public Size(uint width, uint height)
        {
            if (width == 0) throw new ArgumentException("Width must be greater than 0", nameof(width));
            if (height == 0) throw new ArgumentException("Height must be greater than 0", nameof(height));
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets the shorter image dimension.
        /// </summary>
        /// <returns>The smaller of the width and height.</returns>
        public uint Min() => Math.Min(Width, Height);
        /// <summary>
        /// Gets the longer image dimension.
        /// </summary>
        /// <returns>The larger of the width and height.</returns>
        public uint Max() => Math.Max(Width, Height);

        public bool IsSquare => Width == Height;
    }
}