using System.Net;
using System.Text;
using System.Text.Json;
using LabsChallengeApi.Src.Shared.Infrastructure.Database;
using LabsChallengeApi.Src.Shared.Infrastructure.Database.Dtos;
using LabsChallengeApi.Src.Shared.Infrastructure.Database.Factories;
using LabsChallengeApi.Src.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Application.Controllers;

[TestClass]
public class AuthControllerE2EBroadTests
{
    private ISqlConnectionFactory _sqlConnectionFactory = null!;
    private ISqlConnection _connection = null!;
    private IConfiguration _configuration = null!;
    private static WebApplicationFactory<Program> _factory = null!;
    private static HttpClient _client = null!;

    [TestInitialize]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT_VARIABLE", "Development");
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Development.json")
            .Build();
        _sqlConnectionFactory = new SqlServerFactory();
        _connection = _sqlConnectionFactory.Create(_configuration.GetRequiredConnectionString("LabsChallengeDb"));
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TestMethod]
    public async Task Register_ShouldReturnCreated()
    {
        //GIVEN
        await CleanUp();
        var request = new
        {
            Url = "/api/v1/Auth/register",
            Body = new
            {
                Username = "User Test",
                Email = "user@hotmail.com",
                Password = "SecurePass123"
            }
        };
        var content = new StringContent(
            JsonSerializer.Serialize(request.Body),
            Encoding.UTF8,
            "application/json"
        );
        //WHEN
        var response = await _client.PostAsync(request.Url, content);
        //THEN
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        await CleanUp();
    }

    private async Task CleanUp()
    {
        //GIVEN
        var query = new MssqlQueryDto
        {
            Query = "TRUNCATE TABLE [Access_Control].[Users]"
        };
        await _connection.ExecuteNonQueryAsync(query);
    }
}
