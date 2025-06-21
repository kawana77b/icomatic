using Icomatic.Core.Domain.Templates;
using Icomatic.Infrastructure.Contracts;

namespace Icomatic.Infrastructure.Console
{
    internal class TreeDisplayService : ITreeDisplayService
    {
        public void DisplayFilesAsTree(TemplateDefinitions.TemplateInfo[] templateInfos, string? prefix = null)
        {
            System.Console.WriteLine("Generated files structure:");
            System.Console.WriteLine(".");

            for (int i = 0; i < templateInfos.Length; i++)
            {
                var info = templateInfos[i];
                var isLast = i == templateInfos.Length - 1;
                var treePrefix = isLast ? "└── " : "├── ";
                var filename = info.Filename(prefix ?? string.Empty);
                var sizeInfo = $"{info.Size.Width}x{info.Size.Height}";
                var formatInfo = info.Format.ToString().ToUpper();

                System.Console.WriteLine($"{treePrefix}{filename} ({sizeInfo}, {formatInfo})");
            }
        }

        public void DisplayFilesAsTree(IEnumerable<string> filenames)
        {
            System.Console.WriteLine("Generated files structure:");
            System.Console.WriteLine(".");

            var fileArray = filenames.ToArray();
            for (int i = 0; i < fileArray.Length; i++)
            {
                var filename = fileArray[i];
                var isLast = i == fileArray.Length - 1;
                var treePrefix = isLast ? "└── " : "├── ";

                System.Console.WriteLine($"{treePrefix}{filename}");
            }
        }

        public string FormatFileEntry(string filename, string? sizeInfo = null, string? formatInfo = null)
        {
            var details = new List<string>();
            if (!string.IsNullOrEmpty(sizeInfo)) details.Add(sizeInfo);
            if (!string.IsNullOrEmpty(formatInfo)) details.Add(formatInfo);

            return details.Count > 0
                ? $"{filename} ({string.Join(", ", details)})"
                : filename;
        }
    }
}