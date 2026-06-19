namespace Restaurant.Contracts.DTOs.Catalog.Products
{
    public class UpdateProductRequest : CreateProductRequest
    {
        public bool IsAvailable { get; set; }
    }
}
