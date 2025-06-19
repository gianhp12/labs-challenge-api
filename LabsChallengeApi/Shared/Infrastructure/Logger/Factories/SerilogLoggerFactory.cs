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
                IndexFormat = $"logs-{environment.ApplicationName?.ToLower().Replace(".", "-")}-{environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            })
            .CreateLogger();
    }
}
