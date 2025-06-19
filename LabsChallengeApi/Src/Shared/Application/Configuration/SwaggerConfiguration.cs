using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace LabsChallengeApi.Src.Shared.Application.Configuration;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var provider = services.BuildServiceProvider()
                                   .GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = $"Labs Challenge API {description.ApiVersion}",
                    Version = description.GroupName,
                    Description = "Documentação da API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Gian Henrique Pereira",
                        Email = "gian_htc@hotmail.com",
                    }
                });
            }
        });
        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(
         this IApplicationBuilder app,
         IApiVersionDescriptionProvider provider,
         IWebHostEnvironment env)
    {
        app.UseSwagger();
        if (env.IsDevelopment())
        {
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
                options.RoutePrefix = string.Empty;
            });
        }
        return app;
    }
}


