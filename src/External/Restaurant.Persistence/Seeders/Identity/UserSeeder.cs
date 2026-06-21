using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Persistence.Contexts;
using System.Globalization;

namespace Restaurant.Persistence.Seeders.Identity
{
    internal class UserSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "users.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<UserCsvRecord>().ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.Users.Add(new User
                    {
                        Id = record.Id,
                        UserName = record.UserName,
                        Email = record.Email,
                        PasswordHash = record.PasswordHash,
                        IsActive = record.IsActive,
                        PIId = record.PIId,
                        RoleId = record.RoleId,
                    });
                }

                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            });
        }

        private class UserCsvRecord
        {
            public Guid Id { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public Guid PIId { get; set; }
            public Guid RoleId { get; set; }
        }
    }
}
