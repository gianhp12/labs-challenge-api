using MailKit.Net.Smtp;
using MimeKit;

namespace EmailServiceWorker.Src.Infrastructure.Gateways.Adapters;

public class SmtpEmailSender : IEmailSender
{
    private readonly IConfigurationSection EmailSettings;

    public SmtpEmailSender(IConfiguration configuration)
    {
        EmailSettings = configuration.GetSection("EmailSettings");
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var server = EmailSettings.GetValue<string>("Server")!;
        var port = EmailSettings.GetValue<int>("Port");
        var username = EmailSettings.GetValue<string>("Username")!;
        var password = EmailSettings.GetValue<string>("Password")!;
        var from = EmailSettings.GetValue<string>("From")!;
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(server, port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(username, password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}

