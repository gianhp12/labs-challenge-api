using LabsChallengeApi.Src.Modules.UserModule.Domain.Repositories;
using LabsChallengeApi.Src.Modules.UserModule.Infrastructure.Repositories;

namespace LabsChallengeApi.Src.Modules.UserModule.Infrastructure.DI;

public static class UserModuleDependecyInjection
{
    public static IServiceCollection AddUserModuleControlServices(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        return services;
    }
}
