using Icomatic.Core.Domain.Models;
using Icomatic.Core.Domain.Templates;

namespace Icomatic.Services.Contracts
{
    internal interface IIconGenerationService
    {
        void GenerateIcons(Image sourceImage, TemplateDefinitions.TemplateInfo[] templateInfos,
                          string saveDirectory, string prefix, IconStyle.Style style);
    }
}