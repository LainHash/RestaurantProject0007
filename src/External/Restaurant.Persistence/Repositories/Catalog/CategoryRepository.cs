using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Repositories.Catalog
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly RestaurantDbContext _context;
        public CategoryRepository(RestaurantDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default, Guid? excludeId = null)
        {
            var query = _context.Set<Category>().AsQueryable();
            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }
            return !await query.AnyAsync(c => c.Name.ToLower() == name.ToLower(), cancellationToken);
        }
    }
}
