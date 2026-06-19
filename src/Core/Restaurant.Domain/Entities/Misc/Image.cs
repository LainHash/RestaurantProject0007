using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Misc
{
    public class Image : AuditableEntity
    {
        public string AltText { get; set; } = null!;

        public string Url { get; set; } = null!;           // URL truy cập ảnh
        public string StoragePath { get; set; } = null!;   // đường dẫn vật lý hoặc cloud

        public long FileSize { get; set; }                 // byte
        public string ContentType { get; set; } = null!;   // image/jpeg

        public bool IsPrimary { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
