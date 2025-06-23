namespace LabsChallengeApi.Src.Shared.Application.Configuration;

public static class CorsConfiguration
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("LabsChallengeFront", policy =>
            {
                policy.WithOrigins(
                        "http://labs-challenge-front",
                        "http://localhost:8080"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        return services;
    }
}
