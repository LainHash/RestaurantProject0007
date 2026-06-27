using Restaurant.Contracts.Common.Models;

namespace Restaurant.Contracts.DTOs.Customers
{
    public class CustomerResponse : SoftDeleteDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public DateOnly DOB { get; set; }
        public bool Gender { get; set; }

        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string CitizenCardId { get; set; } = string.Empty;
    }
}
