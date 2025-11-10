using System;
using System.ComponentModel.DataAnnotations;

namespace Lilys_CM.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAtUtc { get; set; }

        public bool IsDeleted { get; set; } = false;

        public void MarkUpdated()
        {
            ModifiedAtUtc = DateTime.UtcNow;
        }

        public void MarkDeleted()
        {
            IsDeleted = true;
            ModifiedAtUtc = DateTime.UtcNow;
        }
    }
}
