using System;
using System.Collections.Concurrent;

using Microsoft.Extensions.Options;

namespace Helium.DependencyInjection
{
    internal sealed class DbClientFactory : IDbClientFactory
    {
        private readonly IOptionsMonitor<DbClientFactoryOptions> _optionsMonitor;
        private readonly IServiceProvider _serviceProvider;

        private readonly ConcurrentDictionary<string, DbClient> _clients;
        private readonly Func<string, DbClient> _clientFactory;

        public DbClientFactory(
            IOptionsMonitor<DbClientFactoryOptions> optionsMonitor,
            IServiceProvider serviceProvider)
        {
            _optionsMonitor = optionsMonitor;
            _serviceProvider = serviceProvider;

            _clients = new ConcurrentDictionary<string, DbClient>();
            _clientFactory = CreateClientInternal;
        }

        public DbClient CreateClient(string name)
        {
            return _clients.GetOrAdd(name, _clientFactory);
        }

        private DbClient CreateClientInternal(string name)
        {
            var builder = new DbClientOptionsBuilder();

            var options = _optionsMonitor.Get(name);
            if (options.ConfigureActions.Count == 0)
            {
                var message = name.Length == 0
                    ? $"Failed to create DbClient. Default client is not configured."
                    : $"Failed to create DbClient. Client named \"{name}\" is not configured.";

                throw new InvalidOperationException(message);
            }

            foreach (var configure in options.ConfigureActions)
            {
                configure(builder, _serviceProvider);
            }

            var clientOptions = builder.Build();
            var client = new DbClient(clientOptions);

            return client;
        }
    }
}
