namespace LabsChallengeApi.Shared.Infrastructure.Database;

public interface ISqlConnectionFactory
{
    public ISqlConnection Create(string connectionString);
}
