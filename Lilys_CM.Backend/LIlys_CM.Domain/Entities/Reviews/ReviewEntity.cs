using Lilys_CM.Domain.Entities.Common;
using Lilys_CM.Domain.Entities.Identity;
using Lilys_CM.Domain.Entities.Catalog;
namespace Lilys_CM.Domain.Entities.Reviews { 
    public class ReviewEntity : BaseEntity {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
