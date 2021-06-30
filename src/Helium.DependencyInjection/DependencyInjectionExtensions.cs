using System;
using System.Linq;

using Helium;
using Helium.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IDbClientBuilder AddDbClient(this IServiceCollection services,
            string name, Action<DbClientOptionsBuilder> configure)
        {
            return services.AddDbClientCore(name)
                .ConfigureDbClient(configure);
        }

        public static IDbClientBuilder AddDbClient(this IServiceCollection services,
            string name, Action<DbClientOptionsBuilder, IServiceProvider> configure)
        {
            return services.AddDbClientCore(name)
                .ConfigureDbClient(configure);
        }

        private static IDbClientBuilder AddDbClientCore(this IServiceCollection services, string name)
        {
            if (services.All(x => x.ServiceType != typeof(IDbClientFactory)))
            {
                services.AddOptions();
                services.AddSingleton<IDbClientFactory, DbClientFactory>();
            }

            return new DbClientBuilder(services, name);
        }

        public static IDbClientBuilder ConfigureDbClient(this IDbClientBuilder builder,
            Action<DbClientOptionsBuilder> configure)
        {
            builder.Services.Configure<DbClientFactoryOptions>(builder.Name, options =>
            {
                options.ConfigureActions.Add((optionsBuilder, _) =>
                {
                    configure(optionsBuilder);
                });
            });

            return builder;
        }

        public static IDbClientBuilder ConfigureDbClient(this IDbClientBuilder builder,
            Action<DbClientOptionsBuilder, IServiceProvider> configure)
        {
            builder.Services.Configure<DbClientFactoryOptions>(builder.Name, options =>
            {
                options.ConfigureActions.Add((optionsBuilder, serviceProvider) =>
                {
                    configure(optionsBuilder, serviceProvider);
                });
            });

            return builder;
        }
    }
}
