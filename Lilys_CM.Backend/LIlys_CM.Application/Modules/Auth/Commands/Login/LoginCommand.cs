namespace Lilys_CM.Application.Modules.Auth.Commands.Login;

/// <summary>
/// Command for user login and issuing an access/refresh token pair.
/// </summary>
public sealed class LoginCommand : IRequest<LoginCommandDto>
{
    /// <summary>
    /// User's email.
    /// </summary>
    public string Email { get; init; } = default!;

    /// <summary>
    /// User's password.
    /// </summary>
    public string Password { get; init; } = default!;

    /// <summary>
    /// (Optional) Client "fingerprint" / device identifier for device-bound refresh tokens.
    /// </summary>
    public string? Fingerprint { get; init; }
}
