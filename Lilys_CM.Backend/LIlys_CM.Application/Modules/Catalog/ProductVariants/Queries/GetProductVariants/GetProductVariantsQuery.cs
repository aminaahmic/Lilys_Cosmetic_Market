namespace Lilys_CM.Application.Modules.Catalog.ProductVariants.Queries.GetProductVariants;

public sealed class GetProductVariantsQuery : IRequest<List<ProductVariantDto>>
{
    public int ProductId { get; init; }
}