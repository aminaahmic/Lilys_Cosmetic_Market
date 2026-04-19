namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Commands.EnableCategory;

public sealed class EnableCategoryCommandHandler : IRequestHandler<EnableCategoryCommand, Unit>
{
    private readonly IAppDbContext _context;

    public EnableCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(EnableCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (category is null)
            throw new Lilys_CMNotFoundException("Category not found.");

        if (category.IsEnabled)
            return Unit.Value;

        category.IsEnabled = true;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
