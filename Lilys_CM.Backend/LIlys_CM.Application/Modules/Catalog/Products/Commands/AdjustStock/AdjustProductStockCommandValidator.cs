namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.AdjustStock;

public sealed class AdjustProductStockCommandValidator : AbstractValidator<AdjustProductStockCommand>
{
    public AdjustProductStockCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0);

        RuleFor(x => x.QuantityDelta)
            .NotEqual(0);

        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(x => x.Note)
            .MaximumLength(500);
    }
}
