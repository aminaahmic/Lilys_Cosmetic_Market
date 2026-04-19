using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductStockMovements;

public sealed class GetProductStockMovementsQueryHandler :
    IRequestHandler<GetProductStockMovementsQuery, PageResult<ProductStockMovementDto>>
{
    private readonly IAppDbContext _context;

    public GetProductStockMovementsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PageResult<ProductStockMovementDto>> Handle(
        GetProductStockMovementsQuery request,
        CancellationToken cancellationToken)
    {
        var productExists = await _context.Products
            .AnyAsync(x => x.Id == request.ProductId, cancellationToken);

        if (!productExists)
        {
            throw new Lilys_CMNotFoundException("Product not found.");
        }

        var query = _context.ProductStockMovements
            .Where(x => x.ProductId == request.ProductId)
            .OrderByDescending(x => x.CreatedAtUtc);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Paging.SkipCount)
            .Take(request.Paging.PageSize)
            .Select(x => new ProductStockMovementDto
            {
                Id = x.Id,
                QuantityDelta = x.QuantityDelta,
                PreviousQuantity = x.PreviousQuantity,
                NewQuantity = x.NewQuantity,
                Reason = x.Reason,
                Note = x.Note,
                CreatedAtUtc = x.CreatedAtUtc
            })
            .ToListAsync(cancellationToken);

        return new PageResult<ProductStockMovementDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Paging.Page,
            PageSize = request.Paging.PageSize
        };
    }
}
