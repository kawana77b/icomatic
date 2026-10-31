using Icomatic.Core.Domain.Templates;
using Icomatic.Infrastructure.Console;

namespace Icomatic.Test;

[CollectionDefinition("Console output", DisableParallelization = true)]
public class ConsoleOutputCollection;

[Collection("Console output")]
public class TableDisplayServiceTests
{
    [Fact]
    public void DisplayTemplatesTable_KeepsEachDescriptionOnTheSameRowAsItsName()
    {
        var templates = Enum.GetValues<TemplateDefinitions.Category>();
        using var output = new StringWriter();
        var originalOutput = Console.Out;

        try
        {
            Console.SetOut(output);
            new TableDisplayService().DisplayTemplatesTable(templates);
        }
        finally
        {
            Console.SetOut(originalOutput);
        }

        var rows = output.ToString()
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Where(line => line.TrimStart().StartsWith('|'))
            .Skip(1)
            .Select(line => line.Split('|').Select(cell => cell.Trim()).ToArray())
            .ToArray();

        Assert.Equal(templates.Length, rows.Length);
        for (var index = 0; index < templates.Length; index++)
        {
            Assert.Equal(templates[index].GetName(), rows[index][1]);
            Assert.Equal(templates[index].GetDescription(), rows[index][2]);
        }
    }
}
