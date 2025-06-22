namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Output;

public class AuthenticatedUserOutputDto
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string AccessToken { get; init; }
    public int ExpiresIn { get; init; }
    public bool IsEmailConfirmed { get; init; }

    public AuthenticatedUserOutputDto(string username,
    string email,
    string accessToken,
    int expiresIn,
    bool isEmailConfirmed)
    {
        Username = username;
        Email = email;
        AccessToken = accessToken;
        ExpiresIn = expiresIn;
        IsEmailConfirmed = isEmailConfirmed;
    }
}
