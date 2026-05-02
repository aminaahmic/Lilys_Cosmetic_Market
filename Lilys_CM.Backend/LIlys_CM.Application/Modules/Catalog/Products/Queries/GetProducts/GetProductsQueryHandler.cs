using Lilys_CM.Application.Abstractions;
using Lilys_CM.Application.Common;
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
            .Include(p => p.Category)
            .Include(p => p.Subcategory)
            .Where(p => !p.IsDeleted)
            .AsQueryable();

        if (request.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == request.CategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Brand))
        {
            var brand = request.Brand.Trim().ToLower();
            query = query.Where(p => p.Brand != null && p.Brand.ToLower().Contains(brand));
        }

        if (!string.IsNullOrWhiteSpace(request.Subcategory))
        {
            var subcategory = request.Subcategory.Trim().ToLower();
            query = query.Where(p => p.Subcategory != null && p.Subcategory.Name.ToLower().Contains(subcategory));
        }

        if (request.PriceMin.HasValue)
        {
            query = query.Where(p => p.Price >= request.PriceMin.Value);
        }

        if (request.PriceMax.HasValue)
        {
            query = query.Where(p => p.Price <= request.PriceMax.Value);
        }

        if (request.IsEnabled.HasValue)
        {
            query = query.Where(x => x.IsEnabled == request.IsEnabled.Value);
        }
        
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();

            query = query.Where(p =>
                p.Name.ToLower().Contains(search) ||
                p.Sku.ToLower().Contains(search) ||
                p.Slug.ToLower().Contains(search) ||
                (p.Brand != null && p.Brand.ToLower().Contains(search)) ||
                (p.Subcategory != null && p.Subcategory.Name.ToLower().Contains(search)) ||
                (p.Description != null && p.Description.ToLower().Contains(search)) ||
                (p.ShortDescription != null && p.ShortDescription.ToLower().Contains(search)) ||
                p.Category.Name.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(p => p.Name)
            .Skip(request.Paging.SkipCount)
            .Take(request.Paging.PageSize)
            .Select(p => new ProductDto
            {
                Id = p.Id,

                Name = p.Name,
                Sku = p.Sku,
                Slug = p.Slug,

                ImageUrl = p.ImageUrl,
                ShortDescription = p.ShortDescription,
                Description = p.Description,
                Ingredients = p.Ingredients,
                HowToUse = p.HowToUse,
                Benefits = p.Benefits,

                Brand = p.Brand,
                Size = p.Size,
                CountryOfOrigin = p.CountryOfOrigin,
                Barcode = p.Barcode,

                Price = p.Price,
                CompareAtPrice = p.CompareAtPrice,

                StockQuantity = p.StockQuantity,

                IsEnabled = p.IsEnabled,
                IsFeatured = p.IsFeatured,

                SeoTitle = p.SeoTitle,
                SeoDescription = p.SeoDescription,

                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,

                SubcategoryId = p.SubcategoryId,
                SubcategoryName = p.Subcategory != null ? p.Subcategory.Name : null
            })
            .ToListAsync(cancellationToken);

        return new PageResult<ProductDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Paging.Page,
            PageSize = request.Paging.PageSize
        };
    }
}