using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Production;

namespace Restaurant.Domain.Entities.Misc
{
    public class TemporaryContact : Entity
    {
        public string GuestName { get; set; } = string.Empty;
        public string GuestEmai { get; set; } = string.Empty;
        public string GuestPhone { get; set; } = string.Empty;

        public virtual Reservation Reservation { get; set; } = null!;
    }
}
