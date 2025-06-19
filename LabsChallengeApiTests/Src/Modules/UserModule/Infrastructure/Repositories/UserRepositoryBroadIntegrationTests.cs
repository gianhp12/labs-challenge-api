using LabsChallengeApi.Src.Modules.UserModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.UserModule.Infrastructure.Repositories;
using LabsChallengeApi.Src.Shared.Infrastructure.Database;
using LabsChallengeApi.Src.Shared.Infrastructure.Database.Dtos;
using LabsChallengeApi.Src.Shared.Infrastructure.Database.Factories;
using LabsChallengeApi.Src.Shared.Infrastructure.Extensions;
using LabsChallengeApi.Src.Shared.Infrastructure.Hasher;
using LabsChallengeApi.Src.Shared.Infrastructure.Hasher.Adapters;
using Microsoft.Extensions.Configuration;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Infrastructure.Repositories;

[TestClass]
public class UserRepositoryBroadIntegrationTests
{
    private ISqlConnectionFactory _sqlConnectionFactory = null!;
    private IConfiguration _configuration = null!;
    private ISqlConnection _connection = null!;
    private UserRepository _userRepository = null!;
    private IPasswordHasher _passwordHasher = null!;

    [TestInitialize]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT_VARIABLE", "Development");
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Development.json")
            .Build();
        _sqlConnectionFactory = new SqlServerFactory();
        _userRepository = new UserRepository(_sqlConnectionFactory, _configuration);
        _passwordHasher = new BcryptPasswordHasher();
        _connection = _sqlConnectionFactory.Create(_configuration.GetRequiredConnectionString("LabsChallengeDb"));
    }

    [TestMethod]
    public async Task CreateAsync_ShouldSaveUserInDatabase_WhenUserIsValid()
    {
        //GIVEN
        var queryInit = new MssqlQueryDto
        {
            Query = "TRUNCATE TABLE [Access_Control].[Users]"
        };
        await _connection.ExecuteNonQueryAsync(queryInit);
        var user = User.Create(
            name: "John Doe",
            email: "john.doe@gmail.com",
            password: "J@hn1234"
        );
        var encryptPassword = _passwordHasher.Hash(user.Password!.Value);
        user.SetPasswordHash(encryptPassword);
        await _userRepository.CreateAsync(user);
        //THEN
        var queryCheck = new MssqlQueryDto()
        {
            Query = "SELECT Name, Email FROM [Access_Control].[Users] WHERE Name = @Name AND Email = @Email",
            Parameters = [
                new("@Name", user.Name.Value),
                new("@Email", user.Email.Value)
            ]
        };
        var result = await _connection.ExecuteQueryAsync(queryCheck);
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(user.Name.Value, result[0]["Name"]);
        Assert.AreEqual(user.Email.Value, result[0]["Email"]);
    }
}
