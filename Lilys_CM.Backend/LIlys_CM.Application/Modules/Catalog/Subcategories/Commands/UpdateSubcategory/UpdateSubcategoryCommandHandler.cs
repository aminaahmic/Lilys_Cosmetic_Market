using Lilys_CM.Application.Abstractions;
using Lilys_CM.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.UpdateSubcategory;

public class UpdateSubcategoryCommandHandler : IRequestHandler<UpdateSubcategoryCommand>
{
    private readonly IAppDbContext _context;

    public UpdateSubcategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var subcategory = await _context.Subcategories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (subcategory is null)
            throw new Lilys_CMNotFoundException("Subcategory not found.");

        var categoryExists = await _context.Categories
            .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
            throw new Lilys_CMNotFoundException("Category not found.");

        var normalizedName = request.Name.Trim().ToLower();

        var exists = await _context.Subcategories
            .AnyAsync(x =>
                x.Id != request.Id &&
                x.CategoryId == request.CategoryId &&
                x.Name.ToLower() == normalizedName,
                cancellationToken);

        if (exists)
            throw new Lilys_CMConflictException("Subcategory with same name already exists in this category.");

        subcategory.Name = request.Name.Trim();
        subcategory.CategoryId = request.CategoryId;
        subcategory.IsEnabled = request.IsEnabled;

        await _context.SaveChangesAsync(cancellationToken);
    }
}