using Microsoft.Extensions.DependencyInjection;
using Restaurant.Persistence.Contexts;
using Restaurant.Persistence.Seeders.Catalog;
using Restaurant.Persistence.Seeders.Customers;
using Restaurant.Persistence.Seeders.Identity;
using Restaurant.Persistence.Seeders.Territory;
using Restaurant.Persistence.Seeders.Inventory;
using Restaurant.Persistence.Seeders.Misc;

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
            await SeedAsync<RoleSeeder>(_context);
            await SeedAsync<PersonalInformationSeeder>(_context);
            await SeedAsync<UserSeeder>(_context);
            await SeedAsync<CustomerSeeder>(_context);

            // Catalog seeding: Categories first (FK dependency)
            await SeedAsync<CategorySeeder>(_context);
            await SeedAsync<ProductSeeder>(_context);
            await SeedAsync<ProductStockSeeder>(_context);
            await SeedAsync<ImageSeeder>(_context);
            await SeedAsync<ProductImageSeeder>(_context);

            // TableManagement seeding: Area first, then RestaurantTable
            await SeedAsync<AreaSeeder>(_context);
            await SeedAsync<RestaurantTableSeeder>(_context);
        }

        private async Task SeedAsync<TSeeder>(RestaurantDbContext context) where TSeeder : IDataSeeder
        {
            using var scope = _serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<TSeeder>();
            await seeder.SeedAsync(context);
        }
    }
}
