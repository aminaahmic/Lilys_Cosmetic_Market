using Lilys_CM.Domain.Entities.Common;

namespace Lilys_CM.Domain.Entities.Catalog;

public class ProductStockMovementEntity : BaseEntity
{
    public int ProductId { get; set; }
    public ProductEntity Product { get; set; } = default!;

    public int QuantityDelta { get; set; }
    public int PreviousQuantity { get; set; }
    public int NewQuantity { get; set; }

    public string Reason { get; set; } = default!;
    public string? Note { get; set; }
}
