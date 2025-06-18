using LabsChallengeApi.Shared.Infrastructure.Database.Dtos;
using Microsoft.Data.SqlClient;

namespace LabsChallengeApi.Shared.Infrastructure.Database.Adapters;

public class SqlServerAdapter : ISqlConnection
{
    private readonly string ConnectionString;

    public SqlServerAdapter(string connectionString)
    {
        ConnectionString = connectionString;
    }

    private async Task<SqlConnection> OpenConnection()
    {
        var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        return connection;
    }

    public async Task ExecuteNonQueryAsync(ISqlQuery input)
    {
        if (input is not MssqlQueryDto statement) throw new ArgumentException("input must be of type MssqlQueryDto", nameof(input));
        var isLocalTransaction = statement.Transaction == null;
        var connection = isLocalTransaction ? await OpenConnection() : statement.Transaction!.Connection;
        await using var command = new SqlCommand(statement.Query, connection, statement.Transaction);
        if (statement.Parameters != null)
            command.Parameters.AddRange(statement.Parameters);
        await command.ExecuteNonQueryAsync();
        if (isLocalTransaction)
        {
            await connection.CloseAsync();
            await connection.DisposeAsync();
        }
    }

    public async Task<List<Dictionary<string, object?>>> ExecuteQueryAsync(ISqlQuery input)
    {
        if (input is not MssqlQueryDto statement) throw new ArgumentException("input must be of type MssqlQueryDto", nameof(input));
        var isLocalTransaction = statement.Transaction == null;
        var connection = isLocalTransaction ? await OpenConnection() : statement.Transaction!.Connection;
        await using var command = new SqlCommand(statement.Query, connection, statement.Transaction);
        if (statement.Parameters != null)
            command.Parameters.AddRange(statement.Parameters);
        await using var reader = await command.ExecuteReaderAsync();
        List<Dictionary<string, object?>> result = [];
        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object?>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var key = reader.GetName(i);
                var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                if (!row.TryAdd(key, value)) row.Add($"DuplicatedName_{key}", value);
            }
            result.Add(row);
        }
        await reader.CloseAsync();
        if (isLocalTransaction)
        {
            await connection.CloseAsync();
            await connection.DisposeAsync();
        }
        return result;
    }
}
