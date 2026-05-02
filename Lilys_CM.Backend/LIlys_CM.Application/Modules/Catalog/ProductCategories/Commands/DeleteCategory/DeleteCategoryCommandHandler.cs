using Lilys_CM.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.DeleteCategory;

public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (category == null)
            throw new Lilys_CMNotFoundException("Category not found.");

        var hasProducts = await _context.Products
            .AnyAsync(p => p.CategoryId == request.Id, cancellationToken);

        if (hasProducts)
        {
            throw new Lilys_CMBusinessRuleException(
                "category.delete.products-exist",
                "Cannot delete category that contains products."
            );
        }
        var hasSubcategories = await _context.Subcategories
            .AnyAsync(s => s.CategoryId == request.Id, cancellationToken);

        if (hasSubcategories)
        {
            throw new Lilys_CMBusinessRuleException(
                "category.delete.subcategories-exist",
                "Cannot delete category that contains subcategories."
            );
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
