using System;
using System.Data.Common;

namespace Helium
{
    public sealed class DbClientOptionsBuilder
    {
        private DbProviderFactory? _providerFactory;
        private string? _connectionString;

        public DbClientOptionsBuilder UseProviderFactory(DbProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
            return this;
        }

        public DbClientOptionsBuilder UseConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        public DbClientOptions Build()
        {
            var result = new DbClientOptions();

            var providerFactory = _providerFactory;
            if (providerFactory == null)
                throw new InvalidOperationException("providerFactory == null");

            var connectionString = _connectionString ?? "";

            result.ProviderFactory = providerFactory;
            result.ConnectionString = connectionString;

            return result;
        }
    }
}
