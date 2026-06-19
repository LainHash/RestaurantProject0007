using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Repositories.Catalog
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly RestaurantDbContext _context;
        public ProductRepository(RestaurantDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
