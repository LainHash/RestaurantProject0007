using Restaurant.Contracts.DTOs.Catalog.Misc;

namespace Restaurant.Contracts.DTOs.Catalog.Products
{
    public class ProductResponse
    {
        public Guid Id { get; set; }

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

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
