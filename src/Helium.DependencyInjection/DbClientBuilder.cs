using Microsoft.Extensions.DependencyInjection;

namespace Helium.DependencyInjection
{
    internal sealed class DbClientBuilder : IDbClientBuilder
    {
        public DbClientBuilder(IServiceCollection services, string name)
        {
            Services = services;
            Name = name;
        }

        public IServiceCollection Services { get; }

        public string Name { get; }
    }
}
