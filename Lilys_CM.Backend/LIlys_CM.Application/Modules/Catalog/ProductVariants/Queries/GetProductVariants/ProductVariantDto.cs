namespace Lilys_CM.Application.Modules.Catalog.ProductVariants.Queries.GetProductVariants;

public sealed class ProductVariantDto
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public decimal Price { get; init; }
    public int Stock { get; init; }

    public List<ProductVariantOptionDto> Options { get; init; } = [];
}

public sealed class ProductVariantOptionDto
{
    public int OptionId { get; init; }
    public string OptionName { get; init; } = string.Empty;

    public int OptionValueId { get; init; }
    public string Value { get; init; } = string.Empty;
}