using System.Reflection;

using Helium.DependencyInjection;
using Helium.Sandbox.Migrations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Npgsql;

namespace Helium.Sandbox
{
    public sealed class Startup
    {
        public const string ConnectionString =
            @"Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=postgres";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbClient("Postgres", options =>
            {
                options.UseProviderFactory(NpgsqlFactory.Instance);
                options.UseConnectionString(ConnectionString);
            });

            services.AddSingleton<DbClient>(serviceProvider =>
            {
                var clientFactory = serviceProvider.GetRequiredService<IDbClientFactory>();
                var client = clientFactory.CreateClient("Postgres");

                return client;
            });

            services.AddRazorPages();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var assembly = Assembly.Load(env.ApplicationName);
            MigrationRunner.Migrate(ConnectionString, assembly);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
