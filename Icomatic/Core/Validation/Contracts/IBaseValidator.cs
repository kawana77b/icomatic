using System.CommandLine;

namespace Icomatic.Core.Validation.Contracts
{
    /// <summary>
    /// Base interface for command-line argument validators
    /// </summary>
    internal interface IBaseValidator<TResult>
    {
        /// <summary>
        /// Validates the parsed command-line arguments and returns the validation result
        /// </summary>
        /// <param name="parseResult">The parsed command-line arguments</param>
        /// <returns>The validation result containing success status and processed values</returns>
        TResult ValidateInputs(ParseResult parseResult);
    }

    /// <summary>
    /// Generic validation result for command validators
    /// </summary>
    /// <typeparam name="T">The type of the validation result data</typeparam>
    internal record BaseValidationResult<T>(
        bool IsValid,
        string? ErrorMessage = null,
        T? Data = default
    );
}