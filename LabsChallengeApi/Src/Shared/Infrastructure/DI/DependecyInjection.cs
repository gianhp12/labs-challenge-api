using LabsChallengeApi.Src.Shared.Application.Middlewares;
using LabsChallengeApi.Src.Shared.Infrastructure.Database;
using LabsChallengeApi.Src.Shared.Infrastructure.Database.Factories;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Hasher;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Hasher.Adapters;
using LabsChallengeApi.Src.Shared.Infrastructure.Logger;
using LabsChallengeApi.Src.Shared.Infrastructure.Logger.Adapters;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue.Adapters;
using LabsChallengeApi.Src.Shared.Infrastructure.Queue.Init;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Token;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Token.Adapters;

namespace LabsChallengeApi.Src.Shared.Infrastructure.DI;

public static class DependecyInjection
{
    public static IServiceCollection AddSharedModuleServices(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        services.AddSingleton<InitQueue>();
        services.AddSingleton<IQueueService, RabbitMqAdapter>();
        services.AddSingleton<ISqlConnectionFactory, SqlServerFactory>();
        services.AddSingleton<ILoggerService, SerilogLoggerAdapter>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        services.AddSingleton<ITokenService, JwtTokenService>();
        services.AddTransient<RequestLoggingMiddleware>();
        return services;
    }
}
