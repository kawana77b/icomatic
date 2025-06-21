using Icomatic.Core.Domain.Models;
using Icomatic.Core.Domain.Templates;

namespace Icomatic.Infrastructure.Contracts
{
    internal interface IConsoleUserInterface
    {
        bool PromptConfirmation(TemplateDefinitions.TemplateInfo[] infos, string saveDirectory, string prefix, string template, IconStyle.Style style);

        void WriteSuccess(string message);

        void WriteError(string message);

        void WriteWarning(string message);

        void WriteInfo(string message);

        void WriteGeneratedFile(string filename);
    }
}