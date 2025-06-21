using ConsoleTables;
using Icomatic.Core.Domain.Templates;
using Icomatic.Infrastructure.Contracts;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

#pragma warning disable IL2095 // 'DynamicallyAccessedMemberTypes' on the generic parameter of method or type don't match overridden generic parameter method or type. All overridden members must have the same 'DynamicallyAccessedMembersAttribute' usage.

namespace Icomatic.Infrastructure.Console
{
    internal class TableDisplayService : ITableDisplayService
    {
        /// <summary>
        /// Displays a generic table with automatic property mapping
        /// </summary>

        public void DisplayTable<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>

        (IEnumerable<T> data, params string[] columnHeaders)
        {
            if (!data.Any())
            {
                System.Console.WriteLine("No data to display.");
                return;
            }

            var table = new ConsoleTable(columnHeaders);

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var item in data)
            {
                var values = properties.Take(columnHeaders.Length)
                    .Select(prop => prop.GetValue(item)?.ToString() ?? string.Empty)
                    .ToArray();
                table.AddRow(values);
            }

            ConfigureAndWriteTable(table);
        }

        /// <summary>
        /// Displays a table with custom row selector
        /// </summary>
        public void DisplayTable<T>(IEnumerable<T> data, Func<T, object[]> rowSelector, params string[] columnHeaders)
        {
            if (!data.Any())
            {
                System.Console.WriteLine("No data to display.");
                return;
            }

            var table = new ConsoleTable(columnHeaders);

            foreach (var item in data)
            {
                var row = rowSelector(item);
                table.AddRow(row);
            }

            ConfigureAndWriteTable(table);
        }

        /// <summary>
        /// Displays a key-value table
        /// </summary>
        public void DisplayKeyValueTable<T>(IEnumerable<T> data, Func<T, string> keySelector, Func<T, string> valueSelector, string keyHeader = "Name", string valueHeader = "Value")
        {
            DisplayTable(data, item => new object[] { keySelector(item), valueSelector(item) }, keyHeader, valueHeader);
        }

        /// <summary>
        /// Specialized method for displaying template categories
        /// </summary>
        public void DisplayTemplatesTable(IEnumerable<TemplateDefinitions.Category> templates)
        {
            DisplayKeyValueTable(
                templates,
                template => template.GetName(),
                template => template.GetDescription(),
                "Name",
                "Description"
            );
        }

        /// <summary>
        /// Configures and writes the table with consistent formatting
        /// </summary>
        private static void ConfigureAndWriteTable(ConsoleTable table)
        {
            table
                .Configure(configure =>
                {
                    configure.EnableCount = false;
                    configure.NumberAlignment = Alignment.Right;
                })
                .Write();

            System.Console.WriteLine();
        }
    }
}

#pragma warning restore IL2095 // 'DynamicallyAccessedMemberTypes' on the generic parameter of method or type don't match overridden generic parameter method or type. All overridden members must have the same 'DynamicallyAccessedMembersAttribute' usage.