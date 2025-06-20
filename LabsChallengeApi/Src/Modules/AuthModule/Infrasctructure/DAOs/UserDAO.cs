using LabsChallengeApi.Src.Modules.AuthModule.Domain.DAOs;
using LabsChallengeApi.Src.Shared.Infrastructure.Database;
using LabsChallengeApi.Src.Shared.Infrastructure.Database.Dtos;
using LabsChallengeApi.Src.Shared.Infrastructure.Extensions;

namespace LabsChallengeApi.Src.Modules.AuthModule.Infrastructure.DAOs;

public class UserDAO : IUserDAO
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IConfiguration _configuration;

    public UserDAO(ISqlConnectionFactory sqlConnectionFactory, IConfiguration configuration)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _configuration = configuration;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var query = new MssqlQueryDto
        {
            Query = @"
            SELECT CASE 
                       WHEN EXISTS (
                           SELECT 1 
                           FROM [Access_Control].[Users] 
                           WHERE Email = @Email
                       ) THEN 1 
                       ELSE 0 
                   END AS ExistsResult",
            Parameters =
            [
                new ("@Email", email)
            ]
        };
        var connection = _sqlConnectionFactory.Create(
            _configuration.GetRequiredConnectionString("LabsChallengeDb")
        );
        var resultList = await connection.ExecuteQueryAsync(query);
        var existsValue = Convert.ToBoolean(resultList.First()["ExistsResult"]);
        return existsValue;
    }
}
