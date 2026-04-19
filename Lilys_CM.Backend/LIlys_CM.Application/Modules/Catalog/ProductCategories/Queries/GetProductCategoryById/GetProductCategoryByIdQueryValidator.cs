namespace Lilys_CM.Application.Modules.Catalog.ProductCategories.Queries.GetProductCategoryById;

public sealed class GetProductCategoryByIdQueryValidator : AbstractValidator<GetProductCategoryByIdQuery>
{
    public GetProductCategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
