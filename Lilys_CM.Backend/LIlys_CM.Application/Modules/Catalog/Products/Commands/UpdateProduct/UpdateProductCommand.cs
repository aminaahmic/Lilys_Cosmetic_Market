using MediatR;

namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommand : IRequest<Unit>
{
    public int Id { get; init; }

    public string Name { get; init; } = default!;
    public string Sku { get; init; } = string.Empty;
    public string? Slug { get; init; }

    public string? ImageUrl { get; init; }
    public string? ShortDescription { get; init; }
    public string? Description { get; init; }
    public string? Ingredients { get; init; }
    public string? HowToUse { get; init; }
    public string? Benefits { get; init; }

    public string? Brand { get; init; }
    public string? Size { get; init; }
    public string? CountryOfOrigin { get; init; }
    public string? Barcode { get; init; }

    public decimal Price { get; init; }
    public decimal? CompareAtPrice { get; init; }

    public int StockQuantity { get; init; }

    public bool IsEnabled { get; init; }
    public bool IsFeatured { get; init; }

    public string? SeoTitle { get; init; }
    public string? SeoDescription { get; init; }

    public int CategoryId { get; init; }
    public int? SubcategoryId { get; init; }
}