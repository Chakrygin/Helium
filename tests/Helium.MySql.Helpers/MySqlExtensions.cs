namespace Helium.MySql.Helpers
{
    public static class MySqlExtensions
    {
        public static void CreateDatabase(this DbClient client, string databaseName)
        {
            client
                .Query($"create database `{databaseName}`")
                .Execute();
        }

        public static void DropDatabase(this DbClient client, string databaseName)
        {
            client
                .Query($"drop database if exists `{databaseName}`;")
                .Execute();
        }
    }
}
