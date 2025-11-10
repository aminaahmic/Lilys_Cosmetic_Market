using System;
using Lilys_CM.Domain.Entities.Common;
namespace Lilys_CM.Domain.Entities.Catalog {
    public class CategoryEntity : BaseEntity {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

    }
}

