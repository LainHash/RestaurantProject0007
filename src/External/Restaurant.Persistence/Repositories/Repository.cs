using Restaurant.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        private readonly RestaurantDbContext _context;
        private DbSet<TEntity> Entity;
        
        public Repository(RestaurantDbContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllAsync()
        {
            return Entity.AsNoTracking().AsQueryable();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await Entity.FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Entity.AddAsync(entity);
            return entity;
        }

        public void Update(TEntity entity)
        {
            Entity.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            Entity.Remove(entity);
        }
    }
}
