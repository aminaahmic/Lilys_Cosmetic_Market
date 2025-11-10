using Lilys_CM.Domain.Catalog;
using Lilys_CM.Domain.Entities.Common;
namespace Lilys_CM.Domain.Entities.Catalog { 
    public class OptionValueEntity : BaseEntity {
        public int OptionId { get; set; }
        public OptionEntity Option { get; set; }
        public string Value { get; set; }
    }
}
