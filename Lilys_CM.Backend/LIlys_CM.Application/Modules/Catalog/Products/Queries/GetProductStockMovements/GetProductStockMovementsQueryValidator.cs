namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductStockMovements;

public sealed class GetProductStockMovementsQueryValidator : AbstractValidator<GetProductStockMovementsQuery>
{
    public GetProductStockMovementsQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0);
    }
}
