using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Domain.Repositories.Misc;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Repositories.Misc
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<int> CountByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ProductImage>()
                .CountAsync(pi => pi.ProductId == productId, cancellationToken);
        }

        public async Task UnsetPrimaryAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            // Lấy tất cả Image đang là primary của product này
            var primaryImages = await _context.Set<ProductImage>()
                .Where(pi => pi.ProductId == productId)
                .Include(pi => pi.Image)
                .Where(pi => pi.Image.IsPrimary)
                .Select(pi => pi.Image)
                .ToListAsync(cancellationToken);

            foreach (var image in primaryImages)
            {
                image.IsPrimary = false;
            }
        }

        public async Task AddProductImageAsync(ProductImage productImage, CancellationToken cancellationToken = default)
        {
            await _context.Set<ProductImage>().AddAsync(productImage, cancellationToken);
        }
    }
}
