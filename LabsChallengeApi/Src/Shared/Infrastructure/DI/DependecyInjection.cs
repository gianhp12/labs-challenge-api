using LabsChallengeApi.Src.Shared.Application.Middlewares;
using LabsChallengeApi.Src.Shared.Infrastructure.Database;
using LabsChallengeApi.Src.Shared.Infrastructure.Database.Factories;
using LabsChallengeApi.Src.Shared.Infrastructure.Logger;
using LabsChallengeApi.Src.Shared.Infrastructure.Logger.Adapters;

namespace LabsChallengeApi.Src.Shared.Infrastructure.DI;

public static class DependecyInjection
{
    public static IServiceCollection AddControlServices(this IServiceCollection services)
    {
        services.AddSingleton<ISqlConnectionFactory, SqlServerFactory>();
        services.AddSingleton<ILoggerService, SerilogLoggerAdapter>();
        services.AddTransient<RequestLoggingMiddleware>();
        return services;
    }
}
