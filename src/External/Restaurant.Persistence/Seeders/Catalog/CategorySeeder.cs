using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.Contexts;
using System.Globalization;

namespace Restaurant.Persistence.Seeders.Catalog
{
    internal class CategorySeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Categories.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "categories.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<CategoryCsvRecord>().ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.Categories.Add(new Category
                    {
                        Id = record.Id,
                        Name = record.Name,
                        Description = record.Description ?? string.Empty,
                    });
                }

                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            });
        }

        private class CategoryCsvRecord
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
        }
    }
}
