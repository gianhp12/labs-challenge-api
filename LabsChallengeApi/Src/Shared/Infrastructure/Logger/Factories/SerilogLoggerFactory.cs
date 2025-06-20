using LabsChallengeApi.Src.Shared.Infrastructure.Logger.Factories;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace LabsChallengeApi.Shared.Infrastructure.Logger.Factories;

public static class SerilogLoggerFactory
{
    public static void ConfigureSerilog(IConfiguration configuration, IHostEnvironment environment)
    {
        var logsPath = Path.Combine(AppContext.BaseDirectory, "logs");
        Directory.CreateDirectory(logsPath);
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]!))
            {
                IndexFormat = $"logs-{environment.ApplicationName?.ToLower().Replace(".", "-")}-{environment.EnvironmentName?.ToLower().Replace(".", "-")}-default",
                NumberOfShards = 1,
                NumberOfReplicas = 1,
                BufferBaseFilename = Path.Combine(logsPath, "logs-buffer"),
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog
                                  | EmitEventFailureHandling.WriteToFailureSink
                                  | EmitEventFailureHandling.RaiseCallback,
                FailureSink = new SimpleFileSink(Path.Combine(logsPath, "logs-failed.txt")),
            })
            .CreateLogger();
    }
}
