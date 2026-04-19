namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.DisableCategory;

public sealed class DisableCategoryCommandHandler : IRequestHandler<DisableCategoryCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DisableCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DisableCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (category is null)
            throw new Lilys_CMNotFoundException("Category not found.");

        if (!category.IsEnabled)
            return Unit.Value;

        var hasEnabledProducts = await _context.Products
            .AnyAsync(p => p.CategoryId == request.Id && p.IsEnabled, cancellationToken);

        if (hasEnabledProducts)
        {
            throw new Lilys_CMBusinessRuleException(
                "category.disable.active-products",
                "Cannot disable category with enabled products."
            );
        }

        category.IsEnabled = false;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
