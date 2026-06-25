using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.TableManagement
{
    public class RestaurantTable : SoftDeleteEntity
    {
        public string TableNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
