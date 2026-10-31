using System.CommandLine;

namespace Icomatic.Commands.Base
{
    internal interface ICommandHandler
    {
        /// <summary>
        /// Executes the command using parsed arguments and registered services.
        /// </summary>
        /// <param name="parseResult">The parsed command-line arguments.</param>
        /// <param name="serviceProvider">The provider used to resolve command dependencies.</param>
        /// <returns>Zero on success, or a nonzero exit code if the command fails or is cancelled.</returns>
        int Execute(ParseResult parseResult, IServiceProvider serviceProvider);
    }
}