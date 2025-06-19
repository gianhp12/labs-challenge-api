namespace LabsChallengeApi.Src.Modules.UserModule.Domain.DAOs;

public interface IUserDAO
{
    Task<bool> ExistsByEmailAsync(string email);
}
