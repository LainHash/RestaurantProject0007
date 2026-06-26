using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Interior;
using Restaurant.Persistence.Contexts;
using System.Globalization;

namespace Restaurant.Persistence.Seeders.Interior
{
    internal class RestaurantTableSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.RestaurantTables.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "restaurant_tables.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<RestaurantTableCsvRecord>().ToList();

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

        private class RestaurantTableCsvRecord
        {
            public Guid Id { get; set; }
            public string TableNumber { get; set; } = string.Empty;
            public int Capacity { get; set; }
            public string Status { get; set; } = string.Empty;
            public Guid AreaId { get; set; }
        }
    }
}
