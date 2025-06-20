using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.DAOs;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Hasher;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue.Adapters;
using LabsChallengeApiTests.Src.Modules.UserModule.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Application.Usecases;

[TestClass]
public class RegisterUserUsecaseBroadIntegrationTests
{
    private Mock<IUserRepository> _mockUserRepository = null!;
    private Mock<IUserDAO> _mockUserDAO = null!;
    private Mock<IPasswordHasher> _mockPasswordHasher = null!;
    private IConfiguration _configuration = null!;
    private IQueueService _queueService = null!;
    private RabbitMqTestHelper _rabbitHelper = null!;
    private RegisterUserUsecase _registerUserUsecase = null!;

    [TestInitialize]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT_VARIABLE", "Development");
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Development.json")
            .Build();
        _mockUserRepository = new Mock<IUserRepository>();
        _mockUserDAO = new Mock<IUserDAO>();
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _queueService = new RabbitMqAdapter(_configuration);
        _registerUserUsecase = new RegisterUserUsecase(_mockUserRepository.Object, _mockUserDAO.Object, _mockPasswordHasher.Object, _queueService);
        _rabbitHelper = new RabbitMqTestHelper(
            host: _configuration.GetValue<string>("QueueService:Host")!,
            port: _configuration.GetValue<int>("QueueService:Port"),
            username: _configuration.GetValue<string>("QueueService:Username")!,
            password: _configuration.GetValue<string>("QueueService:Password")!,
            vhost: _configuration.GetValue<string>("QueueService:VHost")!
        );
    }

    [TestMethod]
    public async Task CreateAsync_ShouldSendMessageForRabbitMq_WhenUserIsValidAndSaveInDb()
    {
        //GIVEN
        await _queueService.CreateConnection();
        await _queueService.CreateChannel();
        string queueName = "labs-challenge-api-email.confirmation";
        await _rabbitHelper.PurgeQueueAsync(queueName);
        var inputDto = new RegisterUserInputDto
        {
            Username = "John Doe",
            Email = "john.doe@hotmail.com",
            Password = "Teste1234@"
        };
        _mockUserDAO.Setup(dao => dao.ExistsByEmailAsync(It.IsAny<string>())).ReturnsAsync(false);
        _mockPasswordHasher.Setup(hasher => hasher.Hash(It.IsAny<string>())).Returns("Teste1234WithHash");
        _mockUserRepository.Setup(repository => repository.CreateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        //WHEN
        await _registerUserUsecase.ExecuteAsync(inputDto);
        //THEN
        var message = await _rabbitHelper.GetSingleMessageAsync(queueName);
        Assert.IsNotNull(message);
        var payload = JsonConvert.DeserializeObject<dynamic>(message!);
        Assert.AreEqual(inputDto.Email, (string)payload!.Email);
        Assert.AreEqual(inputDto.Username, (string)payload!.Name.Value);
    }
}
