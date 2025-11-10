using Lilys_CM.Domain.Entities.Catalog;
using Lilys_CM.Domain.Entities.Common;
namespace Lilys_CM.Domain.Entities.Orders { 
    public class OrderItemEntity : BaseEntity {
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public int VariantId { get; set; }
        public ProductVariantEntity Variant { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
