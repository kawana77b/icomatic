using Icomatic.Core.Domain.Models;

namespace Icomatic.Infrastructure.Contracts
{
    internal interface IUserPromptService
    {
        IconStyle.Style PromptForStyle(string defaultValue = "round");

        string PromptForTemplate(IEnumerable<string> availableTemplates, string defaultValue = "pwa");
    }
}