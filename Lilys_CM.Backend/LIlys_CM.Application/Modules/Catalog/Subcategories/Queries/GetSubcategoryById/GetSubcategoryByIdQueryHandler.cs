using Lilys_CM.Application.Abstractions;
using Lilys_CM.Application.Common.Exceptions;
using Lilys_CM.Application.Modules.Catalog.Subcategories.Common;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Subcategories.Queries.GetSubcategoryById;

public class GetSubcategoryByIdQueryHandler : IRequestHandler<GetSubcategoryByIdQuery, SubcategoryDto>
{
    private readonly IAppDbContext _context;

    public GetSubcategoryByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<SubcategoryDto> Handle(GetSubcategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var subcategory = await _context.Subcategories
            .Where(x => x.Id == request.Id)
            .Select(x => new SubcategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                IsEnabled = x.IsEnabled,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                ProductCount = x.Products.Count
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (subcategory is null)
            throw new Lilys_CMNotFoundException("Subcategory not found.");

        return subcategory;
    }
}