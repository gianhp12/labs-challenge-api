using System.Text;
using RabbitMQ.Client;

namespace EmailServiceWorkerTests.Src.Infrastructure.Helpers;

public class RabbitMqTestHelper
{
    private readonly ConnectionFactory _factory;

    public RabbitMqTestHelper(string host, int port, string username, string password, string vhost)
    {
        _factory = new ConnectionFactory
        {
            HostName = host,
            Port = port,
            UserName = username,
            Password = password,
            VirtualHost = vhost
        };
    }

    public async Task PurgeQueueAsync(string queueName)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.QueuePurgeAsync(queueName);
    }

    public async Task<string?> GetSingleMessageAsync(string queueName)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        var result = await channel.BasicGetAsync(queue: queueName, autoAck: true);
        if (result == null) return null;
        return Encoding.UTF8.GetString(result.Body.ToArray());
    }
}
