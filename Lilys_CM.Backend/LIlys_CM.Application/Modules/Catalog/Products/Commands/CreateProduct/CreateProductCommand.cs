namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.CreateProduct;

public sealed class CreateProductCommand : IRequest<int>
{
    public string Name { get; init; } = default!;
    public string? Description { get; init; }
    public string? Brand { get; init; }
    public string? Subcategory { get; init; }
    public decimal Price { get; init; }
    public bool IsEnabled { get; init; } = true;
    public int CategoryId { get; init; }
}
