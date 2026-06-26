using Microsoft.EntityFrameworkCore;
using MiniExcelLibs;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Seeders.Identity
{
    internal class UserSeeder : IDataSeeder
    {
        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var xlsxPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "RestaurantData.xlsx");

            if (!File.Exists(xlsxPath))
                throw new FileNotFoundException($"Seed data file not found: {xlsxPath}");

            var records = MiniExcel.Query<UserExcelRecord>(xlsxPath, sheetName: "Users").ToList();

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
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(record.PasswordHash, 12),
                        IsActive = record.IsActive,
                        RoleId = record.RoleId,
                    });
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });
        }

        private class UserExcelRecord
        {
            public Guid Id { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public Guid RoleId { get; set; }
        }
    }
}
