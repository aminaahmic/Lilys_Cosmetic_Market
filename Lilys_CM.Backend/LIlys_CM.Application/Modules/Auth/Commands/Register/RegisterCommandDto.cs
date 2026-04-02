namespace Lilys_CM.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommandDto
{
    public int UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}