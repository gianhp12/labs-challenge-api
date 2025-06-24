using Serilog;
using Serilog.Context;

namespace LabsChallengeApi.Src.Shared.Infrastructure.Logger.Adapters;

public class SerilogLoggerAdapter : ILoggerService
{
    public void LogError(string message, Exception? exception = null, object? data = null)
    {
        using (LogContext.PushProperty("Data", data ?? new { }))
        {
            Log.Error(exception, message);
        }
    }

    public void LogInformation(string message, object? data = null)
    {
        using (LogContext.PushProperty("Data", data ?? new { }))
        {
            Log.Information(message);
        }
    }
}

