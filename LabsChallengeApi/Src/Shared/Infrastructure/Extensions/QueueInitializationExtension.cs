using LabsChallengeApi.Src.Shared.Infrastructure.Queue.Init;

namespace LabsChallengeApi.Src.Shared.Infrastructure.Extensions;

public static class QueueInitializationExtension
{
    public static async Task InitializeQueue(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var initQueue = scope.ServiceProvider.GetRequiredService<InitQueue>();
        await initQueue.InitializeAsync();
    }
}
