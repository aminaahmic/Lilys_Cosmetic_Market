using Lilys_CM.Domain.Entities.Common;
using Lilys_CM.Domain.Entities.Orders;
using Lilys_CM.Domain.Entities.Identity;
using Lilys_CM.Domain.Entities.Localization;
namespace Lilys_CM.Domain.Entities.Orders { 
    public class OrderEntity : BaseEntity {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatusEntity OrderStatus { get; set; }
        public int? ShippingAddressId { get; set; }
        public AddressEntity ShippingAddress { get; set; }
    }
}
