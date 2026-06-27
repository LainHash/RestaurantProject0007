using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Production;

namespace Restaurant.Domain.Entities.Territory
{
    public class RestaurantTable : SoftDeleteEntity
    {
        public string TableNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Status { get; set; } = string.Empty;

        public Guid AreaId { get; set; }

        public virtual Area Area { get; set; } = null!;
        public virtual IEnumerable<Reservation> Reservations { get; set; } = Enumerable.Empty<Reservation>();
    }
}
