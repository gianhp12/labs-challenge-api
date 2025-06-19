using LabsChallengeApi.Src.Modules.UserModule.Domain.Entities;

namespace LabsChallengeApi.Src.Modules.UserModule.Domain.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User> GetByEmailAsync(string email);
}
