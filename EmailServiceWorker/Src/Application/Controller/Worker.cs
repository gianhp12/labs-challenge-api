using System.Reflection;
using System.Text;
using EmailServiceWorker.Src.Application.Usecases;
using EmailServiceWorker.Src.Infrastructure.Logger;
using EmailServiceWorker.Src.Infrastructure.Queue;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailServiceWorker.Src.Application.Controller;

public class Worker : BackgroundService
{
    private readonly ILoggerService _loggerService;
    private readonly IQueueService _queueService;
    private readonly ISendEmailUsecase _sendEmailUsecase;
    private bool IsRunning { get; set; } = false;

    public Worker(
        IQueueService queueService,
        ISendEmailUsecase sendEmailUsecase,
        ILoggerService loggerService)
    {
        _queueService = queueService;
        _sendEmailUsecase = sendEmailUsecase;
        _loggerService = loggerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IsRunning = true;
        _loggerService.LogInformation("Worker running");
        await _queueService.AddConsumerListener(
            queueName: "labs-challenge-api-email.confirmation",
            onListener: (channel, args) => Wrapper(channel, args, SendConfirmationTokenEmail)
        );
        while (IsRunning && !stoppingToken.IsCancellationRequested)
            await Task.Delay(1000, stoppingToken);
        Environment.Exit(1);
    }

    private async Task Wrapper(IChannel channel, BasicDeliverEventArgs args, Func<IChannel, BasicDeliverEventArgs, Task> callback)
    {
        Dictionary<string, object?> request = null!;
        string? exceptionMessage = null;
        string? exceptionStackTrace = null;
        try
        {
            if (!IsRunning)
            {
                _loggerService.LogInformation("Worker Stopped");
                await channel.AbortAsync();
                return;
            }
            await callback(channel, args);
        }
        catch (Exception ex)
        {
            exceptionMessage = ex.Message;
            exceptionStackTrace = ex.StackTrace?.ToString();
            IsRunning = false;
        }
        finally
        {
            DateTime? conclusionDate = DateTime.Now;
            request.Add("ExceptionMessage", exceptionMessage);
            request.Add("ExceptionStackTrace", exceptionStackTrace);
            request.Add("ConclusionDate", conclusionDate);
            request.Add("MethodInfo", callback.GetMethodInfo().Name);
            _loggerService.LogInformation("Worker execution failed unexpectedly", request);
        }
    }

    private static JToken GetJsonFromMessageBytes(ReadOnlyMemory<byte> message)
    {
        var bodyBytes = message.ToArray();
        var messageString = Encoding.UTF8.GetString(bodyBytes);
        var messageParsed = JToken.Parse(messageString);
        return messageParsed;
    }

    public async Task SendConfirmationTokenEmail(IChannel channel, BasicDeliverEventArgs args)
    {
        var json = GetJsonFromMessageBytes(args.Body);
        _loggerService.LogInformation("Received message from queue");
        var email = json["Email"]!.ToObject<string>()!;
        var token = json["Token"]!.ToObject<string>()!;
        var subject = "Token de confirmação de cadastro";
        var body = $"O seu token de confirmação é: {token}";
        await _sendEmailUsecase.ExecuteAsync(email, subject, body);
        await channel.BasicAckAsync(deliveryTag: args.DeliveryTag, multiple: false);
        _loggerService.LogInformation("Message processed successfully");
    }
}
