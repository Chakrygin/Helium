using MySqlConnector;

namespace Helium.MySql.Helpers
{
    public static class MySqlUtils
    {
        public static DbClient CreateClient(string database)
        {
            var connectionString = CreateConnectionString(database);
            return new DbClient(MySqlConnectorFactory.Instance, connectionString);
        }

        public static string CreateConnectionString(string database)
        {
            var csb = new MySqlConnectionStringBuilder();

            csb.Server = "localhost";

            csb.Database = database;
            csb.UserID = "root";
            csb.Password = "Password12!";

            return csb.ToString();
        }
    }
}
