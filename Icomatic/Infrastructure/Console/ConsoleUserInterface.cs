using Icomatic.Core.Domain.Models;
using Icomatic.Core.Domain.Templates;
using Icomatic.Infrastructure.Contracts;
using Sharprompt;

namespace Icomatic.Infrastructure.Console
{
    internal class ConsoleUserInterface : IConsoleUserInterface
    {
        /// <inheritdoc />
        public bool PromptConfirmation(TemplateDefinitions.TemplateInfo[] infos, string saveDirectory, string prefix, string template, IconStyle.Style style)
        {
            WriteInfo($"Template: {template}");
            WriteInfo($"Style: {style.GetName()}");
            WriteInfo($"Output directory: {saveDirectory}");
            if (!string.IsNullOrEmpty(prefix))
            {
                WriteInfo($"Prefix: {prefix}");
            }
            System.Console.WriteLine();

            WriteWarning("The following files will be generated:");
            foreach (var info in infos)
            {
                var filename = Path.Join(saveDirectory, info.Filename(prefix));
                System.Console.WriteLine($"  {filename}");
            }
            return Prompt.Confirm("Do you want to proceed?");
        }

        /// <inheritdoc />
        public void WriteSuccess(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }

        /// <inheritdoc />
        public void WriteError(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.Error.WriteLine(message);
            System.Console.ResetColor();
        }

        /// <inheritdoc />
        public void WriteWarning(string message)
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }

        /// <inheritdoc />
        public void WriteInfo(string message)
        {
            System.Console.WriteLine(message);
        }

        /// <inheritdoc />
        public void WriteGeneratedFile(string filename)
        {
            WriteInfo($"Generated: {filename}");
        }
    }
}