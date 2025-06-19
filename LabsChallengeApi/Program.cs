using LabsChallengeApi.Shared.Application.Middlewares;
using LabsChallengeApi.Shared.Infrastructure.DI;
using LabsChallengeApi.Shared.Infrastructure.Logger.Factories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
SerilogLoggerFactory.ConfigureSerilog(builder.Configuration, builder.Environment);
builder.Host.UseSerilog();
builder.Services.AddControlServices();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.Run();
