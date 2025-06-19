namespace LabsChallengeApi.Src.Modules.UserModule.Application.Dtos.Input;

public class CreateUserInputDto
{
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}
