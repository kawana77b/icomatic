using Icomatic.Commands;
using Icomatic.Core.Validation;
using Icomatic.Core.Validation.Contracts;
using Icomatic.Infrastructure.Console;
using Icomatic.Infrastructure.Contracts;
using Icomatic.Services;
using Icomatic.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

namespace Icomatic
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var rootCommand = CreateRootCommand(serviceProvider);
            var listCommand = CreateListCommand(serviceProvider);
            var infoCommand = CreateInfoCommand(serviceProvider);

            rootCommand.Subcommands.Add(listCommand);
            rootCommand.Subcommands.Add(infoCommand);

            return rootCommand.Parse(args).Invoke();
        }

        private static RootCommand CreateRootCommand(IServiceProvider serviceProvider)
        {
            var rootCommand = new RootCommand("Generates icon images.\n");
            rootCommand.Arguments.Add(new Argument<string>("file")
            {
                Description = "Specifies the path to the source file, which can also be a URL.",
            });
            rootCommand.Options.Add(new Option<string>("--style", "-s")
            {
                DefaultValueFactory = parseResult => string.Empty,
                Description = "Specify the icon style (plane, round, circle)."
            });
            rootCommand.Options.Add(new Option<string>("--name", "-n")
            {
                DefaultValueFactory = parseResult => string.Empty,
                Description = "Specifies the base name of the generated icon name prefix."
            });
            rootCommand.Options.Add(new Option<string>("--template", "-t")
            {
                DefaultValueFactory = parseResult => string.Empty,
                Description = "Specify the template name."
            });
            rootCommand.Options.Add(new Option<string>("--dir", "-d")
            {
                DefaultValueFactory = parseResult => Directory.GetCurrentDirectory(),
                Description = "Specify the output directory."
            });

            rootCommand.SetAction(parseResult =>
            {
                try
                {
                    var handler = new RootCommandHandler();
                    return handler.Execute(parseResult, serviceProvider);
                }
                catch (Exception ex)
                {
                    var consoleUI = serviceProvider.GetRequiredService<IConsoleUserInterface>();
                    consoleUI.WriteError($"{ex.GetType().Name}: {ex.Message}");
                    return 1;
                }
            });

            return rootCommand;
        }

        private static Command CreateListCommand(IServiceProvider serviceProvider)
        {
            var listCommand = new Command("list", "Displays a list of templates.");
            listCommand.SetAction(parseResult =>
            {
                var handler = new ListCommandHandler();
                return handler.Execute(parseResult, serviceProvider);
            });
            return listCommand;
        }

        private static Command CreateInfoCommand(IServiceProvider serviceProvider)
        {
            var infoCommand = new Command("info", "Displays detailed information about a template.");
            infoCommand.Arguments.Add(new Argument<string>("template"));
            infoCommand.SetAction(parseResult =>
            {
                var handler = new InfoCommandHandler();
                return handler.Execute(parseResult, serviceProvider);
            });
            return infoCommand;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IIconGenerationValidator, IconGenerationValidator>();
            services.AddSingleton<ITemplateService, TemplateService>();
            services.AddSingleton<IUserPromptService, UserPromptService>();
            services.AddSingleton<IConsoleUserInterface, ConsoleUserInterface>();
            services.AddSingleton<ITreeDisplayService, TreeDisplayService>();
            services.AddSingleton<ITableDisplayService, TableDisplayService>();
            services.AddSingleton<IImageProcessingService, ImageProcessingService>();
            services.AddSingleton<IIconGenerationService, IconGenerationService>();
            services.AddSingleton<IWebImageDownloadService, WebImageDownloadService>();
        }
    }
}