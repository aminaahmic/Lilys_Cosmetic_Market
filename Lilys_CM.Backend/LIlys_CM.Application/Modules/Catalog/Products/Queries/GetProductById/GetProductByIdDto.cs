namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductById;

public sealed class GetProductByIdDto
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string Slug { get; set; } = default!;

    public string? ImageUrl { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public string? Ingredients { get; set; }
    public string? HowToUse { get; set; }
    public string? Benefits { get; set; }

    public string? Brand { get; set; }
    public int? BrandId { get; set; }
    public string? BrandName { get; set; }
    public string? BrandLogoUrl { get; set; }
    public string? Size { get; set; }
    public string? CountryOfOrigin { get; set; }
    public string? Barcode { get; set; }

    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }

    public int StockQuantity { get; set; }

    public bool IsEnabled { get; set; }
    public bool IsFeatured { get; set; }

    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;

    public int? SubcategoryId { get; set; }
    public string? SubcategoryName { get; set; }
}