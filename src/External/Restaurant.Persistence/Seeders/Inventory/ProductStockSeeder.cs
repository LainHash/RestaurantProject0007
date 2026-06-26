using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
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

            var xlsxPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "RestaurantData.xlsx");

            if (!File.Exists(xlsxPath))
                throw new FileNotFoundException($"Seed data file not found: {xlsxPath}");

            var records = MiniExcel.Query<ProductStockExcelRecord>(xlsxPath, sheetName: "ProductStocks").ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.ProductStocks.Add(new ProductStock
                    {
                        Id = record.Id,
                        ProductId = record.ProductId,
                        UnitPrice = record.Price,
                        Unit = record.Unit ?? string.Empty,
                        StockQuantity = record.Quantity
                    });
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }

        private class ProductStockExcelRecord
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public decimal Price { get; set; }
            public string? Unit { get; set; }
            public decimal Quantity { get; set; }
        }
    }
}
