using Lilys_CM.Domain.Entities.Common;
namespace Lilys_CM.Domain.Entities.Tokens { 
    public class PasswordResetTokenEntity : BaseEntity {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool Used { get; set; }
    }
}
