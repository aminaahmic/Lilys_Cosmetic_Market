using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
            throw new Lilys_CMNotFoundException("Product not found.");

        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
            throw new Lilys_CMNotFoundException("Category not found.");

        product.Name = request.Name.Trim();
        product.Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim();
        product.Brand = string.IsNullOrWhiteSpace(request.Brand) ? null : request.Brand.Trim();
        product.Subcategory = string.IsNullOrWhiteSpace(request.Subcategory) ? null : request.Subcategory.Trim();
        product.Price = request.Price;
        product.IsEnabled = request.IsEnabled;
        product.CategoryId = request.CategoryId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
