namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductFilterOptions;

public sealed class GetProductFilterOptionsQuery : IRequest<ProductFilterOptionsDto>
{
    public int? CategoryId { get; init; }
}
