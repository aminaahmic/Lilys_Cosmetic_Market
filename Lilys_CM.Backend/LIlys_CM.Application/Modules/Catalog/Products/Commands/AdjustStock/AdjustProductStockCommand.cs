namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.AdjustStock;

public sealed class AdjustProductStockCommand : IRequest<Unit>
{
    public int ProductId { get; set; }
    public int QuantityDelta { get; init; }
    public string Reason { get; init; } = default!;
    public string? Note { get; init; }
}
