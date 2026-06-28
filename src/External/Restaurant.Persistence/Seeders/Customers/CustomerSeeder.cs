using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
using Restaurant.Domain.Entities.Guests;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Seeders.Customers
{
    internal class CustomerSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Customers.AnyAsync())
                return;

            var xlsxPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "RestaurantData.xlsx");

            if (!File.Exists(xlsxPath))
                throw new FileNotFoundException($"Seed data file not found: {xlsxPath}");

            var records = MiniExcel.Query<CustomerExcelRecord>(xlsxPath, sheetName: "Customers").ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.Customers.Add(new Customer
                    {
                        Id = record.Id,
                        UserId = record.UserId,
                        PIId = record.PIId,
                    });
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }

        private class CustomerExcelRecord
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public Guid PIId { get; set; }
        }
    }
}
