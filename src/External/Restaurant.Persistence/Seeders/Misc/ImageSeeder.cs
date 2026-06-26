using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
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

            var xlsxPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "RestaurantData.xlsx");

            if (!File.Exists(xlsxPath))
                throw new FileNotFoundException($"Seed data file not found: {xlsxPath}");

            var records = MiniExcel.Query<ImageExcelRecord>(xlsxPath, sheetName: "Images").ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.Images.Add(new Image
                    {
                        Id = record.Id,
                        AltText = record.AltText ?? string.Empty,
                        ContentType = record.ContentType ?? string.Empty,
                        IsPrimary = record.IsPrimary,
                        Url = record.Url ?? string.Empty,
                        StoragePath = record.StoragePath ?? string.Empty,
                        FileSize = record.FileSize
                    });
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }

        private class ImageExcelRecord
        {
            public Guid Id { get; set; }
            public string? AltText { get; set; }
            public string? ContentType { get; set; }
            public decimal FileSize { get; set; }
            public bool IsPrimary { get; set; }
            public string? Url { get; set; }
            public string? StoragePath { get; set; }
        }
    }
}
