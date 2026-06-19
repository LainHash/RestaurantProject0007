using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Seeders.Misc
{
    internal class ProductImageSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductImages.AnyAsync())
                return;

            var products = await context.Products.ToListAsync();
            var images = await context.Images.ToListAsync();

            if (!products.Any() || !images.Any()) return;

            var productImages = new List<ProductImage>();

            // Assign Image 1 to Product 1
            if (products.Count > 0 && images.Count > 0)
            {
                productImages.Add(new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ProductId = products[0].Id,
                    ImageId = images[0].Id,
                    Url = images[0].Url,
                    IsPrimary = true,
                    DisplayOrder = 1
                });
            }

            // Assign Image 2 to Product 2
            if (products.Count > 1 && images.Count > 1)
            {
                productImages.Add(new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ProductId = products[1].Id,
                    ImageId = images[1].Id,
                    Url = images[1].Url,
                    IsPrimary = true,
                    DisplayOrder = 1
                });
            }

            if (!productImages.Any()) return;

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                context.ProductImages.AddRange(productImages);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }
    }
}
