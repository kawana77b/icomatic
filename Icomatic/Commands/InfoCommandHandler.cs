using Icomatic.Commands.Base;
using Icomatic.Core.Domain.Templates;
using Icomatic.Infrastructure.Contracts;
using Icomatic.Services.Contracts;
using System.CommandLine;

namespace Icomatic.Commands
{
    internal class InfoCommandHandler : BaseCommandHandler
    {
        public override int Execute(ParseResult parseResult, IServiceProvider serviceProvider)
        {
            var templateService = GetService<ITemplateService>(serviceProvider);
            var consoleUI = GetConsoleUI(serviceProvider);
            var treeDisplayService = GetService<ITreeDisplayService>(serviceProvider);

            var templateName = parseResult.GetValue<string>("template");
            if (string.IsNullOrEmpty(templateName))
            {
                consoleUI.WriteError("Template name must be specified");
                return 1;
            }

            if (!templateService.IsValidTemplate(templateName))
            {
                consoleUI.WriteError($"Invalid template: {templateName}");
                consoleUI.WriteInfo("Available templates:");
                var availableTemplates = templateService.GetAvailableTemplates();
                foreach (var template in availableTemplates.OrderBy(t => t.GetName()))
                {
                    consoleUI.WriteInfo($"  - {template.GetName()}");
                }
                return 1;
            }

            // Get template information
            var templateInfos = templateService.GetTemplateInformations(templateName);
            var category = GetCategoryFromName(templateName);

            // Display template information
            consoleUI.WriteInfo($"Template: {templateName}");
            if (category.HasValue)
            {
                consoleUI.WriteInfo($"Description: {category.Value.GetDescription()}");
            }
            consoleUI.WriteInfo($"Files to generate: {templateInfos.Length}");
            Console.WriteLine();

            // Display files in directory tree format
            treeDisplayService.DisplayFilesAsTree(templateInfos);

            return 0;
        }

        private static TemplateDefinitions.Category? GetCategoryFromName(string templateName)
        {
            var availableTemplates = TemplateDefinitions.GetAvailableTemplates();
            return availableTemplates.Keys.FirstOrDefault(k => k.GetName().Equals(templateName, StringComparison.OrdinalIgnoreCase));
        }
    }
}