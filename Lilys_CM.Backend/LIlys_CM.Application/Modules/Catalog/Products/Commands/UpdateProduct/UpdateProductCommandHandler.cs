using Lilys_CM.Application.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
            throw new Lilys_CMNotFoundException("Product not found.");

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

        var normalizedSku = request.Sku.Trim();
        var normalizedSlug = string.IsNullOrWhiteSpace(request.Slug)
            ? SlugGenerator.Generate(request.Name)
            : SlugGenerator.Generate(request.Slug);

        if (string.IsNullOrWhiteSpace(normalizedSlug))
            throw new Lilys_CMConflictException("Slug could not be generated.");

        var skuExists = await _context.Products
            .AnyAsync(p => p.Id != request.Id && p.Sku.ToLower() == normalizedSku.ToLower(), cancellationToken);

        if (skuExists)
            throw new Lilys_CMConflictException("Product with the same SKU already exists.");

        var slugExists = await _context.Products
            .AnyAsync(p => p.Id != request.Id && p.Slug.ToLower() == normalizedSlug.ToLower(), cancellationToken);

        if (slugExists)
            throw new Lilys_CMConflictException("Product with the same slug already exists.");

        if (request.CompareAtPrice.HasValue && request.CompareAtPrice.Value < request.Price)
            throw new Lilys_CMConflictException("Compare-at price must be greater than or equal to price.");

        product.Name = request.Name.Trim();
        product.Sku = normalizedSku;
        product.Slug = normalizedSlug;

        product.ImageUrl = string.IsNullOrWhiteSpace(request.ImageUrl) ? null : request.ImageUrl.Trim();
        product.ShortDescription = string.IsNullOrWhiteSpace(request.ShortDescription) ? null : request.ShortDescription.Trim();
        product.Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim();
        product.Ingredients = string.IsNullOrWhiteSpace(request.Ingredients) ? null : request.Ingredients.Trim();
        product.HowToUse = string.IsNullOrWhiteSpace(request.HowToUse) ? null : request.HowToUse.Trim();
        product.Benefits = string.IsNullOrWhiteSpace(request.Benefits) ? null : request.Benefits.Trim();

        product.Brand = string.IsNullOrWhiteSpace(request.Brand) ? null : request.Brand.Trim();
        product.Size = string.IsNullOrWhiteSpace(request.Size) ? null : request.Size.Trim();
        product.CountryOfOrigin = string.IsNullOrWhiteSpace(request.CountryOfOrigin) ? null : request.CountryOfOrigin.Trim();
        product.Barcode = string.IsNullOrWhiteSpace(request.Barcode) ? null : request.Barcode.Trim();

        product.Price = request.Price;
        product.CompareAtPrice = request.CompareAtPrice;
        product.StockQuantity = request.StockQuantity;

        product.IsEnabled = request.IsEnabled;
        product.IsFeatured = request.IsFeatured;

        product.SeoTitle = string.IsNullOrWhiteSpace(request.SeoTitle) ? null : request.SeoTitle.Trim();
        product.SeoDescription = string.IsNullOrWhiteSpace(request.SeoDescription) ? null : request.SeoDescription.Trim();

        product.CategoryId = request.CategoryId;
        product.SubcategoryId = request.SubcategoryId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}