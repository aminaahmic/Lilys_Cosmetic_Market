namespace Lilys_CM.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ime je obavezno.")
            .MinimumLength(2).WithMessage("Ime mora imati najmanje 2 karaktera.")
            .MaximumLength(100).WithMessage("Ime može imati najviše 100 karaktera.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email je obavezan.")
            .EmailAddress().WithMessage("Unesite ispravan email.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Lozinka je obavezna.")
            .MinimumLength(6).WithMessage("Lozinka mora imati najmanje 6 karaktera.")
            .MaximumLength(100).WithMessage("Lozinka može imati najviše 100 karaktera.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Potvrda lozinke je obavezna.");

        RuleFor(x => x)
            .Must(x => x.Password == x.ConfirmPassword)
            .WithMessage("Lozinka i potvrda lozinke se ne poklapaju.");
    }
}