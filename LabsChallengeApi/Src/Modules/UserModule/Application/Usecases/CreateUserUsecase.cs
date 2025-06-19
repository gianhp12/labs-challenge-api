using LabsChallengeApi.Src.Modules.UserModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.UserModule.Domain.Entities;
using LabsChallengeApi.Src.Modules.UserModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Infrastructure.Hasher;

namespace LabsChallengeApi.Src.Modules.UserModule.Application.Usecases;

public class CreateUserUsecase : ICreateUserUsecase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    public CreateUserUsecase(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task ExecuteAsync(CreateUserInputDto dto)
    {
        var registeredUser = User.Create(name: dto.Username, email: dto.Email, password: dto.Password);
        var encryptPassword = _passwordHasher.Hash(dto.Password);
        registeredUser.SetPasswordHash(encryptPassword);
        await _userRepository.CreateAsync(registeredUser);
    }
}
