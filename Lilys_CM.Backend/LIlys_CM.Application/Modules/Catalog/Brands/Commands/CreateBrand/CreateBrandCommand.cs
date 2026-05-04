namespace Lilys_CM.Application.Modules.Catalog.Brands.Commands.CreateBrand;

public sealed class CreateBrandCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public string? Slug { get; init; }
    public string? Description { get; init; }
    public string? LogoUrl { get; init; }
    public bool IsEnabled { get; init; } = true;
}