namespace Lilys_CM.Application.Modules.Catalog.ProductVariants.Commands.UpdateProductVariant;

public sealed class UpdateProductVariantCommand : IRequest
{
    public int ProductId { get; set; }
    public int VariantId { get; set; }

    public decimal Price { get; init; }
    public int Stock { get; init; }

    public List<UpdateProductVariantOptionCommand> Options { get; init; } = [];
}

public sealed class UpdateProductVariantOptionCommand
{
    public int OptionId { get; init; }
    public string Value { get; init; } = string.Empty;
}