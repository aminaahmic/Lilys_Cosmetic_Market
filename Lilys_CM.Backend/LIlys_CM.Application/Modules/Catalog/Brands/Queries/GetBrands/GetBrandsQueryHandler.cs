using Lilys_CM.Application.Abstractions;
using Lilys_CM.Application.Modules.Catalog.Brands.Common;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Brands.Queries.GetBrands;

public sealed class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<BrandDto>>
{
    private readonly IAppDbContext _context;

    public GetBrandsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Brands.AsNoTracking();

        if (request.OnlyEnabled == true)
        {
            query = query.Where(x => x.IsEnabled);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();

            query = query.Where(x =>
                x.Name.ToLower().Contains(search) ||
                (x.Description != null && x.Description.ToLower().Contains(search)));
        }

        return await query
            .OrderBy(x => x.Name)
            .Select(x => new BrandDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description,
                LogoUrl = x.LogoUrl,
                IsEnabled = x.IsEnabled,
                ProductsCount = x.Products.Count
            })
            .ToListAsync(cancellationToken);
    }
}