using System;
using System.Reflection;

using FluentMigrator.Runner;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Helium.Sandbox.Migrations
{
    public static class MigrationRunner
    {
        public static void Migrate(string connectionString, Assembly assembly)
        {
            using var serviceProvider = CreateServiceProvider(connectionString, assembly);
            using var scope = serviceProvider.CreateScope();

            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            if (runner.HasMigrationsToApplyUp())
            {
                runner.MigrateUp();
            }
        }

        private static ServiceProvider CreateServiceProvider(string connectionString, Assembly assembly)
        {
            var services = new ServiceCollection();

            services
                .AddFluentMigratorCore()
                .ConfigureRunner(builder => builder
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(assembly).For.All())
                .AddLogging(builder => builder
                    .AddFluentMigratorConsole());

            services.Configure<FluentMigratorLoggerOptions>(options =>
            {
                options.ShowSql = true;
                options.ShowElapsedTime = true;
            });

            return services.BuildServiceProvider();
        }
    }
}
