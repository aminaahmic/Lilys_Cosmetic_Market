using Lilys_CM.Domain.Entities.Identity;
using Lilys_CM.Domain.Entities.Common;
namespace Lilys_CM.Domain.Entities.Notifications {
    public class NotificationEntity : BaseEntity {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public int? NotificationTypeId { get; set; }
        public NotificationTypeEntity NotificationType { get; set; }
    }
}
