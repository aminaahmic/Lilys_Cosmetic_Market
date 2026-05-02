using Lilys_CM.Application.Modules.Catalog.Subcategories.Common;

namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Queries.GetSubcategories;

public sealed class GetSubcategoriesQuery : BasePagedQuery<SubcategoryDto>
{
    public int? CategoryId { get; init; }
    public bool? OnlyEnabled { get; init; }
    public string? Search { get; init; }
}