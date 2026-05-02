using Lilys_CM.Application.Modules.Catalog.ProductCategories.Common;

namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Queries.GetProductCategoryById;

public sealed class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, ProductCategoryDto>
{
    private readonly IAppDbContext _context;

    public GetProductCategoryByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductCategoryDto> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
    .Include(c => c.Products)
    .Include(c => c.Subcategories)
    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (category is null)
            throw new Lilys_CMNotFoundException("Category not found.");

        return new ProductCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            IsEnabled = category.IsEnabled,
            CreatedAtUtc = category.CreatedAtUtc,
            ProductCount = category.Products.Count,
            SubcategoryCount = category.Subcategories.Count,
            Icon = category.Icon
        };
    }
}
