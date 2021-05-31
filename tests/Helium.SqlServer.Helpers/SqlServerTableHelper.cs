using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

using Bogus;

using Microsoft.Data.SqlClient;

namespace Helium.SqlServer.Helpers
{
    public sealed class SqlServerTableHelper<TRow>
    {
        public SqlServerTableHelper(string name, string connectionString)
        {
            Name = name;
            ConnectionString = connectionString;
        }

        public string Name { get; }

        public string ConnectionString { get; }

        public bool IsCreated { get; private set; }

        public bool IsDropped { get; private set; }

        public IReadOnlyCollection<TRow> Rows { get; private set; } = new TRow[0];

        public TRow Row => Rows.Single();

        public void Create(params string[] columns)
        {
            Execute($@"
                create table [dbo].[{Name}]
                (
                    {String.Join(", ", columns)}
                )");

            IsCreated = true;
        }

        public void Drop()
        {
            Execute($@"
                if object_id('[dbo].[{Name}]', 'U') is not null
                begin
                    drop table [dbo].[{Name}];
                end");

            IsDropped = true;
        }

        private void Execute(string query)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(query, connection);

            connection.Open();

            command.ExecuteNonQuery();
        }

        public void Fill(Func<Faker, TRow> func)
        {
            var faker = new Faker();
            var row = func(faker);

            Fill(row);
        }

        public void Fill(int count, Func<int, TRow> func)
        {
            var rows =
                from id in Enumerable.Range(1, count)
                select func(id);

            Fill(rows);
        }

        public void Fill(int count, Func<int, Faker, TRow> func)
        {
            var faker = new Faker();
            var rows =
                from id in Enumerable.Range(1, count)
                select func(id, faker);

            Fill(rows);
        }

        public void Fill(params TRow[] rows)
        {
            Fill(rows.AsEnumerable());
        }

        public void Fill(IEnumerable<TRow> rows)
        {
            var type = typeof(TRow);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var table = new DataTable();

            foreach (var property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            Rows = rows.ToList();

            foreach (var row in Rows)
            {
                var values = new object[properties.Length];

                var index = 0;
                foreach (var property in properties)
                {
                    var value = property.GetValue(row);
                    values[index++] = value;
                }

                table.Rows.Add(values);
            }

            using var bulk = new SqlBulkCopy(ConnectionString);

            bulk.DestinationTableName = Name;
            AddColumnOrderHints(bulk, table);

            bulk.WriteToServer(table);
        }

        private static void AddColumnOrderHints(SqlBulkCopy bulk, DataTable table)
        {
            var firstColumn = table.Columns[0];

            if (firstColumn.DataType == typeof(int))
            {
                int? previousValue = null;

                foreach (DataRow row in table.Rows)
                {
                    var value = (int) row[firstColumn];

                    if (previousValue == null)
                    {
                        previousValue = value;
                    }
                    else if (previousValue >= value)
                    {
                        return;
                    }
                }

                bulk.ColumnOrderHints.Add(firstColumn.ColumnName, SortOrder.Ascending);
            }
        }
    }
}
