namespace Helium.Postgres.Helpers
{
    public static class PostgresExtensions
    {
        public static void CreateDatabase(this DbClient client, string databaseName)
        {
            client
                .Query($@"create database ""{databaseName}""")
                .Execute();
        }

        public static void DropDatabase(this DbClient client, string databaseName)
        {
            client
                .Query($@"
                    select pg_terminate_backend(pg_stat_activity.pid)
                    from pg_stat_activity
                    where pg_stat_activity.datname = '{databaseName}' and pid <> pg_backend_pid();

                    drop database if exists ""{databaseName}"";")
                .Execute();
        }
    }
}
