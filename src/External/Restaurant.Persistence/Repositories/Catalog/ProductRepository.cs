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

        public new async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductStock)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
