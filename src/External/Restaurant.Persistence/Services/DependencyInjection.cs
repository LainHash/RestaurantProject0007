using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Persistence.Contexts;
using Restaurant.Persistence.Seeders;
using Restaurant.Persistence.Seeders.Catalog;

namespace Restaurant.Persistence.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // ── Database ─────────────────────────────────────────────────────
            services.AddDbContext<RestaurantDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("MyConnectString"),
                    sqlOptions => sqlOptions.MigrationsAssembly(
                        typeof(RestaurantDbContext).Assembly.FullName)));

            // ── Seeders ──────────────────────────────────────────────────────

            // Catalog seeders
            services.AddScoped<CategorySeeder>();
            services.AddScoped<DatabaseSeeder>();

            return services;
        }

        public static async Task InitialiseDatabaseAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var sp = scope.ServiceProvider;

            var context = sp.GetRequiredService<RestaurantDbContext>();
            await context.Database.MigrateAsync();

            var seeder = sp.GetRequiredService<DatabaseSeeder>();
            await seeder.SeedAllAsync();
        }
    }
}
