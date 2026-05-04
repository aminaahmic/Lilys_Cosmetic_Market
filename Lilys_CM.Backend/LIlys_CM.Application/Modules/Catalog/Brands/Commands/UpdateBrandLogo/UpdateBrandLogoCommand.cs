namespace Lilys_CM.Application.Modules.Catalog.Brands.Commands.UpdateBrandLogo;

public sealed class UpdateBrandLogoCommand : IRequest
{
    public int Id { get; init; }
    public string LogoUrl { get; init; } = string.Empty;
}