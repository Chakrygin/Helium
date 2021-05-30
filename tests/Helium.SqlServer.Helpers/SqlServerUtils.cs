using Microsoft.Data.SqlClient;

namespace Helium.SqlServer.Helpers
{
    public static class SqlServerUtils
    {
        public static DbClient CreateClient(string initialCatalog)
        {
            var connectionString = CreateConnectionString(initialCatalog);
            return new DbClient(SqlClientFactory.Instance, connectionString);
        }

        public static string CreateConnectionString(string initialCatalog)
        {
            var csb = new SqlConnectionStringBuilder();

            csb.DataSource = "localhost";
            csb.InitialCatalog = initialCatalog;
            csb.UserID = "sa";
            csb.Password = "Password12!";

            return csb.ToString();
        }
    }
}
