namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Commands.CreateSubcategory;

public sealed class CreateSubcategoryCommandHandler 
    : IRequestHandler<CreateSubcategoryCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateSubcategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _context.Categories
            .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
            throw new Lilys_CMNotFoundException("Category not found.");

        var normalizedName = request.Name.Trim().ToLower();

        var subcategoryExists = await _context.Subcategories
            .AnyAsync(x =>
                x.CategoryId == request.CategoryId &&
                x.Name.ToLower() == normalizedName,
                cancellationToken);

        if (subcategoryExists)
            throw new Lilys_CMConflictException("Subcategory with the same name already exists in this category.");

        var subcategory = new SubcategoryEntity
        {
            Name = request.Name.Trim(),
            CategoryId = request.CategoryId,
            IsEnabled = request.IsEnabled
        };

        _context.Subcategories.Add(subcategory);
        await _context.SaveChangesAsync(cancellationToken);

        return subcategory.Id;
    }
}