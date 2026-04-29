using Lilys_CM.Application.Modules.Catalog.ProductCategories.Common;
using Lilys_CM.Application.Common;

namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Queries.GetProductCategories;

public sealed class GetProductCategoriesQuery : BasePagedQuery<ProductCategoryDto>
{
    public string? Search { get; init; }
    public bool? OnlyEnabled { get; init; }
    public string? SortBy { get; set; }
}
