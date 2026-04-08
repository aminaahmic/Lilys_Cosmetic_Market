namespace Lilys_CM.Infrastructure.Email;

public class PostmarkOptions
{
    public string ServerToken { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
}