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

        public Task<int> ExecuteAsync(CancellationToken cancellationToken = default) =>
            _client.ExecuteAsync(_command, cancellationToken);
    }
}
