using LabsChallengeApi.Src.Shared.Infrastructure.Security.Token;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Token.Adapters;
using Microsoft.Extensions.Configuration;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Infrastructure.Security.Token;

[TestClass]
public class JwtTokenServiceTests
{
    private ITokenService _tokenService = null!;

    [TestInitialize]
    public void Setup()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            { "JwtSettings:SigningKey", "SuperSecretKey1234567890SuperSecretKey" },
            { "JwtSettings:ValidForSeconds", "3600" },
            { "JwtSettings:Issuer", "TestIssuer" },
            { "JwtSettings:Audience", "TestAudience" }
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();
        _tokenService = new JwtTokenService(configuration);
    }

    [TestMethod]
    public void GenerateToken_ShouldReturnValidTokenDto()
    {
        //GIVEN
        var name = "John Doe";
        var email = "john.doe@example.com";
        //WHEN
        var result = _tokenService.GenerateToken(name, email);
        //THEN
        Assert.IsNotNull(result);
        Assert.IsFalse(string.IsNullOrEmpty(result.AccessToken));
        Assert.AreEqual(3600, result.ExpiresIn);
    }
}