using Microsoft.Data.SqlClient;

namespace LabsChallengeApi.Src.Shared.Infrastructure.Database.Dtos;

public class MssqlQueryDto : ISqlQuery
{
    public required string Query { get; init; }
    public SqlParameter[]? Parameters { get; init; }
    public SqlTransaction? Transaction { get; init; }
}
