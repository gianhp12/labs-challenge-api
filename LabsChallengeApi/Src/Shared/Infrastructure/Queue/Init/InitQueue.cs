namespace LabsChallengeApi.Src.Shared.Infrastructure.Queue.Init;

public class InitQueue
{
    private readonly IQueueService _queueService;

    public InitQueue(IQueueService queueService)
    {
        _queueService = queueService;
    }

    public async Task InitializeAsync()
    {
        await _queueService.CreateConnection();
        await _queueService.CreateChannel();
    }
}
