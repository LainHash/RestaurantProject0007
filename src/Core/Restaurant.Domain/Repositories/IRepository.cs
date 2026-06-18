namespace Restaurant.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
