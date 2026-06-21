using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Repositories.Identity;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(RestaurantDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
    }
}
