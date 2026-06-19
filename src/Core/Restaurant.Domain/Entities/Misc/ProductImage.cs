using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Misc
{
    public class ProductImage : Entity
    {
        public string Url { get; set; } = null!;
        public bool IsPrimary { get; set; }

        public int DisplayOrder { get; set; }

        public Guid ProductId { get; set; }
        public Guid ImageId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Image Image { get; set; } = null!;
    }
}
