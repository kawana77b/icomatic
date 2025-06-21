using System.CommandLine;

namespace Icomatic.Commands.Base
{
    internal interface ICommandHandler
    {
        int Execute(ParseResult parseResult, IServiceProvider serviceProvider);
    }
}