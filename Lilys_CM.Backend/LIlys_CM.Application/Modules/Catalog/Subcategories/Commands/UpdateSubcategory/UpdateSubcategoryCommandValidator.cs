using FluentValidation;

namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.UpdateSubcategory;

public class UpdateSubcategoryCommandValidator : AbstractValidator<UpdateSubcategoryCommand>
{
    public UpdateSubcategoryCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0);
    }
}