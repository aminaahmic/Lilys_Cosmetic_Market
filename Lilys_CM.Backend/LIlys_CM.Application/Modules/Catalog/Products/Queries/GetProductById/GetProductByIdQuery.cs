namespace Lilys_CM.Application.Modules.Catalog.Products.Queries.GetProductById;

public sealed class GetProductByIdQuery : IRequest<GetProductByIdDto>
{
    public int Id { get; set; }
}