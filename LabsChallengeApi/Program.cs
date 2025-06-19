using LabsChallengeApi.Shared.Infrastructure.Logger.Factories;
using LabsChallengeApi.Src.Shared.Application.Middlewares;
using LabsChallengeApi.Src.Shared.Infrastructure.DI;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Serilog.Debugging.SelfLog.Enable(msg => Console.Error.WriteLine("SERILOG SELFLOG: " + msg));
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
