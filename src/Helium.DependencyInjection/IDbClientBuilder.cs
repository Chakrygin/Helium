using Microsoft.Extensions.DependencyInjection;

namespace Helium.DependencyInjection
{
    public interface IDbClientBuilder
    {
        IServiceCollection Services { get; }

        string Name { get; }
    }
}
