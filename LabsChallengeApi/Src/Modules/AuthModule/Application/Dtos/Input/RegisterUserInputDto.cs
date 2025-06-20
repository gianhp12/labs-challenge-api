namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;

public class RegisterUserInputDto
{
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}
