using System;
using System.Collections.Generic;
using Lilys_CM.Domain.Entities.Common;
using Lilys_CM.Domain.Entities.Tokens;

namespace Lilys_CM.Domain.Entities.Identity
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!; // hashirana lozinka

        // 🔹 Role flags
        public bool IsAdmin { get; set; }
        public bool IsCustomer { get; set; }

        public bool IsEnabled { get; set; }

        // 🔹 Opcionalno – za refresh tokene
        public ICollection<RefreshTokenEntity>? RefreshTokens { get; set; }
    }
}
