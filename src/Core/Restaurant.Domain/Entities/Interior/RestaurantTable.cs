using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Interior
{
    public class RestaurantTable : SoftDeleteEntity
    {
        public string TableNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Status { get; set; } = string.Empty;

        public Guid AreaId { get; set; }

        public virtual Area Area { get; set; } = null!;
    }
}
