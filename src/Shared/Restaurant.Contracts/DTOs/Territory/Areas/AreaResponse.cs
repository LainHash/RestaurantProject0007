using Restaurant.Contracts.Common.Models;
using Restaurant.Contracts.DTOs.Territory.RestaurantTables;

namespace Restaurant.Contracts.DTOs.Territory.Areas
{
    public class AreaResponse : SoftDeleteDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Type {  get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;

        public ICollection<RestaurantTableResponse> RestaurantTables { get; set; } = new List<RestaurantTableResponse>();
    }
}
