using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

using Helium.Mapping;
using Helium.Provider;

namespace Helium
{
    public class DbClient
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly string _connectionString;

        private readonly DbMapperFactory _mapperFactory;

        public DbClient(DbProviderFactory providerFactory, string connectionString)
        {
            _providerFactory = providerFactory;
            _connectionString = connectionString;

            var dataReaderType = _providerFactory.GetDataReaderTypeDescriptor();
            _mapperFactory = new DbMapperFactory(dataReaderType);
        }

        public DbClientCommand Query(string query)
        {
            var command = CreateCommand(query, CommandType.Text);
            return new DbClientCommand(this, command);
        }

        public DbClientCommand Procedure(string procedure)
        {
            var command = CreateCommand(procedure, CommandType.StoredProcedure);
            return new DbClientCommand(this, command);
        }

        private DbCommand CreateCommand(string commandText, CommandType commandType)
        {
            var command = _providerFactory.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;

            return command;
        }

        internal int Execute(DbCommand command)
        {
            return command.Connection != null
                ? ExecuteWithConnection(command)
                : ExecuteWithoutConnection(command);
        }

        private int ExecuteWithConnection(DbCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            return command.ExecuteNonQuery();
        }

        private int ExecuteWithoutConnection(DbCommand command)
        {
            using var connection = CreateConnection();

            command.Connection = connection;

            connection.Open();

            return command.ExecuteNonQuery();
        }

        internal Task<int> ExecuteAsync(DbCommand command, CancellationToken cancellationToken)
        {
            return command.Connection != null
                ? ExecuteWithConnectionAsync(command, cancellationToken)
                : ExecuteWithoutConnectionAsync(command, cancellationToken);
        }

        private async Task<int> ExecuteWithConnectionAsync(DbCommand command, CancellationToken cancellationToken)
        {
            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync(cancellationToken);

            return await command.ExecuteNonQueryAsync(cancellationToken);
        }

        private async Task<int> ExecuteWithoutConnectionAsync(DbCommand command, CancellationToken cancellationToken)
        {
            using var connection = CreateConnection();

            command.Connection = connection;

            await command.Connection.OpenAsync(cancellationToken);

            return await command.ExecuteNonQueryAsync(cancellationToken);
        }

        internal T Execute<T>(DbCommand command)
        {
            var mapper = _mapperFactory.GetMapper<T>();

            return command.Connection != null
                ? ExecuteWithConnection(command, mapper)
                : ExecuteWithoutConnection(command, mapper);
        }

        private T ExecuteWithConnection<T>(DbCommand command, DbMapper<T> mapper)
        {
            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            using var reader = command.ExecuteReader(mapper.CommandBehavior);

            return mapper.Map(reader);
        }

        private T ExecuteWithoutConnection<T>(DbCommand command, DbMapper<T> mapper)
        {
            using var connection = CreateConnection();

            command.Connection = connection;

            connection.Open();

            using var reader = command.ExecuteReader(mapper.CommandBehavior);

            return mapper.Map(reader);
        }

        internal Task<T> ExecuteAsync<T>(DbCommand command, CancellationToken cancellationToken)
        {
            var mapper = _mapperFactory.GetMapper<T>();

            return command.Connection != null
                ? ExecuteWithConnectionAsync(command, mapper, cancellationToken)
                : ExecuteWithoutConnectionAsync(command, mapper, cancellationToken);
        }

        private async Task<T> ExecuteWithConnectionAsync<T>(DbCommand command, DbMapper<T> mapper, CancellationToken cancellationToken)
        {
            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync(cancellationToken);

            using var reader = await command.ExecuteReaderAsync(mapper.CommandBehavior, cancellationToken);

            return mapper.Map(reader);
        }

        private async Task<T> ExecuteWithoutConnectionAsync<T>(DbCommand command, DbMapper<T> mapper, CancellationToken cancellationToken)
        {
            using var connection = CreateConnection();

            command.Connection = connection;

            await connection.OpenAsync(cancellationToken);

            using var reader = await command.ExecuteReaderAsync(mapper.CommandBehavior, cancellationToken);

            return mapper.Map(reader);
        }

        private DbConnection CreateConnection()
        {
            var connection = _providerFactory.CreateConnection();

            connection.ConnectionString = _connectionString;

            return connection;
        }
    }
}
