using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Production;

namespace Restaurant.Persistence.Configurations.Production
{
    internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ReservationTime).IsRequired();
            builder.Property(x => x.NumberOfGuests).IsRequired();
            builder.Property(x => x.Status).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);

            // Relationships
            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.TemporaryContact)
                   .WithOne(x => x.Reservation)
                   .HasForeignKey<Reservation>(x => x.TemporaryContactId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.RestaurantTable)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.RestaurantTableId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Soft delete filter
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
