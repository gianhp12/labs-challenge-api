using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Output;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Hasher;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Token;

namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;

public class AuthenticateUserUsecase : IAuthenticateUserUsecase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthenticateUserUsecase(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthenticatedUserOutputDto> ExecuteAsync(AuthenticateInputDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        var isValidPassword = _passwordHasher.Verify(dto.Password, user.PasswordHash!);
        if (!isValidPassword)
        {
            throw new ValidationException("Usuário ou seha inválidos, verifique e tente novamente");
        }
        if (!user.IsEmailConfirmed)
        {
            throw new ValidationException("O e-mail ainda não foi confirmado. Verifique sua caixa de entrada e confirme o cadastro utilizando o token enviado.");
        }
        var token = _tokenService.GenerateToken(user.Email.Value, user.Name.Value);
        return new AuthenticatedUserOutputDto(
            username: user.Name.Value,
            email: user.Email.Value,
            accessToken: token.AccessToken,
            expiresIn: token.ExpiresIn
        );
    }
}
