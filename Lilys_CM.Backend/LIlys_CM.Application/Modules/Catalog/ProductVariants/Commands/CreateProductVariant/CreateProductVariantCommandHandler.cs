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
        x.OptionId,
        Value = x.Value.Trim()
    })
    .ToList();

        var hasDuplicates = normalizedOptions
            .GroupBy(x => new
            {
                x.OptionId,
                Value = x.Value.ToLower()
            })
            .Any(g => g.Count() > 1);

        if (hasDuplicates)
        {
            throw new Lilys_CMConflictException("Duplicate options are not allowed on the same variant.");
        }

        var optionIds = normalizedOptions
            .Select(x => x.OptionId)
            .Distinct()
            .ToList();

        var existingOptionIds = await _context.Options
            .Where(x => optionIds.Contains(x.Id) && !x.IsDeleted)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        if (existingOptionIds.Count != optionIds.Count)
        {
            throw new Lilys_CMNotFoundException("One or more options were not found.");
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
            var optionValue = await _context.OptionValueEntities
                .FirstOrDefaultAsync(
                    x =>
                        x.OptionId == optionItem.OptionId &&
                        x.Value.ToLower() == optionItem.Value.ToLower() &&
                        !x.IsDeleted,
                    cancellationToken);

            if (optionValue is null)
            {
                optionValue = new OptionValueEntity
                {
                    OptionId = optionItem.OptionId,
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