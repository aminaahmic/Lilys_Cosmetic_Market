using Lilys_CM.Application.Modules.Auth.Commands.ForgotPassword;
using Lilys_CM.Application.Modules.Auth.Commands.Login;
using Lilys_CM.Application.Modules.Auth.Commands.Logout;
using Lilys_CM.Application.Modules.Auth.Commands.Refresh;
using Lilys_CM.Application.Modules.Auth.Commands.Register;
using Lilys_CM.Application.Modules.Auth.Commands.ResetPassword;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginCommandDto>> Login([FromBody] LoginCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<RegisterCommandDto>> Register([FromBody] RegisterCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<ActionResult<ForgotPasswordCommandDto>> ForgotPassword([FromBody] ForgotPasswordCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<ActionResult<ResetPasswordCommandDto>> ResetPassword([FromBody] ResetPasswordCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<RefreshTokenCommandDto>> Refresh([FromBody] RefreshTokenCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task Logout([FromBody] LogoutCommand command, CancellationToken ct)
    {
        await mediator.Send(command, ct);
    }
}