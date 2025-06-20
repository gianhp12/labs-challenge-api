namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;

public class AuthenticateInputDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
