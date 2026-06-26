using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Repositories.Territory
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(RestaurantDbContext context) : base(context)
        {
        }
    }
}
