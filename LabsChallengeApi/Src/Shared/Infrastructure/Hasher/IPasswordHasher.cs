namespace LabsChallengeApi.Src.Shared.Infrastructure.Hasher;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}
