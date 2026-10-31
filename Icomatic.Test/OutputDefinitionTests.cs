using Icomatic.Core.Domain.Models;
using Icomatic.Core.Domain.Templates;

namespace Icomatic.Test;

public class OutputDefinitionTests
{
    [Theory]
    [InlineData("icon.PNG", ".png")]
    [InlineData("icon.jpeg", ".jpg")]
    [InlineData("icon.JPG", ".jpg")]
    [InlineData("icon.tif", ".tiff")]
    [InlineData("icon.TIFF", ".tiff")]
    [InlineData("icon.ico", ".ico")]
    public void Format_RecognizesExtensionAndRoundTripsThroughMagick(string path, string extension)
    {
        var format = Format.Get(path);

        Assert.NotNull(format);
        Assert.Equal(extension, format.Value.GetDefaultExtension());
        Assert.Equal(format.Value, format.Value.ToMagickFormat().ToSupportedFormat());
    }

    [Theory]
    [InlineData("icon.txt")]
    [InlineData("icon.png.exe")]
    [InlineData("icon")]
    public void Format_RejectsUnsupportedOrMissingExtension(string path)
    {
        Assert.Null(Format.Get(path));
    }

    [Theory]
    [InlineData("", "favicon.ico")]
    [InlineData("  ", "favicon.ico")]
    [InlineData("my-app", "my-app-favicon.ico")]
    public void Filename_UsesOptionalPrefixAndTemplateFormat(string prefix, string expected)
    {
        var template = new TemplateDefinitions.TemplateInfo
        {
            Name = "favicon",
            Format = Format.SupportedFormat.ICO
        };

        Assert.Equal(expected, template.Filename(prefix));
    }

    [Fact]
    public void Templates_HaveUniqueOutputFilenamesWithinEachCategory()
    {
        Assert.All(TemplateDefinitions.GetTemplatesByName(), template =>
        {
            var definitions = template.Value.ToArray();
            Assert.NotEmpty(definitions);
            Assert.All(definitions, definition => Assert.False(string.IsNullOrWhiteSpace(definition.Name)));
            Assert.Equal(definitions.Length, definitions.Select(info => info.Filename()).Distinct().Count());
        });
    }
}
