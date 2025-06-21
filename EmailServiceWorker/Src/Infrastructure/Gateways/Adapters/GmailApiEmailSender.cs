
namespace EmailServiceWorker.Src.Infrastructure.Gateways.Adapters;

public class GmailApiEmailSender : IEmailSender
{
    public Task SendAsync(string to, string subject, string body, bool isHtml = true)
    {
        throw new NotImplementedException();
    }
}
