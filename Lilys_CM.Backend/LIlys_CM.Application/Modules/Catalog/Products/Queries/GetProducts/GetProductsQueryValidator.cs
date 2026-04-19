namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProducts;

public sealed class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.PriceMin)
            .GreaterThanOrEqualTo(0)
            .When(x => x.PriceMin.HasValue);

        RuleFor(x => x.PriceMax)
            .GreaterThanOrEqualTo(0)
            .When(x => x.PriceMax.HasValue);

        RuleFor(x => x)
            .Must(x => !x.PriceMin.HasValue || !x.PriceMax.HasValue || x.PriceMin <= x.PriceMax)
            .WithMessage("Price min cannot be greater than price max.");

        RuleFor(x => x.Brand)
            .MaximumLength(100);

        RuleFor(x => x.Subcategory)
            .MaximumLength(80);
    }
}
