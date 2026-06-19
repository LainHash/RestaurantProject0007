using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IProductRepository : IRepository<Product>
    {
        new Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
