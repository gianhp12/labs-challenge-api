using LabsChallengeApi.Src.Shared.Infrastructure.Database.Adapters;

namespace LabsChallengeApi.Src.Shared.Infrastructure.Database.Factories;

public class SqlServerFactory : ISqlConnectionFactory
{
    public ISqlConnection Create(string connectionString) => new SqlServerAdapter(connectionString);
}
