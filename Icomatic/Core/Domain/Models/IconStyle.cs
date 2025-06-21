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
        public static string GetName(this IconStyle.Style @this) => EnumUtilities.ToDisplayName(@this);
    }
}