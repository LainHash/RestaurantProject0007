using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Seeders.Territory
{
    internal class AreaSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Areas.AnyAsync())
                return;

            var xlsxPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "RestaurantData.xlsx");

            if (!File.Exists(xlsxPath))
                throw new FileNotFoundException($"Seed data file not found: {xlsxPath}");

            var records = MiniExcel.Query<AreaExcelRecord>(xlsxPath, sheetName: "Areas").ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.Areas.Add(new Area
                    {
                        Id = record.Id,
                        Name = record.Name,
                        Description = record.Description ?? string.Empty,
                        Status = record.Status
                    });
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }

        private class AreaExcelRecord
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string Status { get; set; } = string.Empty;
        }
    }
}
