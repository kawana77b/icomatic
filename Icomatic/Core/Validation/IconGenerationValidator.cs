using Icomatic.Commons;
using Icomatic.Core.Domain.Models;
using Icomatic.Core.Domain.Templates;
using Icomatic.Core.Validation.Contracts;
using Icomatic.Infrastructure.Contracts;
using System.CommandLine;

namespace Icomatic.Core.Validation
{
    /// <summary>
    /// Icon generation specific input validator
    /// </summary>
    internal class IconGenerationValidator(IUserPromptService userPromptService) : IIconGenerationValidator
    {
        private readonly IUserPromptService _userPromptService = userPromptService ?? throw new ArgumentNullException(nameof(userPromptService));

        public IconGenerationValidationResult ValidateFile(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return new IconGenerationValidationResult(false, "Input file must be specified");
            }

            // Check if input is a URL
            if (UriUtilities.IsHttpUrl(filePath))
            {
                if (!UriUtilities.IsImageUrl(filePath))
                {
                    return new IconGenerationValidationResult(false, $"Invalid or unsupported image URL: {filePath}");
                }
            }
            else
            {
                var fi = new FileInfo(filePath);
                if (!fi.Exists)
                {
                    return new IconGenerationValidationResult(false, $"File not found: {fi.FullName}");
                }
                if (Format.Get(fi.FullName) is null)
                {
                    return new IconGenerationValidationResult(false, $"Not a supported extension: {fi.Extension}");
                }
            }

            return new IconGenerationValidationResult(true);
        }

        public IconGenerationValidationResult ValidateStyle(string? styleValue)
        {
            if (string.IsNullOrEmpty(styleValue))
            {
                return new IconGenerationValidationResult(true); // Will be prompted later
            }

            if (!Enum.TryParse<IconStyle.Style>(styleValue, true, out _))
            {
                return new IconGenerationValidationResult(false, $"Invalid style: {styleValue}. Valid options are: plane, round, circle");
            }

            return new IconGenerationValidationResult(true);
        }

        public IconGenerationValidationResult ValidateTemplate(string? templateName)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                return new IconGenerationValidationResult(true); // Will be prompted later
            }

            var templatesByName = TemplateDefinitions.GetTemplatesByName();
            if (!templatesByName.ContainsKey(templateName))
            {
                return new IconGenerationValidationResult(false, $"Invalid template: {templateName}");
            }

            return new IconGenerationValidationResult(true);
        }

        public IconGenerationValidationResult ValidateDirectory(string? directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                return new IconGenerationValidationResult(false, "Save directory cannot be null or empty");
            }

            return new IconGenerationValidationResult(true);
        }

        public IconGenerationValidationResult ValidateInputs(ParseResult parseResult)
        {
            try
            {
                string prefix = parseResult.GetValue<string>("--name") ?? "";

                // Validate file
                var file = parseResult.GetValue<string>("file");
                var fileValidation = ValidateFile(file);
                if (!fileValidation.IsValid)
                {
                    return fileValidation;
                }

                // Validate directory
                var saveDirectory = parseResult.GetValue<string>("--dir");
                var directoryValidation = ValidateDirectory(saveDirectory);
                if (!directoryValidation.IsValid)
                {
                    return directoryValidation;
                }

                // Handle style (with prompting if needed)
                string inputStyleValue = parseResult.GetValue<string>("--style") ?? string.Empty;
                bool styleOptionProvided = !string.IsNullOrWhiteSpace(inputStyleValue);

                IconStyle.Style style;
                if (!styleOptionProvided)
                {
                    style = _userPromptService.PromptForStyle();
                }
                else
                {
                    var styleValidation = ValidateStyle(inputStyleValue);
                    if (!styleValidation.IsValid)
                    {
                        return styleValidation;
                    }
                    if (!Enum.TryParse(inputStyleValue, true, out style))
                    {
                        return new IconGenerationValidationResult(false, $"Invalid style: {inputStyleValue}");
                    }
                }

                // Handle template (with prompting if needed)
                string template = parseResult.GetValue<string>("--template") ?? string.Empty;
                if (string.IsNullOrEmpty(template))
                {
                    var availableTemplates = TemplateDefinitions.GetTemplateNames();
                    template = _userPromptService.PromptForTemplate(availableTemplates);
                }
                else
                {
                    var templateValidation = ValidateTemplate(template);
                    if (!templateValidation.IsValid)
                    {
                        return templateValidation;
                    }
                }

                return new IconGenerationValidationResult(
                    true,
                    null,
                    prefix,
                    style,
                    saveDirectory!,
                    file,
                    template
                );
            }
            catch (Exception ex)
            {
                return new IconGenerationValidationResult(false, $"Validation error: {ex.Message}");
            }
        }
    }
}