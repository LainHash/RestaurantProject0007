using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Repositories.Territory
{
    public class RestaurantTableRepository : Repository<RestaurantTable>, IRestaurantTableRepository
    {
        public RestaurantTableRepository(RestaurantDbContext context) : base(context)
        {
        }
    }
}
