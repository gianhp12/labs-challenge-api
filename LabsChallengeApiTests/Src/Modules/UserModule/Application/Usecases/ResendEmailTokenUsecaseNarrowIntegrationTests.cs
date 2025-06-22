using LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue.Dtos;
using Moq;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Application.Usecases;

[TestClass]
public class ResendEmailTokenUsecaseNarrowIntegrationTests
{
    private Mock<IUserRepository> _mockUserRepository = null!;
    private Mock<IQueueService> _mockQueueService = null!;
    private ResendEmailTokenUsecase _resendEmailTokenUsecase = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockQueueService = new Mock<IQueueService>();
        _resendEmailTokenUsecase = new ResendEmailTokenUsecase(_mockUserRepository.Object, _mockQueueService.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ShouldCallUpdateEmailTokenRequestedAtMethodAndSendMessageForQueue_WhenTokenRequestedIsOlderThan5Minutes()
    {
        //GIVEN
        var email = "john.doe@hotmail.com";
        var user = User.Restore(
            id: 1,
            name: "John Doe",
            email: "john.doe@hotmail.com",
            passwordHash: "password-hash",
            isEmailConfirmed: false,
            emailConfirmationToken: "token-email",
            emailTokenRequestedAt: DateTime.UtcNow.AddMinutes(-5)
        );
        _mockUserRepository.Setup(repository => repository.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

        //WHEN
        await _resendEmailTokenUsecase.ExecuteAsync(email);
        //THEN
        _mockUserRepository.Verify(repository => repository.UpdateEmailTokenRequestedAtAsync(It.IsAny<User>()), Times.Once);
        _mockQueueService.Verify(queue => queue.SendMessage(It.IsAny<QueueMessageDto>()), Times.Once);
    }

    [TestMethod]
    public async Task ExecuteAsync_ShouldThrowValidationException_WhenTokenRequestedIsLessThan5Minutes()
    {
        //GIVEN
        var email = "john.doe@hotmail.com";
        var user = User.Restore(
            id: 1,
            name: "John Doe",
            email: "john.doe@hotmail.com",
            passwordHash: "password-hash",
            isEmailConfirmed: false,
            emailConfirmationToken: "token-email",
            emailTokenRequestedAt: DateTime.UtcNow
        );
        _mockUserRepository.Setup(repository => repository.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        //WHEN //THEN
        await Assert.ThrowsExceptionAsync<ValidationException>(() =>
         _resendEmailTokenUsecase.ExecuteAsync(email));
        _mockUserRepository.Verify(repository => repository.UpdateEmailTokenRequestedAtAsync(It.IsAny<User>()), Times.Never);
        _mockQueueService.Verify(queue => queue.SendMessage(It.IsAny<QueueMessageDto>()), Times.Never);
    }
}
