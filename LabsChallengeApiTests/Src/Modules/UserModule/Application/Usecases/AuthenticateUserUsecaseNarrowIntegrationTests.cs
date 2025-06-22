using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Hasher;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Token;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Token.Dtos;
using Moq;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Application.Usecases;

[TestClass]
public class AuthenticateUserUsecaseNarrowIntegrationTests
{
    private Mock<IUserRepository> _mockUserRepository = null!;
    private Mock<IPasswordHasher> _mockPasswordHasher = null!;
    private Mock<ITokenService> _mockTokenService = null!;
    private AuthenticateUserUsecase _authenticateUserUsecase = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _mockTokenService = new Mock<ITokenService>();
        _authenticateUserUsecase = new AuthenticateUserUsecase(_mockUserRepository.Object, _mockPasswordHasher.Object, _mockTokenService.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ShouldCallRepositoryAndHasherAndTokenService_WhenInputDataIsValid()
    {
        //GIVEN
        var inputDto = new AuthenticateInputDto
        {
            Email = "john.doe@hotmail.com",
            Password = "Teste1234@"
        };
        var user = User.Restore(
            id: 1,
            name: "John Doe",
            email: "john.doe@hotmail.com",
            passwordHash: "password-hash",
            isEmailConfirmed: true,
            emailConfirmationToken: "",
            emailTokenRequestedAt: DateTime.Now
        );
        var tokenDto = new TokenDto
        (
            accessToken: "access-token",
            expiresIn: 1000
        );
        _mockUserRepository.Setup(repository => repository.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockPasswordHasher.Setup(hasher => hasher.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        _mockTokenService.Setup(token => token.GenerateToken(It.IsAny<string>(), It.IsAny<string>())).Returns(tokenDto);
        //WHEN
        await _authenticateUserUsecase.ExecuteAsync(inputDto);
        //THEN
        _mockUserRepository.Verify(repository => repository.GetByEmailAsync(It.IsAny<string>()), Times.Once);
        _mockPasswordHasher.Verify(hasher => hasher.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _mockTokenService.Verify(token => token.GenerateToken(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [TestMethod]
    public async Task ExecuteAsync_ShouldNotCallTokenServiceAndReturnValidationException_WhenHasherReturnFalse()
    {
        //GIVEN
        var inputDto = new AuthenticateInputDto
        {
            Email = "john.doe@hotmail.com",
            Password = "Teste1234@"
        };
        var user = User.Restore(
            id: 1,
            name: "John Doe",
            email: "john.doe@hotmail.com",
            passwordHash: "password-hash",
            isEmailConfirmed: true,
            emailConfirmationToken: "",
            emailTokenRequestedAt: DateTime.Now
        );
        _mockUserRepository.Setup(repository => repository.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockPasswordHasher.Setup(hasher => hasher.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
        //WHEN  //THEN
        await Assert.ThrowsExceptionAsync<ValidationException>(() => _authenticateUserUsecase.ExecuteAsync(inputDto));
        _mockUserRepository.Verify(repository => repository.GetByEmailAsync(It.IsAny<string>()), Times.Once);
        _mockPasswordHasher.Verify(hasher => hasher.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _mockTokenService.Verify(token => token.GenerateToken(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}
