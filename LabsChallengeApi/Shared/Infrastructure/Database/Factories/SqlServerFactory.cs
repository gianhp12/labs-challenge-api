using LabsChallengeApi.Shared.Infrastructure.Database.Adapters;

namespace LabsChallengeApi.Shared.Infrastructure.Database.Factories;

public class SqlServerFactory : ISqlConnectionFactory
{
    public ISqlConnection Create(string connectionString) => new SqlServerAdapter(connectionString);
}
