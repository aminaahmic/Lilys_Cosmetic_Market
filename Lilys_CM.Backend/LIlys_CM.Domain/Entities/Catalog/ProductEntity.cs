namespace Lilys_CM.Domain.Entities.Catalog;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Sku { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public string? Ingredients { get; set; }
    public string? HowToUse { get; set; }
    public string? Benefits { get; set; }

    public string? Brand { get; set; }
    public int? BrandId { get; set; }

    public BrandEntity? BrandEntity { get; set; }
    public string? Size { get; set; }
    public string? CountryOfOrigin { get; set; }
    public string? Barcode { get; set; }

    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }

    public int StockQuantity { get; set; }

    public bool IsEnabled { get; set; } = true;
    public bool IsFeatured { get; set; } = false;

    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }

    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = default!;

    public int? SubcategoryId { get; set; }
    public SubcategoryEntity? Subcategory { get; set; }
    public ICollection<ProductStockMovementEntity> StockMovements { get; set; } = new List<ProductStockMovementEntity>();
}

