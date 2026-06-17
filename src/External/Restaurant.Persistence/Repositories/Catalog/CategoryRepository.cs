using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Repositories.Catalog
{
    public class CategoryRepository : Repository<Category, RestaurantDbContext>, ICategoryRepository
    {
        public CategoryRepository(RestaurantDbContext context) : base(context)
        {
        }
    }
}
