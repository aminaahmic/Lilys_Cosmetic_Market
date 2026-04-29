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


        if (request.OnlyEnabled.HasValue)
        {
            query = query.Where(c => c.IsEnabled == request.OnlyEnabled.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(search));
        }

        query = request.SortBy switch
        {
            "nameDesc" => query.OrderByDescending(c => c.Name),
            _ => query.OrderBy(c => c.Name)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Paging.SkipCount)
            .Take(request.Paging.PageSize)
            .Select(c => new ProductCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                IsEnabled = c.IsEnabled,
                CreatedAt = c.CreatedAt,
                ProductCount = c.Products.Count,
                Icon = c.Icon
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

    private static string GetCategoryIcon(string name)
    {
        var normalized = name.Trim().ToLower();

        return normalized switch
        {
            "kreme" => "spa",
            "serumi" => "science",
            "parfemi" => "local_florist",
            "šminka" or "sminka" => "brush",
            "njega kose" => "content_cut",
            "šamponi" or "samponi" => "shower",
            "ruževi" or "ruzevi" => "face",
            "maskare" => "visibility",
            _ => "category"
        };
    }
}
