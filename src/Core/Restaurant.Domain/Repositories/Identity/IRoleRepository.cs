using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Repositories.Identity;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
