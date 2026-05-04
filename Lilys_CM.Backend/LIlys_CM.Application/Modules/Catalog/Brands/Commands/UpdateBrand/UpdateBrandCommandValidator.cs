using FluentValidation;

namespace Lilys_CM.Application.Modules.Catalog.Brands.Commands.UpdateBrand;

public sealed class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(160);

        RuleFor(x => x.Slug)
            .MaximumLength(180)
            .When(x => !string.IsNullOrWhiteSpace(x.Slug));

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.LogoUrl)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.LogoUrl));
    }
}