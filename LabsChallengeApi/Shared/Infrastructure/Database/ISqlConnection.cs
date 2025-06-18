namespace LabsChallengeApi.Shared.Infrastructure.Database;

public interface ISqlConnection
{
    public Task ExecuteNonQueryAsync(ISqlQuery input);
    public Task<List<Dictionary<string, object?>>> ExecuteQueryAsync(ISqlQuery input);
}
