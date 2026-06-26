using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Interior
{
    public class Area : SoftDeleteEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;

        public virtual ICollection<RestaurantTable> RestaurantTables { get; set; } = new List<RestaurantTable>();
    }
}
