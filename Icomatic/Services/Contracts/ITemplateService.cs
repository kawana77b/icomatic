using Icomatic.Core.Domain.Templates;

namespace Icomatic.Services.Contracts
{
    internal interface ITemplateService
    {
        /// <summary>
        /// Gets the icon definitions for a template name.
        /// </summary>
        /// <param name="templateName">The exact, case-sensitive template name.</param>
        /// <returns>The output definitions for the requested template.</returns>
        /// <exception cref="NotSupportedException">The template name is not supported.</exception>
        TemplateDefinitions.TemplateInfo[] GetTemplateInformations(string templateName);

        /// <summary>
        /// Gets all available template categories ordered by display name.
        /// </summary>
        /// <returns>The available template categories.</returns>
        TemplateDefinitions.Category[] GetAvailableTemplates();

        /// <summary>
        /// Checks whether a template name is supported.
        /// </summary>
        /// <param name="templateName">The exact, case-sensitive template name to check.</param>
        /// <returns><see langword="true"/> if the template exists; otherwise, <see langword="false"/>.</returns>
        bool IsValidTemplate(string templateName);
    }
}