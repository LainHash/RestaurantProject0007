using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Misc;

namespace Restaurant.Domain.Entities.Catalog
{
    public class Product : SoftDeleteEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description {  get; set; }
        public bool IsMadeToOrder { get; set; }
        public bool IsAvailable { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ProductStock ProductStock { get; set; } = null!;
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
