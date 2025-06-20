namespace LabsChallengeApi.Src.Shared.Infrastructure.Security.Token.Dtos;

public class TokenDto
{
    public string AccessToken;
    public int ExpiresIn;

    public TokenDto(string accessToken, int expiresIn)
    {
        AccessToken = accessToken;
        ExpiresIn = expiresIn;
    }
}
