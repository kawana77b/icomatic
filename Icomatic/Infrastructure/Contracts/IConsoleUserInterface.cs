using Icomatic.Core.Domain.Models;
using Icomatic.Core.Domain.Templates;

namespace Icomatic.Infrastructure.Contracts
{
    internal interface IConsoleUserInterface
    {
        /// <summary>
        /// Displays the generation settings and output files, then asks the user to confirm.
        /// </summary>
        /// <param name="infos">The template entries that will be generated.</param>
        /// <param name="saveDirectory">The output directory to display.</param>
        /// <param name="prefix">The filename prefix to display and apply to output names.</param>
        /// <param name="template">The selected template name.</param>
        /// <param name="style">The selected icon style.</param>
        /// <returns><see langword="true"/> if the user confirms; otherwise, <see langword="false"/>.</returns>
        bool PromptConfirmation(TemplateDefinitions.TemplateInfo[] infos, string saveDirectory, string prefix, string template, IconStyle.Style style);

        /// <summary>
        /// Writes a success message to the console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        void WriteSuccess(string message);

        /// <summary>
        /// Writes an error message to the standard error stream.
        /// </summary>
        /// <param name="message">The message to display.</param>
        void WriteError(string message);

        /// <summary>
        /// Writes a warning message to the console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        void WriteWarning(string message);

        /// <summary>
        /// Writes an informational message to the console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        void WriteInfo(string message);

        /// <summary>
        /// Reports a generated file to the console.
        /// </summary>
        /// <param name="filename">The generated file path to display.</param>
        void WriteGeneratedFile(string filename);
    }
}