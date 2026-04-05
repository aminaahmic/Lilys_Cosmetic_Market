using System.Net;
using System.Net.Mail;
using Lilys_CM.Application.Abstractions.Email;
using Lilys_CM.Shared.Options;
using Microsoft.Extensions.Options;

namespace Lilys_CM.Infrastructure.Email;

public sealed class MailtrapEmailSender(IOptions<MailOptions> mailOptions) : IEmailSender
{
    private readonly MailOptions _mailOptions = mailOptions.Value;

    public async Task SendAsync(string to, string subject, string htmlBody, CancellationToken ct = default)
    {
        using var message = new MailMessage
        {
            From = new MailAddress(_mailOptions.FromEmail, _mailOptions.FromName),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true
        };

        message.To.Add(to);

        using var client = new SmtpClient(_mailOptions.Host, _mailOptions.Port)
        {
            Credentials = new NetworkCredential(_mailOptions.Username, _mailOptions.Password),
            EnableSsl = _mailOptions.UseSsl
        };

        ct.ThrowIfCancellationRequested();
        await client.SendMailAsync(message);
    }
}