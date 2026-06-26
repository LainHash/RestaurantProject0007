using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Entities.Customers
{
    public class Customer : SoftDeleteEntity
    {
        public Guid UserId { get; set; }
        public Guid? PIId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual PersonalInformation? PersonalInformation { get; set; }
    }
}
