using Lilys_CM.Domain.Entities.Common;

namespace Lilys_CM.Domain.Entities.Catalog;

public class BrandEntity : BaseEntity
{
    public required string Name { get; set; }

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public string? LogoUrl { get; set; }

    public bool IsEnabled { get; set; } = true;

    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}