using Icomatic.Core.Domain.Templates;

namespace Icomatic.Infrastructure.Contracts
{
    internal interface ITableDisplayService
    {
        void DisplayTable<T>(IEnumerable<T> data, params string[] columnHeaders);

        void DisplayTable<T>(IEnumerable<T> data, Func<T, object[]> rowSelector, params string[] columnHeaders);

        void DisplayKeyValueTable<T>(IEnumerable<T> data, Func<T, string> keySelector, Func<T, string> valueSelector, string keyHeader = "Name", string valueHeader = "Value");

        void DisplayTemplatesTable(IEnumerable<TemplateDefinitions.Category> templates);
    }
}