using Lilys_CM.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductFilterOptions;

public sealed class GetProductFilterOptionsQueryHandler :
    IRequestHandler<GetProductFilterOptionsQuery, ProductFilterOptionsDto>
{
    private readonly IAppDbContext _context;

    public GetProductFilterOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductFilterOptionsDto> Handle(
        GetProductFilterOptionsQuery request,
        CancellationToken cancellationToken)
    {
        var brandsRaw = await _context.Products
            .AsNoTracking()
            .Where(p => !string.IsNullOrWhiteSpace(p.Brand))
            .Select(p => p.Brand!)
            .ToListAsync(cancellationToken);

        var brands = brandsRaw
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(x => x)
            .ToList();

        var productSubcategoryQuery = _context.Products
            .AsNoTracking()
            .Where(p => !string.IsNullOrWhiteSpace(p.Subcategory));

        if (request.CategoryId.HasValue)
        {
            productSubcategoryQuery = productSubcategoryQuery
                .Where(p => p.CategoryId == request.CategoryId.Value);
        }

        var subcategoriesFromProducts = await productSubcategoryQuery
            .Select(p => p.Subcategory!)
            .ToListAsync(cancellationToken);

        var subcategoryTableQuery = _context.Subcategories
            .AsNoTracking()
            .Where(s => !string.IsNullOrWhiteSpace(s.Name));

        if (request.CategoryId.HasValue)
        {
            subcategoryTableQuery = subcategoryTableQuery
                .Where(s => s.CategoryId == request.CategoryId.Value);
        }

        var subcategoriesFromTable = await subcategoryTableQuery
            .Select(s => s.Name)
            .ToListAsync(cancellationToken);

        var subcategories = subcategoriesFromProducts
            .Concat(subcategoriesFromTable)
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(x => x)
            .ToList();

        return new ProductFilterOptionsDto
        {
            Brands = brands,
            Subcategories = subcategories
        };
    }
}
