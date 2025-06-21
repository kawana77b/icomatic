using Icomatic.Commands.Base;
using Icomatic.Commons;
using Icomatic.Core.Domain.Models;
using Icomatic.Core.Validation.Contracts;
using Icomatic.Services.Contracts;
using System.CommandLine;

namespace Icomatic.Commands
{
    internal class RootCommandHandler : BaseCommandHandler
    {
        public override int Execute(ParseResult parseResult, IServiceProvider serviceProvider)
        {
            var validator = GetService<IIconGenerationValidator>(serviceProvider);
            var consoleUI = GetConsoleUI(serviceProvider);
            var templateService = GetService<ITemplateService>(serviceProvider);
            var iconGenerationService = GetService<IIconGenerationService>(serviceProvider);

            var validationResult = validator.ValidateInputs(parseResult);
            if (!validationResult.IsValid)
            {
                consoleUI.WriteError(validationResult.ErrorMessage!);
                return 1;
            }

            if (!Directory.Exists(validationResult.SaveDirectory))
                Directory.CreateDirectory(validationResult.SaveDirectory);

            var file = validationResult.File!;
            var isUrl = UriUtilities.IsHttpUrl(file);
            var infos = templateService.GetTemplateInformations(validationResult.Template);

            using Image? image = GetImage(file, isUrl, serviceProvider);
            if (image is null)
            {
                consoleUI.WriteError("Failed to load image.");
                return 1;
            }

            if (!consoleUI.PromptConfirmation(infos, validationResult.SaveDirectory, validationResult.Prefix, validationResult.Template, validationResult.Style))
            {
                consoleUI.WriteInfo("Operation cancelled.");
                return 1;
            }

            iconGenerationService.GenerateIcons(
                image!,
                infos,
                validationResult.SaveDirectory,
                validationResult.Prefix,
                validationResult.Style
            );
            consoleUI.WriteSuccess("Conversion completed.");
            return 0;
        }

        private static Image? GetImage(string file, bool isUrl, IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(file)) return null;

            var consoleUI = GetConsoleUI(serviceProvider);
            var webImageDownloadService = GetService<IWebImageDownloadService>(serviceProvider);

            if (isUrl)
            {
                try
                {
                    var downloadedImage = webImageDownloadService.DownloadImageAsync(file!).Result;
                    if (downloadedImage != null)
                    {
                        return new Image(downloadedImage.ToByteArray());
                    }
                }
                catch (Exception ex)
                {
                    consoleUI.WriteError($"Failed to download image: {ex.Message}");
                    return null;
                }
            }
            return Image.Load(file!);
        }
    }
}