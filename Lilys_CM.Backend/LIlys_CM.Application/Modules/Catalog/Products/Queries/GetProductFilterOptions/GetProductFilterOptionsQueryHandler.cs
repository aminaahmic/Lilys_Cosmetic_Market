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
            .Where(p => p.IsEnabled && !p.IsDeleted)
            .Where(p =>
                (p.BrandEntity != null && !string.IsNullOrWhiteSpace(p.BrandEntity.Name)) ||
                !string.IsNullOrWhiteSpace(p.Brand))
            .Select(p => p.BrandEntity != null ? p.BrandEntity.Name : p.Brand!)
            .ToListAsync(cancellationToken);

        var brands = brandsRaw
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(x => x)
            .ToList();

        var subcategoryQuery = _context.Subcategories
            .AsNoTracking()
            .Where(s => !s.IsDeleted && !string.IsNullOrWhiteSpace(s.Name));

        if (request.CategoryId.HasValue)
        {
            subcategoryQuery = subcategoryQuery
                .Where(s => s.CategoryId == request.CategoryId.Value);
        }

        var subcategories = await subcategoryQuery
            .Select(s => new SubcategoryFilterOptionDto
            {
                Id = s.Id,
                Name = s.Name
            })
            .OrderBy(s => s.Name)
            .ToListAsync(cancellationToken);

        return new ProductFilterOptionsDto
        {
            Brands = brands,
            Subcategories = subcategories
        };
    }
}