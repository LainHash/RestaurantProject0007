using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Misc;

namespace Restaurant.Persistence.Configurations.Misc
{
    internal class TemporaryContactConfiguration : IEntityTypeConfiguration<TemporaryContact>
    {
        public void Configure(EntityTypeBuilder<TemporaryContact> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.GuestName).HasMaxLength(150).IsRequired();
            builder.Property(x => x.GuestEmai).HasMaxLength(150).IsRequired();
            builder.Property(x => x.GuestPhone).HasMaxLength(20).IsRequired();
        }
    }
}
