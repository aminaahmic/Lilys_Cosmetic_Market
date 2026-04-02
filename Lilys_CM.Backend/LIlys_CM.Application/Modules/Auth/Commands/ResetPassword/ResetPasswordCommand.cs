namespace Lilys_CM.Application.Modules.Auth.Commands.ResetPassword;

public sealed class ResetPasswordCommand : IRequest<ResetPasswordCommandDto>
{
    public string Token { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;
}