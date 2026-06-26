using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
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

            var xlsxPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "RestaurantData.xlsx");

            if (!File.Exists(xlsxPath))
                throw new FileNotFoundException($"Seed data file not found: {xlsxPath}");

            var records = MiniExcel.Query<ProductExcelRecord>(xlsxPath, sheetName: "Products").ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.Products.Add(new Product
                    {
                        Id = record.Id,
                        Name = record.Name,
                        Description = record.Description ?? string.Empty,
                        IsAvailable = record.IsAvailable,
                        CategoryId = record.CategoryId,
                        IsMadeToOrder = record.IsMadeToOrder
                    });
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }

        private class ProductExcelRecord
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public bool IsAvailable { get; set; }
            public Guid CategoryId { get; set; }
            public bool IsMadeToOrder { get; set; }
        }
    }
}
