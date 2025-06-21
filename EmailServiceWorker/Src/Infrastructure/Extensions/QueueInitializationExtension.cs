using EmailServiceWorker.Src.Infrastructure.Queue.Init;

namespace EmailServiceWorker.Src.Infrastructure.Extensions;

public static class QueueInitializationExtension
{
    public static async Task InitializeQueueAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var initQueue = scope.ServiceProvider.GetRequiredService<InitQueue>();
        await initQueue.InitializeAsync();
    }
}
