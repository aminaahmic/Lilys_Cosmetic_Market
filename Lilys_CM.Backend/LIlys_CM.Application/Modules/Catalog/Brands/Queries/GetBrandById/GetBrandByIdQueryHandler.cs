using Lilys_CM.Application.Abstractions;
using Lilys_CM.Application.Modules.Catalog.Brands.Common;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Brands.Queries.GetBrandById;

public sealed class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandDto>
{
    private readonly IAppDbContext _context;

    public GetBrandByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        if (brand is null)
        {
            throw new Lilys_CMNotFoundException("Brand not found.");
        }

        return brand;
    }
}