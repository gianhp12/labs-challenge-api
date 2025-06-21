using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
using Moq;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Application.Usecases;

[TestClass]
public class ValidateEmailTokenUsecaseNarrowIntegrationTests
{
    private Mock<IUserRepository> _mockUserRepository = null!;
    private ValidateEmailTokenUsecase _validateEmailTokenUsecase = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _validateEmailTokenUsecase = new ValidateEmailTokenUsecase(_mockUserRepository.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ShouldCallUpdateEmailConfirmedMethodInRepository_WhenTokenIsValid()
    {
        //GIVEN
        var inputDto = new ValidateEmailTokenInputDto
        {
            Email = "john.doe@hotmail.com",
            Token = "test-token"
        };
        var user = User.Restore(
            id: 1,
            name: "John Doe",
            email: "john.doe@hotmail.com",
            passwordHash: "password-hash",
            isEmailConfirmed: false,
            emailConfirmationToken: "test-token"
        );
        _mockUserRepository.Setup(repository => repository.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        //WHEN
        await _validateEmailTokenUsecase.ExecuteAsync(inputDto);
        //THEN
        _mockUserRepository.Verify(repository => repository.UpdateEmailConfirmedAsync(It.IsAny<User>()), Times.Once);
    }

    [TestMethod]
    public async Task ExecuteAsync_ShouldThrowValidationException_WhenTokenIsNotValid()
    {
        //GIVEN
        var inputDto = new ValidateEmailTokenInputDto
        {
            Email = "john.doe@hotmail.com",
            Token = "test-token"
        };
        var user = User.Restore(
            id: 1,
            name: "John Doe",
            email: "john.doe@hotmail.com",
            passwordHash: "password-hash",
            isEmailConfirmed: false,
            emailConfirmationToken: "token-test"
        );
        _mockUserRepository.Setup(repository => repository.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        //WHEN
        await Assert.ThrowsExceptionAsync<ValidationException>(() => _validateEmailTokenUsecase.ExecuteAsync(inputDto));
        //THEN
        _mockUserRepository.Verify(repository => repository.UpdateEmailConfirmedAsync(It.IsAny<User>()), Times.Never);
    }
}
