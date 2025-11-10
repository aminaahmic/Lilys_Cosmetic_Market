using Lilys_CM.Domain.Entities.Common;
using Lilys_CM.Domain.Entities.Orders;
using Lilys_CM.Domain.Entities.Localization;
namespace Lilys_CM.Domain.Entities.Payments { 
    public class PaymentTransactionEntity : BaseEntity {
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public string StripeChargeId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentStatusId { get; set; }
        public PaymentStatusEntity PaymentStatus { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyEntity Currency { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethodEntity PaymentMethod { get; set; }
    }
}
