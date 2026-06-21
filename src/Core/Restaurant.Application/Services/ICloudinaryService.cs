using Restaurant.Application.Common.Models;

namespace Restaurant.Application.Services
{
    public interface ICloudinaryService
    {
        /// <summary>
        /// Upload file stream lên Cloudinary.
        /// </summary>
        /// <param name="fileStream">Stream nội dung file ảnh</param>
        /// <param name="fileName">Tên file gốc (dùng để xác định format)</param>
        /// <param name="folder">Folder trên Cloudinary — null thì dùng giá trị mặc định từ config</param>
        Task<CloudinaryUploadResult> UploadAsync(
            Stream fileStream,
            string fileName,
            string? folder = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Xoá ảnh trên Cloudinary theo publicId.
        /// </summary>
        Task<bool> DeleteAsync(string publicId, CancellationToken cancellationToken = default);
    }
}
