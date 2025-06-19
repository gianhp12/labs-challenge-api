namespace LabsChallengeApi.Src.Shared.Infrastructure.Database;

public interface ISqlConnectionFactory
{
    public ISqlConnection Create(string connectionString);
}
