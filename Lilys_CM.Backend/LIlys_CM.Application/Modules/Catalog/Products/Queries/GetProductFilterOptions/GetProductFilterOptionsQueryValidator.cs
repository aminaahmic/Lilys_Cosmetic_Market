namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductFilterOptions;

public sealed class GetProductFilterOptionsQueryValidator : AbstractValidator<GetProductFilterOptionsQuery>
{
    public GetProductFilterOptionsQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .When(x => x.CategoryId.HasValue);
    }
}
