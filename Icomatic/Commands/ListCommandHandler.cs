using Icomatic.Commands.Base;
using Icomatic.Infrastructure.Contracts;
using Icomatic.Services.Contracts;
using System.CommandLine;

namespace Icomatic.Commands
{
    internal class ListCommandHandler : BaseCommandHandler
    {
        public override int Execute(ParseResult parseResult, IServiceProvider serviceProvider)
        {
            var templateService = GetService<ITemplateService>(serviceProvider);
            var tableDisplayService = GetService<ITableDisplayService>(serviceProvider);

            var templates = templateService.GetAvailableTemplates();
            tableDisplayService.DisplayTemplatesTable(templates);

            return 0;
        }
    }
}