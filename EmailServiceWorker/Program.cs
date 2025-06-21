using EmailServiceWorker.Src.Application.Controller;
using EmailServiceWorker.Src.Infrastructure.DI;
using EmailServiceWorker.Src.Infrastructure.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Environment.ApplicationName = "EmailServiceWorker";
builder.Services.AddHostedService<Worker>();
builder.Services.AddModuleServices();

var host = builder.Build();

await host.InitializeQueueAsync();

host.Run();
