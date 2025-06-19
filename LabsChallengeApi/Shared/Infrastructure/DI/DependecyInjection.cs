using LabsChallengeApi.Shared.Application.Middlewares;
using LabsChallengeApi.Shared.Infrastructure.Database;
using LabsChallengeApi.Shared.Infrastructure.Database.Factories;
using LabsChallengeApi.Shared.Infrastructure.Logger;
using LabsChallengeApi.Shared.Infrastructure.Logger.Adapters;

namespace LabsChallengeApi.Shared.Infrastructure.DI;

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
