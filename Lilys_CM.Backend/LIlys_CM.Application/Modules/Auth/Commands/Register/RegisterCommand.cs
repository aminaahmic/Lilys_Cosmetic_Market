namespace Lilys_CM.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommand : IRequest<RegisterCommandDto>
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;
}