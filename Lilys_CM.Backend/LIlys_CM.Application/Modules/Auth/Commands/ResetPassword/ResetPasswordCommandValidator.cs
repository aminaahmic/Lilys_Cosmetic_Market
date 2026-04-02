namespace Lilys_CM.Application.Modules.Auth.Commands.ResetPassword;

public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Reset token je obavezan.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Nova lozinka je obavezna.")
            .MinimumLength(6).WithMessage("Nova lozinka mora imati najmanje 6 karaktera.")
            .MaximumLength(100).WithMessage("Nova lozinka može imati najviše 100 karaktera.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Potvrda lozinke je obavezna.");

        RuleFor(x => x)
            .Must(x => x.NewPassword == x.ConfirmPassword)
            .WithMessage("Lozinka i potvrda lozinke se ne poklapaju.");
    }
}