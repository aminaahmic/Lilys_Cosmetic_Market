using Lilys_CM.Application.Common;

namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductStockMovements;

public sealed class GetProductStockMovementsQuery : BasePagedQuery<ProductStockMovementDto>
{
    public int ProductId { get; set; }
}
