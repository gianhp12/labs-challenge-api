using LabsChallengeApi.Src.Shared.Infrastructure.Security.Token.Dtos;

namespace LabsChallengeApi.Src.Shared.Infrastructure.Security.Token;

public interface ITokenService
{
    TokenDto GenerateToken(string email, string name);
}
