using LabsChallengeApi.Shared.Infrastructure.Logger.Factories;
using LabsChallengeApi.Src.Modules.AuthModule.Infrasctructure.DI;
using LabsChallengeApi.Src.Shared.Application.Configuration;
using LabsChallengeApi.Src.Shared.Application.Middlewares;
using LabsChallengeApi.Src.Shared.Infrastructure.DI;
using LabsChallengeApi.Src.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationJwt(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddHealthChecks();
Serilog.Debugging.SelfLog.Enable(msg => Console.Error.WriteLine("SERILOG SELFLOG: " + msg));
SerilogLoggerFactory.ConfigureSerilog(builder.Configuration, builder.Environment);
builder.Host.UseSerilog();
builder.Services.AddSharedModuleServices();
builder.Services.AddAuthModuleControlServices();
builder.Services.AddControllers();

var app = builder.Build();

await app.InitializeQueue();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
var env = app.Environment;

app.UseSwaggerConfiguration(provider, env);
app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapControllers();
app.Run();
