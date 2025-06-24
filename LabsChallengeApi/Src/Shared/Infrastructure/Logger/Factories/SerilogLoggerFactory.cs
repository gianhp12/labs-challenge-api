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
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error)
        .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Error)
        .MinimumLevel.Override("LabsChallengeApi.Src.Shared.Application.Middlewares.RequestLoggingMiddleware", Serilog.Events.LogEventLevel.Information)
        .Enrich.WithEnvironmentName()
        .Enrich.WithMachineName()
        .Enrich.WithThreadId()
        .Enrich.WithProcessId()
        .Enrich.WithProperty("Application", environment.ApplicationName)
        .Enrich.WithProperty("Environment", environment.EnvironmentName)
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]!))
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
            IndexFormat = $"{environment.ApplicationName?.ToLower().Replace(".", "-")}-{environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy.MM}",
            NumberOfShards = 1,
            NumberOfReplicas = 1,
            FailureSink = new SimpleFileSinkFactory(Path.Combine(logsPath, "logs-failed.txt")),
            BufferBaseFilename = Path.Combine(logsPath, "logs-buffer"),
            EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                EmitEventFailureHandling.WriteToFailureSink |
                                EmitEventFailureHandling.RaiseCallback,
        })
        .WriteTo.File(
            Path.Combine(logsPath, "logs.txt"),
            rollingInterval: RollingInterval.Day)
        .CreateLogger();
}

}
