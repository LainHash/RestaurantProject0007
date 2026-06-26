using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Persistence.Configurations.Interior
{
    internal class RestaurantTableConfiguration : IEntityTypeConfiguration<RestaurantTable>
    {
        public void Configure(EntityTypeBuilder<RestaurantTable> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TableNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);

            // Soft delete filter
            builder.HasQueryFilter(x => !x.IsDeleted);

            // Relationships
            builder.HasOne(x => x.Area)
                   .WithMany()
                   .HasForeignKey(x => x.AreaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
