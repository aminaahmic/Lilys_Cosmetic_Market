using Lilys_CM.Application.Abstractions;
using Lilys_CM.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PageResult<ProductDto>>
{
    private readonly IAppDbContext _context;

    public GetProductsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PageResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products
            .Include(p => p.Subcategory)
            .ThenInclude(s => s.Category)
            .AsQueryable();

        if (request.SubcategoryId.HasValue)
        {
            query = query.Where(p => p.SubcategoryId == request.SubcategoryId.Value);
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(p => p.Subcategory != null && p.Subcategory.CategoryId == request.CategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            query = query.Where(p =>
                p.Name.Contains(search) ||
                (p.Description != null && p.Description.Contains(search)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(p => p.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                SubcategoryId = p.SubcategoryId,
                SubcategoryName = p.Subcategory != null ? p.Subcategory.Name : string.Empty,
                CategoryName = p.Subcategory != null && p.Subcategory.Category != null
                    ? p.Subcategory.Category.Name
                    : string.Empty
            })
            .ToListAsync(cancellationToken);

        return new PageResult<ProductDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}