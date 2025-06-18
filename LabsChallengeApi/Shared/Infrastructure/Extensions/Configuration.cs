namespace LabsChallengeApi.Shared.Infrastructure.Extensions;

public static class Configuration
{
    public static string GetConnectionStringByEnvironment(this IConfiguration configuration, string key)
    {
        var environment = configuration.GetEnvironmentString();
        if (string.IsNullOrEmpty(environment)) throw new Exception($"Environment variable {environment} is not set.");
        var connectionString = configuration.GetConnectionString($"{key}_{environment}");
        if (string.IsNullOrEmpty(connectionString)) throw new Exception($"Connection string {key}_{environment} is not set in the configuration.");
        return connectionString;
    }

    public static IConfigurationSection GetEnvironmentSettings(this IConfiguration configuration, string section)
    {
        var environment = configuration.GetEnvironmentString();
        var settings = configuration.GetSection($"{section}:{environment}");
        if (!settings.Exists())
            throw new Exception("Error: environment not detected");
        return settings;
    }

    public static string GetEnvironmentString(this IConfiguration configuration)
    {
        var environmentKey = configuration.GetValue<string>("EnvironmentKey")!;
        var environment = Environment.GetEnvironmentVariable(environmentKey);
        if (string.IsNullOrEmpty(environment)) throw new Exception("Error: environment not detected");
        return environment;
    }
}
