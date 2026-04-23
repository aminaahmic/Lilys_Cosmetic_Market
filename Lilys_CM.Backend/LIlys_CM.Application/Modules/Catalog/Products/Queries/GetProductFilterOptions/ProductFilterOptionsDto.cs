namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductFilterOptions;

public sealed class ProductFilterOptionsDto
{
    public List<string> Brands { get; init; } = new();
    public List<string> Subcategories { get; init; } = new();
    
}
