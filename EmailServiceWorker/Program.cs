using EmailServiceWorker.Src.Application.Controller;
using EmailServiceWorker.Src.Infrastructure.DI;
using EmailServiceWorker.Src.Infrastructure.Extensions;
using EmailServiceWorker.Src.Infrastructure.Logger.Factories;
using Serilog;
using Serilog.Debugging;

SelfLog.Enable(msg => Console.Error.WriteLine("SERILOG SELFLOG: " + msg));

var builder = Host.CreateApplicationBuilder(args);

builder.Environment.ApplicationName = "EmailServiceWorker";
SerilogLoggerFactory.ConfigureSerilog(builder.Configuration, builder.Environment);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
builder.Services.AddHostedService<Worker>();
builder.Services.AddModuleServices();

var host = builder.Build();

await host.InitializeQueueAsync();
host.Run();
