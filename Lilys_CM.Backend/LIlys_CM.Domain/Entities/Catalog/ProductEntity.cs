using Lilys_CM.Domain.Entities.Common;
namespace Lilys_CM.Domain.Entities.Catalog { 
    public class ProductEntity : BaseEntity {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SubcategoryId { get; set; }
        public SubcategoryEntity Subcategory { get; set; }
    }
}
