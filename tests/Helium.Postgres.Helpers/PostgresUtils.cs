using Npgsql;

namespace Helium.Postgres.Helpers
{
    public static class PostgresUtils
    {
        public static DbClient CreateClient(string database)
        {
            var connectionString = CreateConnectionString(database);
            return new DbClient(NpgsqlFactory.Instance, connectionString);
        }

        public static string CreateConnectionString(string database)
        {
            var csb = new NpgsqlConnectionStringBuilder();

            csb.Host = "localhost";
            csb.Port = 5432;

            csb.Database = database;
            csb.Username = "postgres";
            csb.Password = "postgres";

            return csb.ToString();
        }
    }
}
