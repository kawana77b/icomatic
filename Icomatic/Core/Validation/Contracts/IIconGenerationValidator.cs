using Icomatic.Core.Domain.Models;

namespace Icomatic.Core.Validation.Contracts
{
    /// <summary>
    /// Icon generation specific validator interface
    /// </summary>
    internal interface IIconGenerationValidator : IBaseValidator<IconGenerationValidationResult>
    {
        IconGenerationValidationResult ValidateFile(string? filePath);

        IconGenerationValidationResult ValidateStyle(string? styleValue);

        IconGenerationValidationResult ValidateTemplate(string? templateName);

        IconGenerationValidationResult ValidateDirectory(string? directoryPath);
    }

    /// <summary>
    /// Icon generation specific validation result
    /// </summary>
    internal record IconGenerationValidationResult(
        bool IsValid,
        string? ErrorMessage = null,
        string Prefix = "",
        IconStyle.Style Style = IconStyle.Style.Round,
        string SaveDirectory = "",
        string? File = null,
        string Template = ""
    );
}