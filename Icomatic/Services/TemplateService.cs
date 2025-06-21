using Icomatic.Core.Domain.Templates;
using Icomatic.Services.Contracts;

namespace Icomatic.Services
{
    internal class TemplateService : ITemplateService
    {
        public TemplateDefinitions.TemplateInfo[] GetTemplateInformations(string templateName)
        {
            if (!IsValidTemplate(templateName))
            {
                throw new NotSupportedException($"Not a supported template name: {templateName}");
            }

            var templates = TemplateDefinitions.GetTemplatesByName();
            return [.. templates.GetValueOrDefault(templateName)!];
        }

        public TemplateDefinitions.Category[] GetAvailableTemplates()
        {
            return [.. TemplateDefinitions.GetAvailableTemplates().Select(x => x.Key).OrderBy(x => x.GetName())];
        }

        public bool IsValidTemplate(string templateName)
        {
            var templates = TemplateDefinitions.GetTemplatesByName();
            return templates.ContainsKey(templateName);
        }
    }
}