using Icomatic.Core.Domain.Models;

namespace Icomatic.Core.Validation.Contracts
{
    /// <summary>
    /// Icon generation specific validator interface
    /// </summary>
    internal interface IIconGenerationValidator : IBaseValidator<IconGenerationValidationResult>
    {
        /// <summary>
        /// Checks that the input identifies an existing file with a supported extension or a plausible HTTP image URL.
        /// </summary>
        /// <param name="filePath">The local file path or HTTP/HTTPS URL to validate.</param>
        /// <returns>A result indicating whether the input passes validation, with an error message on failure.</returns>
        /// <remarks>URL validation checks the address only; it does not download or decode the image.</remarks>
        IconGenerationValidationResult ValidateFile(string? filePath);

        /// <summary>
        /// Checks whether a supplied style value can be parsed as an icon style.
        /// </summary>
        /// <param name="styleValue">The style value to validate; null or empty is allowed for later prompting.</param>
        /// <returns>A result indicating whether the style passes validation, with an error message on failure.</returns>
        IconGenerationValidationResult ValidateStyle(string? styleValue);

        /// <summary>
        /// Checks whether a supplied template name is supported.
        /// </summary>
        /// <param name="templateName">The case-sensitive template name; null or empty is allowed for later prompting.</param>
        /// <returns>A result indicating whether the template passes validation, with an error message on failure.</returns>
        IconGenerationValidationResult ValidateTemplate(string? templateName);

        /// <summary>
        /// Checks that an output directory path is provided.
        /// </summary>
        /// <param name="directoryPath">The directory path to validate.</param>
        /// <returns>A successful result for a nonempty path, or a failed result with an error message.</returns>
        /// <remarks>This check does not verify that the directory exists or is writable.</remarks>
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