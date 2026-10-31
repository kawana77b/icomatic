using Icomatic.Core.Domain.Templates;

namespace Icomatic.Infrastructure.Contracts
{
    internal interface ITableDisplayService
    {
        /// <summary>
        /// Displays a table using the first public properties of each item as columns.
        /// </summary>
        /// <param name="data">The items to display.</param>
        /// <param name="columnHeaders">The column headings, which determine how many properties are displayed.</param>
        /// <typeparam name="T">The type of each data item.</typeparam>
        void DisplayTable<T>(IEnumerable<T> data, params string[] columnHeaders);

        /// <summary>
        /// Displays a table using a custom projection for each row.
        /// </summary>
        /// <param name="data">The items to display.</param>
        /// <param name="rowSelector">The function that supplies cell values for each item.</param>
        /// <param name="columnHeaders">The column headings in cell order.</param>
        /// <typeparam name="T">The type of each data item.</typeparam>
        void DisplayTable<T>(IEnumerable<T> data, Func<T, object[]> rowSelector, params string[] columnHeaders);

        /// <summary>
        /// Displays a two-column table of keys and values selected from each item.
        /// </summary>
        /// <param name="data">The items to display.</param>
        /// <param name="keySelector">The function that supplies the first column value.</param>
        /// <param name="valueSelector">The function that supplies the second column value.</param>
        /// <param name="keyHeader">The heading for the key column.</param>
        /// <param name="valueHeader">The heading for the value column.</param>
        /// <typeparam name="T">The type of each data item.</typeparam>
        void DisplayKeyValueTable<T>(IEnumerable<T> data, Func<T, string> keySelector, Func<T, string> valueSelector, string keyHeader = "Name", string valueHeader = "Value");

        /// <summary>
        /// Displays template category names and descriptions in a table.
        /// </summary>
        /// <param name="templates">The template categories to display.</param>
        void DisplayTemplatesTable(IEnumerable<TemplateDefinitions.Category> templates);
    }
}