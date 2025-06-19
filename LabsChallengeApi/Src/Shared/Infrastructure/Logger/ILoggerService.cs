namespace LabsChallengeApi.Src.Shared.Infrastructure.Logger;

public interface ILoggerService
{
    void LogInformation(string message, object? data = null);
    void LogError(string message, Exception? exception = null, object? data = null);
}
