using Lilys_CM.Application.Modules.Catalog.Brands.Common;
using MediatR;

namespace Lilys_CM.Application.Modules.Catalog.Brands.Queries.GetBrands;

public sealed class GetBrandsQuery : IRequest<List<BrandDto>>
{
    public bool? OnlyEnabled { get; init; }
    public string? Search { get; init; }
}