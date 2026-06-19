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

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "product_stocks.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<ProductStockCsvRecord>().ToList();

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

        private class ProductStockCsvRecord
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public decimal Price { get; set; }
            public string? Unit { get; set; }
            public decimal Quantity { get; set; }
        }
    }
}
