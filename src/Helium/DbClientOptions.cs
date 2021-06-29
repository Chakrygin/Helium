using System.Data.Common;

namespace Helium
{
    public sealed class DbClientOptions
    {
        public DbProviderFactory ProviderFactory { get; internal set; } = null!;

        public string ConnectionString { get; internal set; } = null!;
    }
}
