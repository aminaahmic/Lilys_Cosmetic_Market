namespace Lilys_CM.Application.Modules.Catalog.Brands.Commands.DeleteBrand;

public sealed class DeleteBrandCommand : IRequest
{
    public int Id { get; init; }
}