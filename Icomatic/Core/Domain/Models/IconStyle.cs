using Icomatic.Commons;
using System.ComponentModel.DataAnnotations;

namespace Icomatic.Core.Domain.Models
{
    internal static class IconStyle
    {
        public enum Style
        {
            [Display(Name = "plane")]
            Plane,

            [Display(Name = "round")]
            Round,

            [Display(Name = "circle")]
            Circle,
        }

        public static readonly Style DefaultStyle = Style.Round;

        public static readonly Style[] Styles = [
            Style.Plane,
            Style.Round,
            Style.Circle,
        ];
    }

    internal static class IconStyleExtensions
    {
        /// <summary>
        /// Gets the display name of an icon style.
        /// </summary>
        /// <param name="this">The icon style.</param>
        /// <returns>The display name, or an empty string if no name is defined.</returns>
        public static string GetName(this IconStyle.Style @this) => EnumUtilities.ToDisplayName(@this);
    }
}