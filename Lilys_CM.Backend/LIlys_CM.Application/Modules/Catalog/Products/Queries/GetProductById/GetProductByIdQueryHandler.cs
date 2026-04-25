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
            .Include(p => p.Subcategory)
            .FirstOrDefaultAsync(p => p.Id == request.Id && p.IsEnabled, cancellationToken);

        if (product is null)
            throw new Lilys_CMNotFoundException("Product not found.");

        return new GetProductByIdDto
        {
            Id = product.Id,

            Name = product.Name,
            Sku = product.Sku,
            Slug = product.Slug,

            ImageUrl = product.ImageUrl,
            ShortDescription = product.ShortDescription,
            Description = product.Description,
            Ingredients = product.Ingredients,
            HowToUse = product.HowToUse,
            Benefits = product.Benefits,

            Brand = product.Brand,
            Size = product.Size,
            CountryOfOrigin = product.CountryOfOrigin,
            Barcode = product.Barcode,

            Price = product.Price,
            CompareAtPrice = product.CompareAtPrice,

            StockQuantity = product.StockQuantity,

            IsEnabled = product.IsEnabled,
            IsFeatured = product.IsFeatured,

            SeoTitle = product.SeoTitle,
            SeoDescription = product.SeoDescription,

            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,

            SubcategoryId = product.SubcategoryId,
            SubcategoryName = product.Subcategory != null ? product.Subcategory.Name : null
        };
    }
}