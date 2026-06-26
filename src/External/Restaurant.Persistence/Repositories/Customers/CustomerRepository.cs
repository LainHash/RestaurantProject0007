using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Customers;
using Restaurant.Domain.Repositories.Customers;
using Restaurant.Persistence.Contexts;
using Restaurant.Persistence.Repositories;

namespace Restaurant.Persistence.Repositories.Customers;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(RestaurantDbContext context) : base(context)
    {
    }

    public async Task<Customer?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await Entity.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
}
