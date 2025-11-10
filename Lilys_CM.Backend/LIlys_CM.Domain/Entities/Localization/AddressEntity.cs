using Lilys_CM.Domain.Entities.Common;
using Lilys_CM.Domain.Entities.Identity;
using Lilys_CM.Domain.Localization;
namespace Lilys_CM.Domain.Entities.Localization { 
    public class AddressEntity : BaseEntity {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int CountryId { get; set; }
        public CountryEntity Country { get; set; }
    }
}
