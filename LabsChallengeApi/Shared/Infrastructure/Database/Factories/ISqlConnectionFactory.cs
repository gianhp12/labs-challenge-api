namespace LabsChallengeApi.Shared.Infrastructure.Database.Factories;

public interface ISqlConnectionFactory
{
    public ISqlConnection Create(string connectionString);
}
