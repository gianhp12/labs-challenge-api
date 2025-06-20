using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.DAOs;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue.Dtos;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Hasher;

namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;

public class RegisterUserUsecase : IRegisterUserUsecase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDAO _userDAO;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IQueueService _queueService;

    public RegisterUserUsecase(IUserRepository userRepository, IUserDAO userDAO, IPasswordHasher passwordHasher, IQueueService queueService)
    {
        _userRepository = userRepository;
        _userDAO = userDAO;
        _passwordHasher = passwordHasher;
        _queueService = queueService;
    }

    public async Task ExecuteAsync(RegisterUserInputDto dto)
    {
        await EnsureEmailIsUnique(dto.Email);
        var user = User.Create(name: dto.Username, email: dto.Email, password: dto.Password);
        var encryptPassword = _passwordHasher.Hash(dto.Password);
        user.SetPasswordHash(encryptPassword);
        await _userRepository.CreateAsync(user);
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

    private async Task EnsureEmailIsUnique(string email)
    {
        if (await _userDAO.ExistsByEmailAsync(email))
        {
            throw new ValidationException("Já existe um usuário cadastrado com o email informado.");
        }
    }
}
