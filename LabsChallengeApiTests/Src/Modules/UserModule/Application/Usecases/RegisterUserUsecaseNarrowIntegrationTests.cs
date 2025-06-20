using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.DAOs;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Hasher;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue.Dtos;
using Moq;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Application.Usecases;

[TestClass]
public class CreateUserUsecaseNarrowIntegrationTests
{
    private Mock<IUserRepository> _mockUserRepository = null!;
    private Mock<IUserDAO> _mockUserDAO = null!;
    private Mock<IPasswordHasher> _mockPasswordHasher = null!;
    private Mock<IQueueService> _mockQueueService = null!;
    private RegisterUserUsecase _registerUserUsecase = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockUserDAO = new Mock<IUserDAO>();
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _mockQueueService = new Mock<IQueueService>();
        _registerUserUsecase = new RegisterUserUsecase(_mockUserRepository.Object, _mockUserDAO.Object, _mockPasswordHasher.Object, _mockQueueService.Object);
    }

    public async Task ExecuteAsync_ShouldCallRepositoryAndPasswordHasherAndQueueService_WhenInputDataIsValid()
    {
        //GIVEN
        var inputDto = new RegisterUserInputDto
        {
            Username = "John Doe",
            Email = "john.doe@hotmail.com",
            Password = "Teste1234"
        };
        _mockUserDAO.Setup(dao => dao.ExistsByEmailAsync(It.IsAny<string>())).ReturnsAsync(false);
        //WHEN
        await _registerUserUsecase.ExecuteAsync(inputDto);
        //THEN
        _mockPasswordHasher.Verify(hasher => hasher.Hash(It.IsAny<string>()), Times.Once);
        _mockUserRepository.Verify(repository => repository.CreateAsync(It.IsAny<User>()), Times.Once);
        _mockQueueService.Verify(queue => queue.SendMessage(It.IsAny<QueueMessageDto>()), Times.Once);
    }

    public async Task ExecuteAsync_ShouldNotCallRepositoryAndHaserAndQueueService_WhenEmailAlreadyExists()
    {
        //GIVEN
        var inputDto = new RegisterUserInputDto
        {
            Username = "John Doe",
            Email = "john.doe@hotmail.com",
            Password = "Teste1234"
        };
        _mockUserDAO.Setup(dao => dao.ExistsByEmailAsync(It.IsAny<string>())).ReturnsAsync(true);
        //WHEN
        await _registerUserUsecase.ExecuteAsync(inputDto);
        //THEN
        _mockUserRepository.Verify(repository => repository.CreateAsync(It.IsAny<User>()), Times.Never);
        _mockPasswordHasher.Verify(hasher => hasher.Hash(It.IsAny<string>()), Times.Never);
        _mockQueueService.Verify(queue => queue.SendMessage(It.IsAny<QueueMessageDto>()), Times.Never);
    }

    public async Task ExecuteAsync_ShouldNotCallRepositoryAndHasherAndQueueService_WhenThrowValidationException()
    {
        //GIVEN
        var inputDto = new RegisterUserInputDto
        {
            Username = "John Doe",
            Email = "john.doe@hotmail.com",
            Password = ""
        };
        _mockUserDAO.Setup(dao => dao.ExistsByEmailAsync(It.IsAny<string>())).ReturnsAsync(false);
        //WHEN
        await _registerUserUsecase.ExecuteAsync(inputDto);
        //THEN
        _mockUserRepository.Verify(repository => repository.CreateAsync(It.IsAny<User>()), Times.Never);
        _mockPasswordHasher.Verify(hasher => hasher.Hash(It.IsAny<string>()), Times.Never);
        _mockQueueService.Verify(queue => queue.SendMessage(It.IsAny<QueueMessageDto>()), Times.Never);
    }
}
