namespace Lilys_CM.Application.Modules.Catalog.Brands.Common;

public sealed class BrandDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Slug { get; init; }
    public string? Description { get; init; }
    public string? LogoUrl { get; init; }
    public bool IsEnabled { get; init; }
    public int ProductsCount { get; init; }
}