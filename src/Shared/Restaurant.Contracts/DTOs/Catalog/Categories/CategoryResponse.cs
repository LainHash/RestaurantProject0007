using Restaurant.Contracts.Common.Models;

namespace Restaurant.Contracts.DTOs.Catalog.Categories
{
    public class CategoryResponse : SoftDeleteDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
