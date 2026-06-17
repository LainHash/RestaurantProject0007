using Restaurant.Domain.Repositories;
using Restaurant.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Persistence.Repositories
{
    public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        private readonly RestaurantDbContext _context;
        private DbSet<TEntity> Entity;
        
        public Repository(RestaurantDbContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();
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
