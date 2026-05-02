namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.CreateSubcategory;

public sealed class CreateSubcategoryCommandValidator 
    : AbstractValidator<CreateSubcategoryCommand>
{
    public CreateSubcategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0);
    }
}