namespace Restaurant.Contracts.DTOs.Catalog.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsMadeToOrder { get; set; }

        public Guid CategoryId { get; set; }

        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal StockQuantity { get; set; }
    }
}
