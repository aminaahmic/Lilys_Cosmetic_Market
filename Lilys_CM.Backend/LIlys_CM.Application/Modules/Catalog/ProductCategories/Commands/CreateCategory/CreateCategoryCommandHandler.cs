using Lilys_CM.Application.Abstractions;
using Lilys_CM.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var normalizedName = request.Name.Trim().ToLower();

        var categoryExists = await _context.Categories
            .AnyAsync(x => x.Name.ToLower() == normalizedName, cancellationToken);

        if (categoryExists)
            throw new Lilys_CMConflictException("Category with the same name already exists.");

        var category = new CategoryEntity
        {
            Name = request.Name.Trim(),
            IsEnabled = request.IsEnabled,
            Icon=request.Icon
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
