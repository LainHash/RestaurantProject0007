using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Features.Catalog.Products.Queries.GetOne;
using Restaurant.Domain.Repositories;
using Restaurant.Domain.Specifications;
using Restaurant.Persistence.Contexts;
using Restaurant.Persistence.Specifications;

namespace Restaurant.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly RestaurantDbContext _context;
        protected readonly DbSet<TEntity> Entity;

        public Repository(RestaurantDbContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Entity.AsNoTracking().AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellation = default)
        {
            var query = SpecificationEvaluator
                .GetQuery(Entity.AsQueryable(), specification);

            return await query.ToListAsync(cancellation);
        }

        public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellation = default)
        {
            // Đếm tổng không áp dụng paging — chỉ áp dụng Criteria
            var query = SpecificationEvaluator
                .GetQuery(Entity.AsQueryable(), specification, applyPaging: false);

            return await query.CountAsync(cancellation);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Entity.FindAsync(id);
        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator
                .GetQuery(Entity.AsQueryable(), specification);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
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

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
