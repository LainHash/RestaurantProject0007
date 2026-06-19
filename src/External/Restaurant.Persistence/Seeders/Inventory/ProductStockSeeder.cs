using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Seeders.Inventory
{
    internal class ProductStockSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductStocks.AnyAsync())
                return;

            var products = await context.Products.ToListAsync();
            if (!products.Any()) return;

            var stocks = new List<ProductStock>();

            foreach (var product in products)
            {
                stocks.Add(new ProductStock
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    UnitPrice = 10.0m,
                    StockQuantity = 100m,
                    Unit = "Portion"
                });
            }

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                context.ProductStocks.AddRange(stocks);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }
    }
}
