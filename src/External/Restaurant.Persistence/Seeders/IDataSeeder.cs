using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Seeders;

public interface IDataSeeder
{
    Task SeedAsync(RestaurantDbContext context);
}
