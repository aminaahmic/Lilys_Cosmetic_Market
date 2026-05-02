using Lilys_CM.Application.Modules.Catalog.Subcategories.Common;

namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Queries.GetSubcategories;

public sealed class GetSubcategoriesQueryHandler 
    : IRequestHandler<GetSubcategoriesQuery, PageResult<SubcategoryDto>>
{
    private readonly IAppDbContext _context;

    public GetSubcategoriesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PageResult<SubcategoryDto>> Handle(
        GetSubcategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Subcategories.AsQueryable();

        if (request.CategoryId.HasValue)
            query = query.Where(x => x.CategoryId == request.CategoryId.Value);

        if (request.OnlyEnabled.HasValue)
            query = query.Where(x => x.IsEnabled == request.OnlyEnabled.Value);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(x => x.Category.Name).ThenBy(x => x.Name);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Paging.SkipCount)
            .Take(request.Paging.PageSize)
            .Select(x => new SubcategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                IsEnabled = x.IsEnabled,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                ProductCount = x.Products.Count
            })
            .ToListAsync(cancellationToken);

        return new PageResult<SubcategoryDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Paging.Page,
            PageSize = request.Paging.PageSize
        };
    }
}