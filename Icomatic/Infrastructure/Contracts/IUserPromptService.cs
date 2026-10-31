using Icomatic.Core.Domain.Models;

namespace Icomatic.Infrastructure.Contracts
{
    internal interface IUserPromptService
    {
        /// <summary>
        /// Prompts the user to select an icon style.
        /// </summary>
        /// <param name="defaultValue">The display name of the initially selected style.</param>
        /// <returns>The selected icon style.</returns>
        IconStyle.Style PromptForStyle(string defaultValue = "round");

        /// <summary>
        /// Prompts the user to select one of the available templates.
        /// </summary>
        /// <param name="availableTemplates">The template names offered to the user.</param>
        /// <param name="defaultValue">The initially selected template name.</param>
        /// <returns>The selected template name.</returns>
        string PromptForTemplate(IEnumerable<string> availableTemplates, string defaultValue = "pwa");
    }
}