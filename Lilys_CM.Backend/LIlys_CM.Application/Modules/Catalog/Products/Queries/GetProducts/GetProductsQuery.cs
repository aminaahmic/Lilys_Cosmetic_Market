using Lilys_CM.Application.Common;

namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProducts;

public sealed class GetProductsQuery : BasePagedQuery<ProductDto>
{
    public int? CategoryId { get; init; }
    public string? Brand { get; init; }
    public int? BrandId { get; init; }
    public string? Subcategory { get; init; }
    public decimal? PriceMin { get; init; }
    public decimal? PriceMax { get; init; }
    public bool? IsEnabled { get; init; }
    public string? Search { get; init; }
}
