namespace Lilys_CM.Application.Modules.Catalog.Products.Commands.DeleteProduct;

public sealed class DeleteProductCommand : IRequest<Unit>
{
    public int Id { get; set; }
}