using Icomatic.Core.Domain.Templates;

namespace Icomatic.Services.Contracts
{
    internal interface ITemplateService
    {
        TemplateDefinitions.TemplateInfo[] GetTemplateInformations(string templateName);

        TemplateDefinitions.Category[] GetAvailableTemplates();

        bool IsValidTemplate(string templateName);
    }
}