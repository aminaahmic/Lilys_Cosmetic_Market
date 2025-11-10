using Lilys_CM.Domain.Entities.Common;
using Lilys_CM.Domain.Entities.Localization;
namespace Lilys_CM.Domain.Localization {
    public class CountryEntity : BaseEntity {
        public string Name { get; set; }
        public string IsoCode2 { get; set; }
        public string IsoCode3 { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyEntity Currency { get; set; }
    }
}