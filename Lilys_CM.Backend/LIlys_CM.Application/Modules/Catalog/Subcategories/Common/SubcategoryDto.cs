namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Common;

public class SubcategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool IsEnabled { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
    public int ProductCount { get; set; }
}