using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace LabsChallengeApi.Src.Shared.Infrastructure.Logger.Factories;

public class SimpleFileSink : ILogEventSink
{
    private readonly StreamWriter _writer;
    private readonly JsonFormatter _jsonFormatter;

    public SimpleFileSink(string path)
    {
        _writer = new StreamWriter(File.Open(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
        {
            AutoFlush = true
        };
        _jsonFormatter = new JsonFormatter();
    }

    public void Emit(LogEvent logEvent)
    {
        _jsonFormatter.Format(logEvent, _writer);
        _writer.WriteLine();
    }
}
