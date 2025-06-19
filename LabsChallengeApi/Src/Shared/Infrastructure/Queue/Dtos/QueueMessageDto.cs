using RabbitMQ.Client;

namespace LabsChallengeApi.Src.Shared.Infrastructure.Queue.Dtos;

public class QueueMessageDto
{
    public required object Message { get; init; }
    public required string Exchange { get; init; }
    public string RoutingKey { get; init; } = string.Empty;
    public BasicProperties? Props { get; init; }
}
