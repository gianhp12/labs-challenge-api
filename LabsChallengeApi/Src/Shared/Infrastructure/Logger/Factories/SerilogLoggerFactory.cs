using LabsChallengeApi.Src.Shared.Infrastructure.Logger.Factories;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace LabsChallengeApi.Shared.Infrastructure.Logger.Factories;

public static class SerilogLoggerFactory
{
    public static void ConfigureSerilog(IConfiguration configuration, IHostEnvironment environment)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]!))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                IndexFormat = $"logs-{environment.ApplicationName?.ToLower().Replace(".", "-")}-{environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                NumberOfShards = 1,
                NumberOfReplicas = 1,
                BufferBaseFilename = "./logs-buffer",
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog
                                  | EmitEventFailureHandling.WriteToFailureSink
                                  | EmitEventFailureHandling.RaiseCallback,
                FailureSink = new SimpleFileSink("logs-failed.txt"),
            })
            .CreateLogger();
    }
}
