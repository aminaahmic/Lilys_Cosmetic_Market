using Lilys_CM.Application.Common;
using MediatR;

namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<PageResult<ProductDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public int? CategoryId { get; set; }
    public int? SubcategoryId { get; set; }
    public string? Search { get; set; }
}