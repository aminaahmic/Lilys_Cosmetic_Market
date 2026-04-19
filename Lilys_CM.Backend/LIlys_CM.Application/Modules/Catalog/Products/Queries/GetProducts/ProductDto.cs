namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProducts;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public string? Subcategory { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsEnabled { get; set; }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
}
