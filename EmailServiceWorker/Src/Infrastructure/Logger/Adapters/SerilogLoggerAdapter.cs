using Serilog;

namespace EmailServiceWorker.Src.Infrastructure.Logger.Adapters;

public class SerilogLoggerAdapter : ILoggerService
{
    public void LogError(string message, Exception? exception = null, object? data = null)
    {
        Log.Error(exception, "{Message} {@Data}", message, data);
    }

    public void LogInformation(string message, object? data = null)
    {
        Log.Information("{Message} {@Data}", message, data);
    }
}
