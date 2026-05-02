public sealed class GetSubcategoriesByCategoryQuery : IRequest<List<SubcategoryDto>>
{
    public int CategoryId { get; init; }
}