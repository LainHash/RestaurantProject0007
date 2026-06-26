using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Interior;

namespace Restaurant.Persistence.Configurations.Interior
{
    internal class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);

            // Soft delete filter
            builder.HasQueryFilter(x => !x.IsDeleted);

            // Relationships
            builder.HasMany(x => x.RestaurantTables)
                   .WithOne(x => x.Area)
                   .HasForeignKey(x => x.AreaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
