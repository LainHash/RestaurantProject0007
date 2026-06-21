using Microsoft.Extensions.DependencyInjection;
using Restaurant.Persistence.Contexts;
using Restaurant.Persistence.Seeders.Catalog;
using Restaurant.Persistence.Seeders.Customers;
using Restaurant.Persistence.Seeders.Identity;

namespace Restaurant.Persistence.Seeders
{
    internal class DatabaseSeeder
    {
        private readonly RestaurantDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public DatabaseSeeder(RestaurantDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task SeedAllAsync()
        {
            // Identity seeding: Role first, then PersonalInformation, then User, then Customer
            await SeedAsync<Restaurant.Persistence.Seeders.Identity.RoleSeeder>(_context);
            await SeedAsync<Restaurant.Persistence.Seeders.Identity.PersonalInformationSeeder>(_context);
            await SeedAsync<Restaurant.Persistence.Seeders.Identity.UserSeeder>(_context);
            await SeedAsync<Restaurant.Persistence.Seeders.Customers.CustomerSeeder>(_context);

            // Catalog seeding: Categories first (FK dependency)
            await SeedAsync<CategorySeeder>(_context);
            await SeedAsync<Restaurant.Persistence.Seeders.Catalog.ProductSeeder>(_context);
            await SeedAsync<Restaurant.Persistence.Seeders.Inventory.ProductStockSeeder>(_context);
            await SeedAsync<Restaurant.Persistence.Seeders.Misc.ImageSeeder>(_context);
            await SeedAsync<Restaurant.Persistence.Seeders.Misc.ProductImageSeeder>(_context);
        }

        private async Task SeedAsync<TSeeder>(RestaurantDbContext context) where TSeeder : IDataSeeder
        {
            using var scope = _serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<TSeeder>();
            await seeder.SeedAsync(context);
        }
    }
}
