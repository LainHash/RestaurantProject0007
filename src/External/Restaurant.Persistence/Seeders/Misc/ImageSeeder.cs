using Microsoft.EntityFrameworkCore;
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

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "images.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<ImageCsvRecord>().ToList();

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

        private class ImageCsvRecord
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
