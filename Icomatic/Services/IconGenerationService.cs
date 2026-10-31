using Icomatic.Core.Domain.Models;
using Icomatic.Core.Domain.Templates;
using Icomatic.Infrastructure.Contracts;
using Icomatic.Services.Contracts;

namespace Icomatic.Services
{
    internal class IconGenerationService : IIconGenerationService
    {
        private const double DefaultCornerRadius = 0.2;
        private readonly IConsoleUserInterface _consoleUI;

        /// <summary>
        /// Initializes the icon generation service with a console user interface.
        /// </summary>
        /// <param name="consoleUI">The interface used to report progress and generated files.</param>
        /// <exception cref="ArgumentNullException">The console interface is null.</exception>
        public IconGenerationService(IConsoleUserInterface consoleUI)
        {
            _consoleUI = consoleUI ?? throw new ArgumentNullException(nameof(consoleUI));
        }

        /// <inheritdoc />
        public void GenerateIcons(Image sourceImage, TemplateDefinitions.TemplateInfo[] templateInfos,
                                 string saveDirectory, string prefix, IconStyle.Style style)
        {
            _consoleUI.WriteInfo("Generating icons...");

            foreach (var info in templateInfos)
            {
                GenerateSingleIcon(sourceImage, info, saveDirectory, prefix, style);
            }
        }

        private void GenerateSingleIcon(Image sourceImage, TemplateDefinitions.TemplateInfo info,
                                       string saveDirectory, string prefix, IconStyle.Style style)
        {
            using var resized = sourceImage.Resize(info.Size.Width, info.Size.Height);
            using var finalImage = ApplyStyle(resized, style);

            var filename = Path.Join(saveDirectory, info.Filename(prefix));

            if (style != IconStyle.Style.Plane)
            {
                finalImage.Save(filename);
            }
            else
            {
                finalImage.Save(filename, info.Format);
            }

            _consoleUI.WriteGeneratedFile(filename);
        }

        private Image ApplyStyle(Image image, IconStyle.Style style)
        {
            var imageProcessingService = new ImageProcessingService();
            return imageProcessingService.ApplyStyle(image, style, DefaultCornerRadius);
        }
    }
}