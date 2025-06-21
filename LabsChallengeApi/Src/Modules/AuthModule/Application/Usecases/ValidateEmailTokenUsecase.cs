using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;

public class ValidateEmailTokenUsecase : IValidateEmailTokenUsecase
{
    private readonly IUserRepository _userRepository;

    public ValidateEmailTokenUsecase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(ValidateEmailTokenInputDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (!user.EmailConfirmationToken.Equals(dto.Token))
        {
            throw new ValidationException("Token inválido");
        }
        user.SetEmailConfirmed();
        await _userRepository.UpdateEmailConfirmedAsync(user);
    }
}
