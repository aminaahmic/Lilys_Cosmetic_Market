namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProducts;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int? SubcategoryId { get; set; }
    public string SubcategoryName { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
}