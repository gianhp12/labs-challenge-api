using LabsChallengeApi.Src.Modules.UserModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.UserModule.Application.Usecases;
using LabsChallengeApi.Src.Modules.UserModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.UserModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Infrastructure.Hasher;
using Moq;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Application.Usecases;

[TestClass]
public class CreateUserUsecaseNarrowIntegrationTests
{
    private Mock<IUserRepository> _mockUserRepository = null!;
    private Mock<IPasswordHasher> _mockPasswordHasher = null!;
    private CreateUserUsecase _createUserUsecase = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _createUserUsecase = new CreateUserUsecase(_mockUserRepository.Object, _mockPasswordHasher.Object);
    }

    public async Task ExecuteAsync_ShouldCallRepositoryAndPasswordHasher_WhenInputDataIsValid()
    {
        //GIVEN
        var inputDto = new CreateUserInputDto
        {
            Username = "John Doe",
            Email = "john.doe@hotmail.com",
            Password = "Teste1234"
        };
        //WHEN
        await _createUserUsecase.ExecuteAsync(inputDto);
        //THEN
        _mockPasswordHasher.Verify(hasher => hasher.Hash(It.IsAny<string>()), Times.Once);
        _mockUserRepository.Verify(repository => repository.CreateAsync(It.IsAny<User>()), Times.Once);
    }

    public async Task ExecuteAsync_ShouldNotCallRepository_WhenThrowValidationException()
    {
        //GIVEN
        var inputDto = new CreateUserInputDto
        {
            Username = "John Doe",
            Email = "john.doe@hotmail.com",
            Password = ""
        };
        //WHEN
        await _createUserUsecase.ExecuteAsync(inputDto);
        //THEN
        _mockUserRepository.Verify(repository => repository.CreateAsync(It.IsAny<User>()), Times.Never);
    }

    public async Task ExecuteAsync_ShouldNotCallPasswordHasher_WhenThrowValidationException()
    {
        //GIVEN
        var inputDto = new CreateUserInputDto
        {
            Username = "John Doe",
            Email = "john.doe@hotmail.com",
            Password = ""
        };
        //WHEN
        await _createUserUsecase.ExecuteAsync(inputDto);
        //THEN
        _mockPasswordHasher.Verify(hasher => hasher.Hash(It.IsAny<string>()), Times.Never);
    }
}
