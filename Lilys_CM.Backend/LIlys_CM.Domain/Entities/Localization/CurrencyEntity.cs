using Lilys_CM.Domain.Entities.Common;
namespace Lilys_CM.Domain.Entities.Localization { 
    public class CurrencyEntity : BaseEntity {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public int Decimals { get; set; }
    }
}
