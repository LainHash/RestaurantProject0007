using Restaurant.Contracts.Common.Models;
using Restaurant.Contracts.DTOs.Customers;
using Restaurant.Contracts.DTOs.Misc;
using Restaurant.Contracts.DTOs.Territory.RestaurantTables;

namespace Restaurant.Contracts.DTOs.Production
{
    public class ReservationResponse : SoftDeleteDTO
    {
        public DateTime ReservationTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Note { get; set; }

        public TemporaryContactResponse? TemporaryContact { get; set; } 
        public CustomerResponse? Customer { get; set; }
        public RestaurantTableResponse RestaurantTable { get; set; } = null!;
    }
}
