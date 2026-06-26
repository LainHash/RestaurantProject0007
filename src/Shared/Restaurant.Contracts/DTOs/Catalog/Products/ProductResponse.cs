using Restaurant.Contracts.Common.Models;
using Restaurant.Contracts.DTOs.Catalog.Misc;

namespace Restaurant.Contracts.DTOs.Catalog.Products
{
    public class ProductResponse : SoftDeleteDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsMadeToOrder { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal StockQuantity { get; set; }

        public ImageResponse? PrimaryImage { get; set; }
        public IEnumerable<ImageResponse> Images { get; set; } = Enumerable.Empty<ImageResponse>();

        public bool IsAvailable { get; set; }
    }
}
