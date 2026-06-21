using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Persistence.Contexts;
using System.Globalization;

namespace Restaurant.Persistence.Seeders.Identity
{
    internal class PersonalInformationSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.PersonalInformations.AnyAsync())
                return;

            var csvPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "personal_information.csv");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"Seed data file not found: {csvPath}");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            });

            var records = csv.GetRecords<PersonalInformationCsvRecord>().ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();

                foreach (var record in records)
                {
                    context.PersonalInformations.Add(new PersonalInformation
                    {
                        Id = record.Id,
                        FirstName = record.FirstName,
                        LastName = record.LastName,
                        DOB = record.DOB,
                        Gender = record.Gender,
                        Address = record.Address,
                        City = record.City,
                        Country = record.Country,
                        Phone = record.Phone,
                        CitizenCardId = record.CitizenCardId,
                    });
                }

                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            });
        }

        private class PersonalInformationCsvRecord
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public DateOnly DOB { get; set; }
            public bool Gender { get; set; }
            public string Address { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string Country { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string CitizenCardId { get; set; } = string.Empty;
        }
    }
}
