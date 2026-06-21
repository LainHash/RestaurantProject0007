using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Repositories.Identity;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(RestaurantDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Role>().FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
    }
}
