using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Application.Services;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantDbContext _context;

        public UnitOfWork(RestaurantDbContext context)
        {
            _context = context;
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default)
        {
            return _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
