using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Guests;

namespace Restaurant.Domain.Entities.Identity
{
    public class User : SoftDeleteEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public string? VerificationCode { get; set; }
        public DateTime? VerificationCodeExpiresAt { get; set; }
        public Guid RoleId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
