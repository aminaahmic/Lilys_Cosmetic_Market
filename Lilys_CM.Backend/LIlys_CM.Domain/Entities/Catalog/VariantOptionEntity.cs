using Lilys_CM.Domain.Entities.Common;
namespace Lilys_CM.Domain.Entities.Catalog { 
    public class VariantOptionEntity : BaseEntity {
        public int VariantId { get; set; }
        public ProductVariantEntity Variant { get; set; }
        public int OptionValueId { get; set; }
        public OptionValueEntity OptionValue { get; set; }
    }
}
