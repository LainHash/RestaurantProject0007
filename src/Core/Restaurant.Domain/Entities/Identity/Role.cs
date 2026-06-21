using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Identity
{
    public class Role : SoftDeleteEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
