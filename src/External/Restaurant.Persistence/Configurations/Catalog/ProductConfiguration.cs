using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Persistence.Configurations.Catalog
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(1000);

            // Soft delete filter
            //builder.HasQueryFilter(x => !x.IsDeleted);

            // Relationships
            // Category relationship is already configured in CategoryConfiguration

            builder.HasOne(x => x.ProductStock)
                   .WithOne(x => x.Product)
                   .HasForeignKey<Domain.Entities.Inventory.ProductStock>(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ProductImages)
                   .WithOne(x => x.Product)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
