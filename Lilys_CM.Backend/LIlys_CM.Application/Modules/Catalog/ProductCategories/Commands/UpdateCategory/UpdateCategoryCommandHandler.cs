using Lilys_CM.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (category == null)
            throw new Lilys_CMNotFoundException("Category not found.");

        var normalizedName = request.Name.Trim().ToLower();

        var nameInUse = await _context.Categories
            .AnyAsync(x => x.Id != request.Id && x.Name.ToLower() == normalizedName, cancellationToken);

        if (nameInUse)
            throw new Lilys_CMConflictException("Category with the same name already exists.");

        category.Name = request.Name.Trim();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
