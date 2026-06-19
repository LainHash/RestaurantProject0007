using Microsoft.EntityFrameworkCore.Storage;

namespace Restaurant.Application.Services
{
    public interface IUnitOfWork
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
