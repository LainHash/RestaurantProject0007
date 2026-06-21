using Restaurant.Domain.Entities.Misc;

namespace Restaurant.Domain.Repositories.Misc
{
    public interface IImageRepository : IRepository<Image>
    {
        /// <summary>Đếm số ảnh hiện tại của một product.</summary>
        Task<int> CountByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        /// <summary>Unset IsPrimary = false cho tất cả ảnh của product (dùng trước khi set primary mới).</summary>
        Task UnsetPrimaryAsync(Guid productId, CancellationToken cancellationToken = default);

        /// <summary>Thêm mới quan hệ ProductImage.</summary>
        Task AddProductImageAsync(ProductImage productImage, CancellationToken cancellationToken = default);
    }
}
