using LabsChallengeApi.Src.Modules.AuthModule.Domain.Entities;

namespace LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User> GetByEmailAsync(string email);
}
