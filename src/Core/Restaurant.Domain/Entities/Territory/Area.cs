using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Territory
{
    public class Area : SoftDeleteEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public virtual ICollection<RestaurantTable> RestaurantTables { get; set; } = new List<RestaurantTable>();
    }
}
