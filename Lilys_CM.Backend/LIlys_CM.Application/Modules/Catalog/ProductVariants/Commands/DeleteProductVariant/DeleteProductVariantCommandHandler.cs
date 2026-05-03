namespace Lilys_CM.Application.Modules.Catalog.ProductVariants.Commands.DeleteProductVariant;

public sealed class DeleteProductVariantCommandHandler
    : IRequestHandler<DeleteProductVariantCommand>
{
    private readonly IAppDbContext _context;

    public DeleteProductVariantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteProductVariantCommand request,
        CancellationToken cancellationToken)
    {
        var variant = await _context.ProductVariants
            .FirstOrDefaultAsync(
                x => x.Id == request.VariantId && x.ProductId == request.ProductId,
                cancellationToken);

        if (variant is null)
        {
            throw new Lilys_CMNotFoundException("Product variant not found.");
        }

        var variantOptions = await _context.VariantOptionEntities
            .Where(x => x.VariantId == variant.Id)
            .ToListAsync(cancellationToken);

        _context.VariantOptionEntities.RemoveRange(variantOptions);
        _context.ProductVariants.Remove(variant);

        await _context.SaveChangesAsync(cancellationToken);
    }
}