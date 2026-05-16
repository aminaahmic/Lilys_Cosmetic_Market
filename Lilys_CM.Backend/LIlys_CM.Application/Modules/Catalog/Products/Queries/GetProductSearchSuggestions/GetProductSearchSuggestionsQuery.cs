namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductSearchSuggestions;

public sealed class GetProductSearchSuggestionsQuery : IRequest<List<string>>
{
    public string? Search { get; init; }
    public int Take { get; init; } = 8;
}