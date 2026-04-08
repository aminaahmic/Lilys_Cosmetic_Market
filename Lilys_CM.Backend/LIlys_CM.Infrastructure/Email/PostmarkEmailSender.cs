using Lilys_CM.Application.Abstractions.Email;
using Microsoft.Extensions.Options;
using PostmarkDotNet;

namespace Lilys_CM.Infrastructure.Email;

public sealed class PostmarkEmailSender(IOptions<PostmarkOptions> options) : IEmailSender
{
    private readonly PostmarkOptions _options = options.Value;

    public async Task SendAsync(string to, string subject, string htmlBody, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        var client = new PostmarkClient(_options.ServerToken);

        var message = new PostmarkMessage
        {
            To = to,
            From = $"{_options.FromName} <{_options.FromEmail}>",
            Subject = subject,
            HtmlBody = htmlBody
        };

        var response = await client.SendMessageAsync(message);

        if (response.Status != PostmarkStatus.Success)
        {
            throw new Exception($"Postmark error: {response.Message}");
        }
    }
}