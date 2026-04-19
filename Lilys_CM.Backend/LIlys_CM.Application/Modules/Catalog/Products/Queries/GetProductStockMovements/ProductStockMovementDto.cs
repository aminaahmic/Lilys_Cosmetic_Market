namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductStockMovements;

public sealed class ProductStockMovementDto
{
    public int Id { get; set; }
    public int QuantityDelta { get; set; }
    public int PreviousQuantity { get; set; }
    public int NewQuantity { get; set; }
    public string Reason { get; set; } = default!;
    public string? Note { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
