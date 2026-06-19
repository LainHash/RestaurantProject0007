using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Misc;

namespace Restaurant.Persistence.Configurations.Misc
{
    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FileName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.OriginalName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Url).IsRequired().HasMaxLength(500);
            builder.Property(x => x.StoragePath).IsRequired().HasMaxLength(500);
            builder.Property(x => x.ContentType).IsRequired().HasMaxLength(100);
        }
    }
}
