namespace Lilys_CM.Application.Modules.Auth.Commands.Login;


public sealed class LoginCommandDto
{

    public string AccessToken { get; set; } = default!;


    public string RefreshToken { get; set; } = default!;

    public DateTime ExpiresAtUtc { get; set; }
}
