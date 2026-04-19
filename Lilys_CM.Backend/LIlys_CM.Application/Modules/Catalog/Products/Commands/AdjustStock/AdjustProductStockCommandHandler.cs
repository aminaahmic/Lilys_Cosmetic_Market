using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.AdjustStock;

public sealed class AdjustProductStockCommandHandler : IRequestHandler<AdjustProductStockCommand, Unit>
{
    private readonly IAppDbContext _context;

    public AdjustProductStockCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AdjustProductStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product is null)
        {
            throw new Lilys_CMNotFoundException("Product not found.");
        }

        var previousQuantity = product.StockQuantity;
        var newQuantity = previousQuantity + request.QuantityDelta;

        if (newQuantity < 0)
        {
            throw new Lilys_CMBusinessRuleException(
                "stock.insufficient",
                "Stock cannot go below zero. Reduce quantity delta or restock first.");
        }

        product.StockQuantity = newQuantity;

        var movement = new ProductStockMovementEntity
        {
            ProductId = product.Id,
            QuantityDelta = request.QuantityDelta,
            PreviousQuantity = previousQuantity,
            NewQuantity = newQuantity,
            Reason = request.Reason.Trim(),
            Note = string.IsNullOrWhiteSpace(request.Note) ? null : request.Note.Trim()
        };

        _context.ProductStockMovements.Add(movement);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
