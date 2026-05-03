namespace Lilys_CM.Application.Modules.Catalog.ProductVariants.Commands.DeleteProductVariant;

public sealed class DeleteProductVariantCommand : IRequest
{
    public int ProductId { get; init; }
    public int VariantId { get; init; }
}