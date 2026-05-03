namespace Lilys_CM.Application.Modules.Catalog.ProductVariants.Queries.GetProductVariants;

public sealed class GetProductVariantsQueryHandler
    : IRequestHandler<GetProductVariantsQuery, List<ProductVariantDto>>
{
    private readonly IAppDbContext _context;

    public GetProductVariantsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductVariantDto>> Handle(
        GetProductVariantsQuery request,
        CancellationToken cancellationToken)
    {
        var productExists = await _context.Products
            .AnyAsync(x => x.Id == request.ProductId, cancellationToken);

        if (!productExists)
        {
            throw new Lilys_CMNotFoundException("Product not found.");
        }

        return await _context.ProductVariants
            .AsNoTracking()
            .Where(x => x.ProductId == request.ProductId)
            .OrderBy(x => x.Id)
            .Select(x => new ProductVariantDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Price = x.Price,
                Stock = x.Stock,

                Options = _context.VariantOptionEntities
                    .Where(vo => vo.VariantId == x.Id)
                    .Select(vo => new ProductVariantOptionDto
                    {
                        OptionId = vo.OptionValue.OptionId,
                        OptionName = vo.OptionValue.Option.Name,
                        OptionValueId = vo.OptionValueId,
                        Value = vo.OptionValue.Value
                    })
                    .ToList()
            })
            .ToListAsync(cancellationToken);
    }
}