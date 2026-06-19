using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Seeders.Misc
{
    internal class ImageSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Images.AnyAsync())
                return;

            var images = new List<Image>
            {
                new Image
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    FileName = "spring-rolls.jpg",
                    OriginalName = "spring-rolls-orig.jpg",
                    Url = "/images/spring-rolls.jpg",
                    StoragePath = "local/images/spring-rolls.jpg",
                    FileSize = 102400,
                    ContentType = "image/jpeg",
                    IsPrimary = true
                },
                new Image
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    FileName = "pho-beef.jpg",
                    OriginalName = "pho-beef-orig.jpg",
                    Url = "/images/pho-beef.jpg",
                    StoragePath = "local/images/pho-beef.jpg",
                    FileSize = 204800,
                    ContentType = "image/jpeg",
                    IsPrimary = true
                }
            };

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                context.Images.AddRange(images);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }
    }
}
