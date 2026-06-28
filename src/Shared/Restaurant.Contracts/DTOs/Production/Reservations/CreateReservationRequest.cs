namespace Restaurant.Contracts.DTOs.Production.Reservations
{
    public class CreateReservationRequest
    {
        public DateTime ReservationTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Note { get; set; }

        public Guid? CustomerId { get; set; }

        public string? GuestName { get; set; } = string.Empty;
        public string? GuestEmail { get; set; } = string.Empty;
        public string? GuestPhone { get; set; } = string.Empty;

        public Guid? RestaurantTableId { get; set; }
        public string? TableType { get; set; }
    }
}
