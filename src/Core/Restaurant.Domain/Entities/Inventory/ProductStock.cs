using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Inventory
{
    public class ProductStock : Entity
    {
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal StockQuantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
