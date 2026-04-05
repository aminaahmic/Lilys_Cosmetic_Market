using System.ComponentModel.DataAnnotations;

namespace Lilys_CM.Shared.Options;

public sealed class MailOptions
{
    public const string SectionName = "Mail";

    [Required]
    public string Host { get; init; } = default!;

    [Range(1, 65535)]
    public int Port { get; init; }

    public bool UseSsl { get; init; }

    [Required]
    public string Username { get; init; } = default!;

    [Required]
    public string Password { get; init; } = default!;

    [Required]
    public string FromEmail { get; init; } = default!;

    [Required]
    public string FromName { get; init; } = default!;
}