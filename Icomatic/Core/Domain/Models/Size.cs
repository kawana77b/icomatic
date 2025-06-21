namespace Icomatic.Core.Domain.Models
{
    internal record Size
    {
        public uint Width { get; init; }

        public uint Height { get; init; }

        public Size(uint width, uint height)
        {
            if (width == 0) throw new ArgumentException("Width must be greater than 0", nameof(width));
            if (height == 0) throw new ArgumentException("Height must be greater than 0", nameof(height));
            Width = width;
            Height = height;
        }

        public uint Min() => Math.Min(Width, Height);
        public uint Max() => Math.Max(Width, Height);

        public bool IsSquare => Width == Height;
    }
}