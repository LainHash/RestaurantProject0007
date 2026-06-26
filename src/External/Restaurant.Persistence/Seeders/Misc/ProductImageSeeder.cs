using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
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

            var xlsxPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "RestaurantData.xlsx");

            if (!File.Exists(xlsxPath))
                throw new FileNotFoundException($"Seed data file not found: {xlsxPath}");

            var records = MiniExcel.Query<ProductImageExcelRecord>(xlsxPath, sheetName: "ProductImages").ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.ProductImages.Add(new ProductImage
                    {
                        Id = record.Id,
                        ProductId = record.ProductId,
                        ImageId = record.ImageId,
                        DisplayOrder = record.DisplayOrder
                    });
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }

        private class ProductImageExcelRecord
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public Guid ImageId { get; set; }
            public bool IsPrimary { get; set; }
            public int DisplayOrder { get; set; }
        }
    }
}
