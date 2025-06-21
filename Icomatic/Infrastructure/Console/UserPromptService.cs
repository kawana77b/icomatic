using Icomatic.Core.Domain.Models;
using Icomatic.Infrastructure.Contracts;
using Sharprompt;

namespace Icomatic.Infrastructure.Console
{
    internal class UserPromptService : IUserPromptService
    {
        public IconStyle.Style PromptForStyle(string defaultValue = "round")
        {
            var styleOptions = IconStyle.Styles.Select(s => s.GetName()).ToArray();
            var selectedStyleName = Prompt.Select("Select icon style", styleOptions, defaultValue: defaultValue);

            if (Enum.TryParse<IconStyle.Style>(selectedStyleName, true, out var style))
            {
                return style;
            }
            throw new ArgumentException($"Invalid style: {selectedStyleName}");
        }

        public string PromptForTemplate(IEnumerable<string> availableTemplates, string defaultValue = "pwa")
        {
            var templateNames = availableTemplates.ToArray();
            return Prompt.Select("Select template", templateNames, defaultValue: defaultValue);
        }
    }
}