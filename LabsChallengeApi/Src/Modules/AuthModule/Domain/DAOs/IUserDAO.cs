namespace LabsChallengeApi.Src.Modules.AuthModule.Domain.DAOs;

public interface IUserDAO
{
    Task<bool> ExistsByEmailAsync(string email);
}
