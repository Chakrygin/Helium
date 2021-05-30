namespace Helium.SqlServer.Helpers
{
    public static class SqlServerExtensions
    {
        public static void CreateDatabase(this DbClient client, string databaseName)
        {
            client
                .Query($"create database [{databaseName}]")
                .Execute();
        }

        public static void DropDatabase(this DbClient client, string databaseName)
        {
            client
                .Query($@"
                    if db_id('{databaseName}') is not null
                    begin
                        alter database [{databaseName}] set single_user with rollback immediate;
                        drop database [{databaseName}];
                    end")
                .Execute();
        }
    }
}
