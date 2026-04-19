using Lilys_CM.Domain.Entities.Common;

namespace Lilys_CM.Domain.Entities.Catalog
{
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Subcategory { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsEnabled { get; set; } = true;

        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; } = default!;

        public ICollection<ProductStockMovementEntity> StockMovements { get; set; } =
            new List<ProductStockMovementEntity>();
    }
}
