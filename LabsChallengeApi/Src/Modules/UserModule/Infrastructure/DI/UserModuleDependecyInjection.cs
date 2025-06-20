using LabsChallengeApi.Src.Modules.UserModule.Application.Usecases;
using LabsChallengeApi.Src.Modules.UserModule.Domain.DAOs;
using LabsChallengeApi.Src.Modules.UserModule.Domain.Repositories;
using LabsChallengeApi.Src.Modules.UserModule.Infrastructure.DAOs;
using LabsChallengeApi.Src.Modules.UserModule.Infrastructure.Repositories;

namespace LabsChallengeApi.Src.Modules.UserModule.Infrastructure.DI;

public static class UserModuleDependecyInjection
{
    public static IServiceCollection AddUserModuleControlServices(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserDAO, UserDAO>();
        services.AddTransient<ICreateUserUsecase, CreateUserUsecase>();
        return services;
    }
}
