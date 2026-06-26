using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Seeders.Identity
{
    internal class PersonalInformationSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.PersonalInformations.AnyAsync())
                return;

            var xlsxPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "RestaurantData.xlsx");

            if (!File.Exists(xlsxPath))
                throw new FileNotFoundException($"Seed data file not found: {xlsxPath}");

            var records = MiniExcel.Query<PersonalInformationExcelRecord>(xlsxPath, sheetName: "PersonalInformations").ToList();

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
                        DOB = DateOnly.FromDateTime(record.DOB),
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

        private class PersonalInformationExcelRecord
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public DateTime DOB { get; set; }
            public bool Gender { get; set; }
            public string Address { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string Country { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string CitizenCardId { get; set; } = string.Empty;
        }
    }
}
