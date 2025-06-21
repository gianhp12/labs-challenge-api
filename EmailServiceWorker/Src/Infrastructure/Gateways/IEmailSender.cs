namespace EmailServiceWorker.Src.Infrastructure.Gateways;

public interface IEmailSender
{
    Task SendAsync(string to, string subject, string body);
}
