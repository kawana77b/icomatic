using Icomatic.Infrastructure.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

namespace Icomatic.Commands.Base
{
    internal abstract class BaseCommandHandler : ICommandHandler
    {
        public abstract int Execute(ParseResult parseResult, IServiceProvider serviceProvider);

        protected static IConsoleUserInterface GetConsoleUI(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IConsoleUserInterface>();
        }

        protected static T GetService<T>(IServiceProvider serviceProvider) where T : notnull
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }
}