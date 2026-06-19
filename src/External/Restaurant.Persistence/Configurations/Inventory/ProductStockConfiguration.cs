using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Persistence.Configurations.Inventory
{
    internal class ProductStockConfiguration : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.StockQuantity).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Unit).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.ProductId).IsUnique();

            // Relationships
            // Product relationship is already configured in ProductConfiguration
        }
    }
}
