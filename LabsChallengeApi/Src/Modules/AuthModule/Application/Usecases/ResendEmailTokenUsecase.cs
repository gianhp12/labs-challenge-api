using LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue.Dtos;

namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;

public class ResendEmailTokenUsecase : IResendEmailTokenUsecase
{
    private readonly IUserRepository _userRepository;
    private readonly IQueueService _queueService;

    public ResendEmailTokenUsecase(IUserRepository userRepository, IQueueService queueService)
    {
        _userRepository = userRepository;
        _queueService = queueService;
    }

    public async Task ExecuteAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        var now = DateTime.UtcNow;
        if (user.EmailTokenRequestedAt.AddMinutes(5) > now)
        {
            throw new ValidationException("É necessário aguardar 5 minutos para solicitar um novo token de confirmação.");
        }
        user.SetEmailTokenRequestedAt();
        await _userRepository.UpdateEmailTokenRequestedAtAsync(user);
        await _queueService.SendMessage(new QueueMessageDto
        {
            Exchange = "labs-challenge-api.exchange",
            RoutingKey = "labs-challenge-api-email.confirmation",
            Message = new
            {
                Email = user.Email.Value,
                Name = user.Name,
                Token = user.EmailConfirmationToken
            }
        });
    }
}
