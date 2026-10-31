using Icomatic.Core.Domain.Models;
using Icomatic.Core.Domain.Templates;

namespace Icomatic.Services.Contracts
{
    internal interface IIconGenerationService
    {
        /// <summary>
        /// Generates and saves icons for the supplied template entries and reports each generated file.
        /// </summary>
        /// <param name="sourceImage">The source image to resize and style.</param>
        /// <param name="templateInfos">The output sizes, names, and formats to generate.</param>
        /// <param name="saveDirectory">The existing directory in which to save the icons.</param>
        /// <param name="prefix">The optional filename prefix, separated from the template name by a hyphen.</param>
        /// <param name="style">The icon style to apply.</param>
        /// <remarks>Rounded and circular styles are saved as PNG; the plain style uses each template entry's format.</remarks>
        void GenerateIcons(Image sourceImage, TemplateDefinitions.TemplateInfo[] templateInfos,
                          string saveDirectory, string prefix, IconStyle.Style style);
    }
}