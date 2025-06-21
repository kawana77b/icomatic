using Icomatic.Core.Domain.Templates;

namespace Icomatic.Infrastructure.Contracts
{
    internal interface ITreeDisplayService
    {
        void DisplayFilesAsTree(TemplateDefinitions.TemplateInfo[] templateInfos, string? prefix = null);

        void DisplayFilesAsTree(IEnumerable<string> filenames);

        string FormatFileEntry(string filename, string? sizeInfo = null, string? formatInfo = null);
    }
}