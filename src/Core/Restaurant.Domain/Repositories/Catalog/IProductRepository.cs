using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ProductStock> AddAsync(ProductStock entity, CancellationToken cancellationToken = default);
    }
}
