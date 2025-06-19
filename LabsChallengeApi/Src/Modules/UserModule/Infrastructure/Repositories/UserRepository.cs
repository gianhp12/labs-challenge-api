using LabsChallengeApi.Src.Modules.UserModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.UserModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Infrastructure.Database;
using LabsChallengeApi.Src.Shared.Infrastructure.Database.Dtos;
using LabsChallengeApi.Src.Shared.Infrastructure.Extensions;

namespace LabsChallengeApi.Src.Modules.UserModule.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IConfiguration _configuration;

    public UserRepository(ISqlConnectionFactory sqlConnectionFactory, IConfiguration configuration)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _configuration = configuration;
    }

    public async Task CreateAsync(User user)
    {
        var query = new MssqlQueryDto()
        {
            Query = @"INSERT INTO [Access_Control].[Users] (Name, Email, PasswordHash, IsEmailConfirmed, CreatedAt, UpdatedAt)
                    VALUES (@Name, @Email, @PasswordHash, @IsEmailConfirmed, @CreatedAt, @UpdatedAt)",
            Parameters = [
                new ("@Name", user.Name),
                new ("@Email",user.Email.Value),
                new ("@PasswordHash", user.PasswordHash),
                new ("@IsEmailConfirmed", user.IsEmailConfirmed),
                new ("@CreatedAt", DateTime.Now),
                new ("@UpdatedAt", DateTime.Now),
            ]
        };
        var connection = _sqlConnectionFactory.Create(_configuration.GetRequiredConnectionString("LabsChallengeDb"));
        await connection.ExecuteNonQueryAsync(query);
    }
}
