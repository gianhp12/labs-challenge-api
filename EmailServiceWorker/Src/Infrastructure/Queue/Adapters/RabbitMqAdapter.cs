using System.Text;
using EmailServiceWorker.Src.Infrastructure.Extensions;
using EmailServiceWorker.Src.Infrastructure.Queue.Dtos;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailServiceWorker.Src.Infrastructure.Queue.Adapters;

public class RabbitMqAdapter : IQueueService
{
    private IConnection Connection { get; set; } = null!;
    private IChannel Channel { get; set; } = null!;
    private readonly IConfigurationSection QueueSettings;

    public RabbitMqAdapter(IConfiguration configuration)
    {
        QueueSettings = configuration.GetSettingsSection("QueueService");
        var props = QueueSettings.GetChildren();
        if (!props.Any(x => x.Key == "Host")) throw new Exception($"Key (QueueService:host) is not set in the configuration.");
        if (!props.Any(x => x.Key == "Port")) throw new Exception($"Key (QueueService:Port) is not set in the configuration.");
        if (!props.Any(x => x.Key == "Username")) throw new Exception($"Key (QueueService:Username) is not set in the configuration.");
        if (!props.Any(x => x.Key == "Password")) throw new Exception($"Key (QueueService:Password) is not set in the configuration.");
        if (!props.Any(x => x.Key == "VHost")) throw new Exception($"Key (QueueService:VHost) is not set in the configuration.");
    }

    public async Task CreateConnection()
    {
        var host = QueueSettings.GetValue<string>("Host")!;
        var port = QueueSettings.GetValue<int>("Port");
        var userName = QueueSettings.GetValue<string>("Username")!;
        var password = QueueSettings.GetValue<string>("Password")!;
        var vHost = QueueSettings.GetValue<string>("VHost")!;
        var connFactory = new ConnectionFactory
        {
            HostName = host,
            VirtualHost = vHost,
            Port = port,
            UserName = userName,
            Password = password,
            AutomaticRecoveryEnabled = true,
            RequestedHeartbeat = TimeSpan.FromSeconds(60),
        };
        var connection = await connFactory.CreateConnectionAsync();
        Connection = connection;
    }

    public async Task CreateChannel() { Channel = await Connection.CreateChannelAsync(); }

    public async Task SendMessage(QueueMessageDto dto)
    {
        var message = dto.Message;
        if (message is not string) message = JsonConvert.SerializeObject(dto.Message);
        var body = Encoding.UTF8.GetBytes((string)message);
        var props = dto.Props ?? new BasicProperties();
        await Channel.BasicPublishAsync(exchange: dto.Exchange, routingKey: dto.RoutingKey, mandatory: true, basicProperties: props, body: body);
    }

    public async Task AddConsumerListener(string queueName, Func<IChannel, BasicDeliverEventArgs, Task> onListener, ushort consumeMessagesLimit = 0)
    {
        var consumerChannel = await Connection.CreateChannelAsync();
        await consumerChannel.QueueDeclarePassiveAsync(queue: queueName);
        if (consumeMessagesLimit > 0)
            await consumerChannel.BasicQosAsync(0, consumeMessagesLimit, false);
        var consumer = new AsyncEventingBasicConsumer(consumerChannel);
        consumer.ReceivedAsync += async (ch, ea) => await onListener(consumerChannel, ea);
        await consumerChannel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer);
    }
}
