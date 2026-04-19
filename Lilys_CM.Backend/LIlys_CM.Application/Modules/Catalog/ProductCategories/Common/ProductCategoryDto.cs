namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Common;

public class ProductCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool IsEnabled { get; set; }
}
