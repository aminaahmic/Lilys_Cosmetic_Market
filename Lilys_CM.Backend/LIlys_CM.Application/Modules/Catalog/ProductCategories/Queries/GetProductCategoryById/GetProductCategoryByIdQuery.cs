using Lilys_CM.Application.Modules.Catalog.ProductCategories.Common;

namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Queries.GetProductCategoryById;

public sealed class GetProductCategoryByIdQuery : IRequest<ProductCategoryDto>
{
    public int Id { get; init; }
}
