using EmailServiceWorker.Src.Application.Usecases;
using EmailServiceWorker.Src.Infrastructure.Gateways;
using EmailServiceWorker.Src.Infrastructure.Gateways.Adapters;
using EmailServiceWorker.Src.Infrastructure.Logger;
using EmailServiceWorker.Src.Infrastructure.Logger.Adapters;
using EmailServiceWorker.Src.Infrastructure.Queue;
using EmailServiceWorker.Src.Infrastructure.Queue.Adapters;
using EmailServiceWorker.Src.Infrastructure.Queue.Init;

namespace EmailServiceWorker.Src.Infrastructure.DI;

public static class DependecyInjection
{
    public static IServiceCollection AddModuleServices(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, SerilogLoggerAdapter>();
        services.AddSingleton<IQueueService, RabbitMqAdapter>();
        services.AddSingleton<InitQueue>();
        services.AddSingleton<IEmailSender, GmailApiEmailSender>();
        services.AddTransient<ISendConfirmationTokenEmailUsecase, SendConfirmationTokenEmailUsecase>();
        return services;
    }
}
