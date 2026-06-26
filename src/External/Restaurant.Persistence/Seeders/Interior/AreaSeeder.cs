using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Interior;
using Restaurant.Persistence.Contexts;
using System.Globalization;

namespace Restaurant.Persistence.Seeders.Interior
{
    internal class AreaSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Areas.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "areas.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<AreaCsvRecord>().ToList();

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

        private class AreaCsvRecord
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string Status { get; set; } = string.Empty;
        }
    }
}
