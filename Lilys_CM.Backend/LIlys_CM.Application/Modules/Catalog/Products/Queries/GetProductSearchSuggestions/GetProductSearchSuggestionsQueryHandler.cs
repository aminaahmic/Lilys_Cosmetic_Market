using Lilys_CM.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductSearchSuggestions;

public sealed class GetProductSearchSuggestionsQueryHandler
    : IRequestHandler<GetProductSearchSuggestionsQuery, List<string>>
{
    private readonly IAppDbContext _context;

    public GetProductSearchSuggestionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> Handle(
        GetProductSearchSuggestionsQuery request,
        CancellationToken cancellationToken)
    {
        var search = request.Search?.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(search) || search.Length < 2)
        {
            return new List<string>();
        }

        var take = request.Take <= 0 ? 8 : Math.Min(request.Take, 12);

        var productNames = await _context.Products
            .AsNoTracking()
            .Where(p =>
                !p.IsDeleted &&
                p.IsEnabled &&
                p.Name.ToLower().Contains(search))
            .OrderBy(p => p.Name)
            .Select(p => p.Name)
            .Take(take)
            .ToListAsync(cancellationToken);

        var brandNames = await _context.Products
            .AsNoTracking()
            .Where(p =>
                !p.IsDeleted &&
                p.IsEnabled &&
                p.BrandEntity != null &&
                p.BrandEntity.Name.ToLower().Contains(search))
            .OrderBy(p => p.BrandEntity!.Name)
            .Select(p => p.BrandEntity!.Name)
            .Distinct()
            .Take(take)
            .ToListAsync(cancellationToken);

        var categoryNames = await _context.Products
            .AsNoTracking()
            .Where(p =>
                !p.IsDeleted &&
                p.IsEnabled &&
                p.Category.Name.ToLower().Contains(search))
            .OrderBy(p => p.Category.Name)
            .Select(p => p.Category.Name)
            .Distinct()
            .Take(take)
            .ToListAsync(cancellationToken);

        return productNames
            .Concat(brandNames)
            .Concat(categoryNames)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .Take(take)
            .ToList();
    }
}