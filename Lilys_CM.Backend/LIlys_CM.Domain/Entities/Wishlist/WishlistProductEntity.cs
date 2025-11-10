using Lilys_CM.Domain.Entities.Common;
using Lilys_CM.Domain.Entities.Catalog;
namespace Lilys_CM.Domain.Entities.Wishlist { 
    public class WishlistProductEntity : BaseEntity {
        public int WishlistId { get; set; }
        public WishlistEntity Wishlist { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}
