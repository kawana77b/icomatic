using Icomatic.Core.Domain.Templates;

namespace Icomatic.Infrastructure.Contracts
{
    internal interface ITreeDisplayService
    {
        /// <summary>
        /// Displays template output filenames, dimensions, and formats as a console tree.
        /// </summary>
        /// <param name="templateInfos">The template entries to display.</param>
        /// <param name="prefix">The optional filename prefix; null omits the prefix.</param>
        void DisplayFilesAsTree(TemplateDefinitions.TemplateInfo[] templateInfos, string? prefix = null);

        /// <summary>
        /// Displays filenames as a console tree.
        /// </summary>
        /// <param name="filenames">The filenames to display in enumeration order.</param>
        void DisplayFilesAsTree(IEnumerable<string> filenames);

        /// <summary>
        /// Formats a filename with any supplied size and format details in parentheses.
        /// </summary>
        /// <param name="filename">The filename to format.</param>
        /// <param name="sizeInfo">The optional image dimension text.</param>
        /// <param name="formatInfo">The optional image format text.</param>
        /// <returns>The filename followed by the nonempty details, or just the filename if no details are supplied.</returns>
        string FormatFileEntry(string filename, string? sizeInfo = null, string? formatInfo = null);
    }
}