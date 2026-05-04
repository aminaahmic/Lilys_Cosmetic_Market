using Lilys_CM.Application.Common.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
            throw new Lilys_CMNotFoundException("Category not found.");

        if (request.SubcategoryId.HasValue)
        {
            var subcategory = await _context.Subcategories
                .FirstOrDefaultAsync(s => s.Id == request.SubcategoryId.Value, cancellationToken);

            if (subcategory is null)
                throw new Lilys_CMNotFoundException("Subcategory not found.");

            if (subcategory.CategoryId != request.CategoryId)
                throw new Lilys_CMConflictException("Selected subcategory does not belong to the selected category.");
        }
        BrandEntity? brand = null;

        if (request.BrandId.HasValue)
        {
            brand = await _context.Brands
                .FirstOrDefaultAsync(x => x.Id == request.BrandId.Value, cancellationToken);

            if (brand is null)
            {
                throw new Lilys_CMNotFoundException("Brand not found.");
            }

            if (!brand.IsEnabled)
            {
                throw new Lilys_CMConflictException("Selected brand is not active.");
            }
        }
        var normalizedSku = request.Sku.Trim();
        var normalizedSlug = string.IsNullOrWhiteSpace(request.Slug)
            ? SlugGenerator.Generate(request.Name)
            : SlugGenerator.Generate(request.Slug);

        if (string.IsNullOrWhiteSpace(normalizedSlug))
            throw new Lilys_CMConflictException("Slug could not be generated.");

        var skuExists = await _context.Products
            .AnyAsync(p => p.Sku.ToLower() == normalizedSku.ToLower(), cancellationToken);

        if (skuExists)
            throw new Lilys_CMConflictException("Product with the same SKU already exists.");

        var slugExists = await _context.Products
            .AnyAsync(p => p.Slug.ToLower() == normalizedSlug.ToLower(), cancellationToken);

        if (slugExists)
            throw new Lilys_CMConflictException("Product with the same slug already exists.");

        if (request.CompareAtPrice.HasValue && request.CompareAtPrice.Value < request.Price)
            throw new Lilys_CMConflictException("Compare-at price must be greater than or equal to price.");

        var product = new ProductEntity
        {
            Name = request.Name.Trim(),
            Sku = normalizedSku,
            Slug = normalizedSlug,

            ImageUrl = string.IsNullOrWhiteSpace(request.ImageUrl) ? null : request.ImageUrl.Trim(),
            ShortDescription = string.IsNullOrWhiteSpace(request.ShortDescription) ? null : request.ShortDescription.Trim(),
            Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim(),
            Ingredients = string.IsNullOrWhiteSpace(request.Ingredients) ? null : request.Ingredients.Trim(),
            HowToUse = string.IsNullOrWhiteSpace(request.HowToUse) ? null : request.HowToUse.Trim(),
            Benefits = string.IsNullOrWhiteSpace(request.Benefits) ? null : request.Benefits.Trim(),

            Brand = brand?.Name ?? (string.IsNullOrWhiteSpace(request.Brand) ? null : request.Brand.Trim()),
            BrandId = request.BrandId,
            Size = string.IsNullOrWhiteSpace(request.Size) ? null : request.Size.Trim(),
            CountryOfOrigin = string.IsNullOrWhiteSpace(request.CountryOfOrigin) ? null : request.CountryOfOrigin.Trim(),
            Barcode = string.IsNullOrWhiteSpace(request.Barcode) ? null : request.Barcode.Trim(),

            Price = request.Price,
            CompareAtPrice = request.CompareAtPrice,
            StockQuantity = request.StockQuantity,

            IsEnabled = request.IsEnabled,
            IsFeatured = request.IsFeatured,

            SeoTitle = string.IsNullOrWhiteSpace(request.SeoTitle) ? null : request.SeoTitle.Trim(),
            SeoDescription = string.IsNullOrWhiteSpace(request.SeoDescription) ? null : request.SeoDescription.Trim(),

            CategoryId = request.CategoryId,
            SubcategoryId = request.SubcategoryId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}