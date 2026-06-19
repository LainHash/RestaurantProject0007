using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default, Guid? excludeId = null);
    }
}
