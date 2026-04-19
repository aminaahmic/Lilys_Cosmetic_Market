namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.DisableCategory;

public sealed class DisableCategoryCommandValidator : AbstractValidator<DisableCategoryCommand>
{
    public DisableCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
