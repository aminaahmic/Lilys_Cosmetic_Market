namespace Lilys_CM.Application.Modules.Catalog.ProductVariants.Commands.UpdateProductVariant;

public sealed class UpdateProductVariantCommandValidator
    : AbstractValidator<UpdateProductVariantCommand>
{
    public UpdateProductVariantCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0);

        RuleFor(x => x.VariantId)
            .GreaterThan(0);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Options)
            .NotEmpty()
            .WithMessage("At least one option is required.");

        RuleForEach(x => x.Options).ChildRules(option =>
        {
            option.RuleFor(x => x.OptionId)
                  .GreaterThan(0);
            option.RuleFor(x => x.Value)
                .NotEmpty()
                .MaximumLength(100);
        });
    }
}