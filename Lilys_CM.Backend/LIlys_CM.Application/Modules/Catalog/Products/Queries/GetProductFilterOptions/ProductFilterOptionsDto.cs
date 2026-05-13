namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductFilterOptions;

public sealed class ProductFilterOptionsDto
{
    public List<string> Brands { get; set; } = [];
    public List<SubcategoryFilterOptionDto> Subcategories { get; set; } = [];
}

public sealed class SubcategoryFilterOptionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}