using Lilys_CM.Domain.Entities.Common;
namespace Lilys_CM.Domain.Entities.Catalog{
    public class ProductVariantEntity : BaseEntity {
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
