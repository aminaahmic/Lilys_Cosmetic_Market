public class SubcategoryEntity : BaseEntity
{
    public string Name { get; set; } = default!;
    public bool IsEnabled { get; set; } = true;

    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = default!;

    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}