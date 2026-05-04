using Lilys_CM.Domain.Catalog;

namespace Lilys_CM.Application.Modules.Catalog.ProductVariants.Commands.UpdateProductVariant;

public sealed class UpdateProductVariantCommandHandler
    : IRequestHandler<UpdateProductVariantCommand>
{
    private readonly IAppDbContext _context;

    public UpdateProductVariantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateProductVariantCommand request,
        CancellationToken cancellationToken)
    {
        var variant = await _context.ProductVariants
            .FirstOrDefaultAsync(
                x => x.Id == request.VariantId && x.ProductId == request.ProductId,
                cancellationToken);

        if (variant is null)
        {
            throw new Lilys_CMNotFoundException("Product variant not found.");
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

        variant.Price = request.Price;
        variant.Stock = request.Stock;

        var existingVariantOptions = await _context.VariantOptionEntities
            .Where(x => x.VariantId == variant.Id)
            .ToListAsync(cancellationToken);

        _context.VariantOptionEntities.RemoveRange(existingVariantOptions);

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
    }
}