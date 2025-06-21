using EmailServiceWorker.Src.Infrastructure.Queue.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailServiceWorker.Src.Infrastructure.Queue;

public interface IQueueService
{
    public Task CreateConnection();
    public Task CreateChannel();
    public Task SendMessage(QueueMessageDto dto);
    public Task AddConsumerListener(string queueName, Func<IChannel, BasicDeliverEventArgs, Task> onListener, ushort consumeMessagesLimit = 0);
}
