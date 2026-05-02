using Lilys_CM.Application.Abstractions;
using Lilys_CM.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.DeleteSubcategory;

public class DeleteSubcategoryCommandHandler : IRequestHandler<DeleteSubcategoryCommand>
{
    private readonly IAppDbContext _context;

    public DeleteSubcategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var subcategory = await _context.Subcategories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (subcategory is null)
            throw new Lilys_CMNotFoundException("Subcategory not found.");

        var hasProducts = await _context.Products
            .AnyAsync(x => x.SubcategoryId == request.Id, cancellationToken);

        if (hasProducts)
            throw new Lilys_CMBusinessRuleException(
                "subcategory.delete.products-exist",
                "Cannot delete subcategory that contains products."
            );

        _context.Subcategories.Remove(subcategory);

        await _context.SaveChangesAsync(cancellationToken);
    }
}