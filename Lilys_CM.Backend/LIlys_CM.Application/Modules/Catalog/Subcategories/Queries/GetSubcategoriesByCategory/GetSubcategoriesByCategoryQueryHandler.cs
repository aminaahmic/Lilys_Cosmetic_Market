public sealed class GetSubcategoriesByCategoryQueryHandler 
    : IRequestHandler<GetSubcategoriesByCategoryQuery, List<SubcategoryDto>>
{
    private readonly IAppDbContext _context;

    public GetSubcategoriesByCategoryQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SubcategoryDto>> Handle(
        GetSubcategoriesByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Subcategories
            .Where(x => x.CategoryId == request.CategoryId)
            .Select(x => new SubcategoryDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);
    }
}