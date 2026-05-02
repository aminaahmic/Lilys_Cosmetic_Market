using Lilys_CM.Domain.Entities.Common;

namespace Lilys_CM.Domain.Entities.Catalog;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; } = default!;
    public bool IsEnabled { get; set; } = true;
    public string? Icon { get; set; }

    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    public ICollection<SubcategoryEntity> Subcategories { get; set; } = new List<SubcategoryEntity>();
}