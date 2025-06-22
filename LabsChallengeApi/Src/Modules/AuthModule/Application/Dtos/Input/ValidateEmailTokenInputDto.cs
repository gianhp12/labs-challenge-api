namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;

public class ValidateEmailTokenInputDto
{
    public required string Email { get; init; }
    public required string Token { get; init; }
}
