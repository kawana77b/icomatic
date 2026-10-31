using Icomatic.Core.Domain.Models;
using Icomatic.Core.Validation;
using Icomatic.Infrastructure.Contracts;
using System.CommandLine;

namespace Icomatic.Test;

public class IconGenerationValidatorTests
{
    private const string ImageUrl = "https://example.com/image.png?version=1";

    [Fact]
    public void ValidateInputs_UsesExplicitOptionsWithoutPrompting()
    {
        var prompts = new TestPromptService();
        var validator = new IconGenerationValidator(prompts);

        var result = validator.ValidateInputs(Parse(ImageUrl, "--dir", "output", "--name", "app",
            "--style", "CiRcLe", "--template", "pwa"));

        Assert.True(result.IsValid, result.ErrorMessage);
        Assert.Equal(ImageUrl, result.File);
        Assert.Equal("output", result.SaveDirectory);
        Assert.Equal("app", result.Prefix);
        Assert.Equal(IconStyle.Style.Circle, result.Style);
        Assert.Equal("pwa", result.Template);
        Assert.Equal(0, prompts.StyleCalls);
        Assert.Equal(0, prompts.TemplateCalls);
    }

    [Fact]
    public void ValidateInputs_PromptsForOmittedStyleAndTemplate()
    {
        var prompts = new TestPromptService();
        var validator = new IconGenerationValidator(prompts);

        var result = validator.ValidateInputs(Parse(ImageUrl, "--dir", "output"));

        Assert.True(result.IsValid, result.ErrorMessage);
        Assert.Equal(IconStyle.Style.Circle, result.Style);
        Assert.Equal("web", result.Template);
        Assert.Equal(1, prompts.StyleCalls);
        Assert.Equal(1, prompts.TemplateCalls);
    }

    [Theory]
    [InlineData("invalid", "pwa", "Invalid style:")]
    [InlineData("round", "missing-template", "Invalid template:")]
    public void ValidateInputs_RejectsInvalidOptionsWithoutPrompting(string style, string template, string error)
    {
        var prompts = new TestPromptService();
        var validator = new IconGenerationValidator(prompts);

        var result = validator.ValidateInputs(Parse(ImageUrl, "--dir", "output", "--style", style,
            "--template", template));

        Assert.False(result.IsValid);
        Assert.StartsWith(error, result.ErrorMessage);
        Assert.Equal(0, prompts.StyleCalls);
        Assert.Equal(0, prompts.TemplateCalls);
    }

    private static ParseResult Parse(params string[] args)
    {
        var command = new RootCommand
        {
            Arguments = { new Argument<string>("file") },
            Options =
            {
                new Option<string>("--dir"),
                new Option<string>("--name"),
                new Option<string>("--style"),
                new Option<string>("--template")
            }
        };
        var result = command.Parse(args);
        Assert.Empty(result.Errors);
        return result;
    }

    private sealed class TestPromptService : IUserPromptService
    {
        public int StyleCalls { get; private set; }
        public int TemplateCalls { get; private set; }

        public IconStyle.Style PromptForStyle(string defaultValue = "round")
        {
            StyleCalls++;
            return IconStyle.Style.Circle;
        }

        public string PromptForTemplate(IEnumerable<string> availableTemplates, string defaultValue = "pwa")
        {
            TemplateCalls++;
            Assert.Contains("web", availableTemplates);
            return "web";
        }
    }
}
