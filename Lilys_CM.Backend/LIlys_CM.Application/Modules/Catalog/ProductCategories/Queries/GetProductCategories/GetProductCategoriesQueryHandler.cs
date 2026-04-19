using Lilys_CM.Application.Abstractions;
using Lilys_CM.Application.Common;
using Lilys_CM.Application.Modules.Catalog.ProductCategories.Common;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Queries.GetProductCategories;

public sealed class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, PageResult<ProductCategoryDto>>
{
    private readonly IAppDbContext _context;

    public GetProductCategoriesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PageResult<ProductCategoryDto>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Categories.AsQueryable();

        if (request.OnlyEnabled == true)
        {
            query = query.Where(c => c.IsEnabled);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(c => c.Name)
            .Skip(request.Paging.SkipCount)
            .Take(request.Paging.PageSize)
            .Select(c => new ProductCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                IsEnabled = c.IsEnabled
            })
            .ToListAsync(cancellationToken);

        return new PageResult<ProductCategoryDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Paging.Page,
            PageSize = request.Paging.PageSize
        };
    }
}
