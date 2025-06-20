namespace LabsChallengeApi.Src.Shared.Infrastructure.Security.Hasher;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}
