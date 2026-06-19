using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Misc;

namespace Restaurant.Persistence.Configurations.Misc
{
    internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Image)
                   .WithMany(x => x.ProductImages)
                   .HasForeignKey(x => x.ImageId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Product relationship is already configured in ProductConfiguration
        }
    }
}
