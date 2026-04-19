namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.EnableCategory;

public sealed class EnableCategoryCommandValidator : AbstractValidator<EnableCategoryCommand>
{
    public EnableCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
