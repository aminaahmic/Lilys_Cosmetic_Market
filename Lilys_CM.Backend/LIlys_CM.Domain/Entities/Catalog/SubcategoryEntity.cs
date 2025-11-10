using Lilys_CM.Domain.Entities.Common;
namespace Lilys_CM.Domain.Entities.Catalog { 
    public class SubcategoryEntity : BaseEntity {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}
