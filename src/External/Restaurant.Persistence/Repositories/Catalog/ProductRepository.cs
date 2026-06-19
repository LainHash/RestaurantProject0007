using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
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

        public async Task<ProductStock> AddAsync(ProductStock entity, CancellationToken cancellationToken = default)
        {
            await _context.ProductStocks.AddAsync(entity);
            return entity;
        }
    }
}
