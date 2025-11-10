using Lilys_CM.Domain.Entities.Common;
using Lilys_CM.Domain.Entities.Identity;
namespace Lilys_CM.Domain.Entities.Wishlist { 
    public class WishlistEntity : BaseEntity {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public string Name { get; set; }
    }
}
