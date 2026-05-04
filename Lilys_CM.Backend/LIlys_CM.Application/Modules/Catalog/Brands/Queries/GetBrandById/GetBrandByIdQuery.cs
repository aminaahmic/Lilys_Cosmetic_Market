using Lilys_CM.Application.Modules.Catalog.Brands.Common;

namespace Lilys_CM.Application.Modules.Catalog.Brands.Queries.GetBrandById;

public sealed class GetBrandByIdQuery : IRequest<BrandDto>
{
    public int Id { get; init; }
}