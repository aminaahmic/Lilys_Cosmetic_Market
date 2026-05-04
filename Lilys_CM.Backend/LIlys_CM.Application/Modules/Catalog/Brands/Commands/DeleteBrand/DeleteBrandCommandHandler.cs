using Lilys_CM.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Brands.Commands.DeleteBrand;

public sealed class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
{
    private readonly IAppDbContext _context;

    public DeleteBrandCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (brand is null)
        {
            throw new Lilys_CMNotFoundException("Brand not found.");
        }

        var hasProducts = await _context.Products
            .AnyAsync(x => x.BrandId == request.Id, cancellationToken);

        if (hasProducts)
        {
            throw new Lilys_CMConflictException("Brand cannot be deleted because it has products.");
        }

        _context.Brands.Remove(brand);

        await _context.SaveChangesAsync(cancellationToken);
    }
}