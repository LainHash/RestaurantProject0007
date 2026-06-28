using Restaurant.Domain.Entities.Guests;

namespace Restaurant.Domain.Repositories.Customers;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
