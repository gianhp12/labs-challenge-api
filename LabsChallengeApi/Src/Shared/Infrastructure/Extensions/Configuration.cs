namespace LabsChallengeApi.Src.Shared.Infrastructure.Extensions;

public static class Configuration
{
    public static string GetRequiredConnectionString(this IConfiguration configuration, string key)
    {
        var connectionString = configuration.GetConnectionString(key);
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException($"Connection string '{key}' is not set in the configuration.");
        return connectionString!;
    }

    public static IConfigurationSection GetSettingsSection(this IConfiguration configuration, string section)
    {
        var settings = configuration.GetSection(section);
        if (!settings.Exists())
            throw new InvalidOperationException($"Section '{section}' not found in configuration.");
        return settings;
    }
}
