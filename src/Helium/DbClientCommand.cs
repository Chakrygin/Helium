using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Helium
{
    public sealed class DbClientCommand
    {
        private readonly DbClient _client;
        private readonly DbCommand _command;

        public DbClientCommand(DbClient client, DbCommand command)
        {
            _client = client;
            _command = command;
        }

        public int Execute() =>
            _client.Execute(_command);

        public Task<int> ExecuteAsync() =>
            _client.ExecuteAsync(_command, CancellationToken.None);

        public Task<int> ExecuteAsync(CancellationToken cancellationToken) =>
            _client.ExecuteAsync(_command, cancellationToken);

#nullable disable

        public T Execute<T>() =>
            _client.Execute<T>(_command);

        public Task<T> ExecuteAsync<T>() =>
            _client.ExecuteAsync<T>(_command, CancellationToken.None);

        public Task<T> ExecuteAsync<T>(CancellationToken cancellationToken) =>
            _client.ExecuteAsync<T>(_command, cancellationToken);

#nullable restore
    }
}
