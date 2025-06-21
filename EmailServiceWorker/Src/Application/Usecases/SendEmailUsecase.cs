using EmailServiceWorker.Src.Infrastructure.Gateways;

namespace EmailServiceWorker.Src.Application.Usecases;

public class SendEmailUsecase : ISendEmailUsecase
{
    private readonly IEmailSender _emailSender;

    public SendEmailUsecase(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task ExecuteAsync(string email, string subject, string body)
    {
        await _emailSender.SendAsync(email, subject, body);
    }
}
