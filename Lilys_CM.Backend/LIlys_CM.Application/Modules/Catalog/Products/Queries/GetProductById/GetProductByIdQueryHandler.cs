using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdDto>
{
    private readonly IAppDbContext _context;

    public GetProductByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductByIdDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
            throw new Lilys_CMNotFoundException("Product not found.");

        return new GetProductByIdDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Brand = product.Brand,
            Subcategory = product.Subcategory,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            IsEnabled = product.IsEnabled,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name
        };
    }
}
