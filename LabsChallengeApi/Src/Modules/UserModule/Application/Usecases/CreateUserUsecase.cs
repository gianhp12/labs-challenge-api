using System.ComponentModel.DataAnnotations;
using LabsChallengeApi.Src.Modules.UserModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.UserModule.Domain.DAOs;
using LabsChallengeApi.Src.Modules.UserModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.UserModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Infrastructure.Hasher;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue.Dtos;

namespace LabsChallengeApi.Src.Modules.UserModule.Application.Usecases;

public class CreateUserUsecase : ICreateUserUsecase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDAO _userDAO;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IQueueService _queueService;

    public CreateUserUsecase(IUserRepository userRepository, IUserDAO userDAO, IPasswordHasher passwordHasher, IQueueService queueService)
    {
        _userRepository = userRepository;
        _userDAO = userDAO;
        _passwordHasher = passwordHasher;
        _queueService = queueService;
    }

    public async Task ExecuteAsync(CreateUserInputDto dto)
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
