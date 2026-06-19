using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Seeders.Catalog
{
    internal class ProductSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Products.AnyAsync())
                return;

            // Get categories to assign products to them
            var categories = await context.Categories.ToListAsync();
            if (!categories.Any()) return;

            var appetizerCat = categories.FirstOrDefault(c => c.Name == "Appetizer") ?? categories.First();
            var mainCourseCat = categories.FirstOrDefault(c => c.Name == "Main Course") ?? categories.First();

            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Name = "Spring Rolls",
                    Description = "Delicious crispy spring rolls",
                    IsAvailable = true,
                    IsMadeToOrder = false,
                    CategoryId = appetizerCat.Id
                },
                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    Name = "Pho Beef",
                    Description = "Traditional Vietnamese beef noodle soup",
                    IsAvailable = true,
                    IsMadeToOrder = true,
                    CategoryId = mainCourseCat.Id
                }
            };

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }
    }
}
