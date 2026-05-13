using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.DeleteProduct;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
            throw new Lilys_CMNotFoundException("Product not found.");

        product.MarkDeleted();
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}