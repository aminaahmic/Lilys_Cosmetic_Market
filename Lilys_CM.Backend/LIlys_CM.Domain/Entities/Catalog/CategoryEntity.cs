using Lilys_CM.Domain.Entities.Common;

namespace Lilys_CM.Domain.Entities.Catalog;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; } = default!;
    public bool IsEnabled { get; set; } = true;

    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}