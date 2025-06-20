using LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.DAOs;
using LabsChallengeApi.Src.Modules.AuthModule.Domain.Repositories;
using LabsChallengeApi.Src.Modules.AuthModule.Infrastructure.DAOs;
using LabsChallengeApi.Src.Modules.AuthModule.Infrastructure.Repositories;

namespace LabsChallengeApi.Src.Modules.AuthModule.Infrasctructure.DI;

public static class AuthModuleDependencyInjection
{
    public static IServiceCollection AddAuthModuleControlServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticateUserUsecase, AuthenticateUserUsecase>();
        services.AddScoped<IRegisterUserUsecase, RegisterUserUsecase>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserDAO, UserDAO>();
        return services;
    }
}
