namespace Restaurant.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAllAsync(CancellationToken cancellation = default);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellation = default);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
