using System.Text;
using EmailServiceWorker.Src.Application.Controller;
using EmailServiceWorker.Src.Application.Usecases;
using EmailServiceWorker.Src.Infrastructure.Logger;
using EmailServiceWorker.Src.Infrastructure.Queue;
using Moq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace EmailServiceWorkerTests.Src.Application.Controller;

[TestClass]
public class WorkerNarrowIntegrationTests
{
    private Mock<IQueueService> _mockQueue = null!;
    private Mock<ILoggerService> _mockLogger = null!;
    private Mock<ISendEmailUsecase> _mockSendEmailUsecase = null!;
    private Worker _worker = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockQueue = new Mock<IQueueService>();
        _mockLogger = new Mock<ILoggerService>();
        _mockSendEmailUsecase = new Mock<ISendEmailUsecase>();

        _worker = new Worker(
            loggerService: _mockLogger.Object,
            queueService: _mockQueue.Object,
            sendEmailUsecase: _mockSendEmailUsecase.Object
        );
    }

    [TestMethod]
    public async Task SendConfirmationTokenEmail_Should_Process_Message_And_Send_Email()
    {
        //GIVEN
        var mockChannel = new Mock<IChannel>();
        var email = "test@example.com";
        var token = "123456";
        var json = new
        {
            Email = email,
            Token = token
        };
        var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json));
        var args = new BasicDeliverEventArgs(
            consumerTag: "tag",
            deliveryTag: 1,
            redelivered: false,
            exchange: "exchange",
            routingKey: "routingKey",
            properties: new ReadOnlyBasicProperties(span: new ReadOnlySpan<byte>()),
            body: new ReadOnlyMemory<byte>(messageBody)
        );
        //WHEN
        await _worker.SendConfirmationTokenEmail(mockChannel.Object, args);
        //THEN
        _mockSendEmailUsecase.Verify(x =>
            x.ExecuteAsync(
                email,
                "Token de confirmação de cadastro",
                $"O seu token de confirmação é: {token}"), Times.Once);
        mockChannel.Verify(x => x.BasicAckAsync(args.DeliveryTag, false, It.IsAny<CancellationToken>()), Times.Once);
    }
}

