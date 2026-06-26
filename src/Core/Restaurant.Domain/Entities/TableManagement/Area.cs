using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.TableManagement
{
    public class Area : SoftDeleteEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;

        public virtual RestaurantTable RestaurantTable { get; set; } = null!;
    }
}
