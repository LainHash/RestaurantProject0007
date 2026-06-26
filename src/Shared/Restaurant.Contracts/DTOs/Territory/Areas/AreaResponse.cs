using Restaurant.Contracts.Common.Models;

namespace Restaurant.Contracts.DTOs.Territory.Areas
{
    public class AreaResponse : SoftDeleteDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
