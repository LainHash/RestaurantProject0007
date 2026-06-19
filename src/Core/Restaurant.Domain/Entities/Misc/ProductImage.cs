using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Misc
{
    public class ProductImage : Entity
    {
        public Guid ProductId { get; set; }
        //public Product Product { get; set; } = null!;

        public string Url { get; set; } = null!;
        public bool IsPrimary { get; set; }

        public int DisplayOrder { get; set; }
    }
}
