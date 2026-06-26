using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Seeders.Territory
{
    internal class RestaurantTableSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.RestaurantTables.AnyAsync())
                return;

            var xlsxPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "RestaurantData.xlsx");

            if (!File.Exists(xlsxPath))
                throw new FileNotFoundException($"Seed data file not found: {xlsxPath}");

            var records = MiniExcel.Query<RestaurantTableExcelRecord>(xlsxPath, sheetName: "RestaurantTables").ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.RestaurantTables.Add(new RestaurantTable
                    {
                        Id = record.Id,
                        TableNumber = record.TableNumber,
                        Capacity = record.Capacity,
                        Status = record.Status,
                        AreaId = record.AreaId
                    });
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }

        private class RestaurantTableExcelRecord
        {
            public Guid Id { get; set; }
            public string TableNumber { get; set; } = string.Empty;
            public int Capacity { get; set; }
            public string Status { get; set; } = string.Empty;
            public Guid AreaId { get; set; }
        }
    }
}
