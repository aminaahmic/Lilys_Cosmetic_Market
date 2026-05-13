namespace Lilys_CM.Application.Modules.Catalog.ProductVariants.Commands.CreateProductVariant;

public sealed class CreateProductVariantCommand : IRequest<int>
{
    public int ProductId { get; set; }

    public decimal Price { get; init; }
    public int Stock { get; init; }

    public List<CreateProductVariantOptionCommand> Options { get; init; } = [];
}

public sealed class CreateProductVariantOptionCommand
{
    public int OptionId { get; init; }
    public string Value { get; init; } = string.Empty;
}