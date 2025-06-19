using LabsChallengeApi.Src.Modules.UserModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.UserModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
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
                new ("@Name", user.Name.Value),
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

    public async Task<User> GetByEmailAsync(string email)
    {
        var query = new MssqlQueryDto()
        {
            Query = @"SELECT TOP 1 Id, Name, Email, PasswordHash, IsEmailConfirmed, EmailConfirmationToken
                      FROM Access_Control].[Users] WHERE Email = @Email",
            Parameters = [
                new ("@Email", email)
            ]
        };
        var connection = _sqlConnectionFactory.Create(_configuration.GetRequiredConnectionString("LabsChallengeDb"));
        var resultList = await connection.ExecuteQueryAsync(query);
        if (resultList == null || resultList.Count == 0)
        {
            throw new NotFoundException("Nenhum usuário encontrado com o email informado");
        }
        var result = resultList.First();
        var user = User.Restore(
            id: (int)result["Id"]!,
            name: (string)result["Name"]!,
            email: (string)result["Email"]!,
            passwordHash: (string)result["PasswordHash"]!,
            isEmailConfirmed: (bool)result["IsEmailConfirmed"]!
        );
        return user;
    }
}
