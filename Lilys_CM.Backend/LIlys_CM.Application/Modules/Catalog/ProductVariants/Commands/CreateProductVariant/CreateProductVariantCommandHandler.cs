using Lilys_CM.Domain.Catalog;

namespace Lilys_CM.Application.Modules.Catalog.ProductVariants.Commands.CreateProductVariant;

public sealed class CreateProductVariantCommandHandler
    : IRequestHandler<CreateProductVariantCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateProductVariantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateProductVariantCommand request,
        CancellationToken cancellationToken)
    {
        var productExists = await _context.Products
            .AnyAsync(x => x.Id == request.ProductId, cancellationToken);

        if (!productExists)
        {
            throw new Lilys_CMNotFoundException("Product not found.");
        }

        var normalizedOptions = request.Options
            .Select(x => new
            {
                OptionName = x.OptionName.Trim(),
                Value = x.Value.Trim()
            })
            .ToList();

        var hasDuplicates = normalizedOptions
            .GroupBy(x => new
            {
                OptionName = x.OptionName.ToLower(),
                Value = x.Value.ToLower()
            })
            .Any(g => g.Count() > 1);

        if (hasDuplicates)
        {
            throw new Lilys_CMConflictException("Duplicate options are not allowed on the same variant.");
        }

        var variant = new ProductVariantEntity
        {
            ProductId = request.ProductId,
            Price = request.Price,
            Stock = request.Stock
        };

        _context.ProductVariants.Add(variant);
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var optionItem in normalizedOptions)
        {
            var option = await _context.Options
                .FirstOrDefaultAsync(
                    x => x.Name.ToLower() == optionItem.OptionName.ToLower(),
                    cancellationToken);

            if (option is null)
            {
                option = new OptionEntity
                {
                    Name = optionItem.OptionName
                };

                _context.Options.Add(option);
                await _context.SaveChangesAsync(cancellationToken);
            }

            var optionValue = await _context.OptionValueEntities
                .FirstOrDefaultAsync(
                    x =>
                        x.OptionId == option.Id &&
                        x.Value.ToLower() == optionItem.Value.ToLower(),
                    cancellationToken);

            if (optionValue is null)
            {
                optionValue = new OptionValueEntity
                {
                    OptionId = option.Id,
                    Value = optionItem.Value
                };

                _context.OptionValueEntities.Add(optionValue);
                await _context.SaveChangesAsync(cancellationToken);
            }

            var variantOption = new VariantOptionEntity
            {
                VariantId = variant.Id,
                OptionValueId = optionValue.Id
            };

            _context.VariantOptionEntities.Add(variantOption);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return variant.Id;
    }
}