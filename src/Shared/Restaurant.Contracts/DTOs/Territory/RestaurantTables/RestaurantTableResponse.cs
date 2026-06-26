using Restaurant.Contracts.Common.Models;

namespace Restaurant.Contracts.DTOs.Territory.RestaurantTables
{
    public class RestaurantTableResponse : SoftDeleteDTO
    {
        public string TableNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
