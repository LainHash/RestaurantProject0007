using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Services;
using Restaurant.Contracts.Settings;

namespace Restaurant.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary            _cloudinary;
        private readonly CloudinarySettings    _settings;

        public CloudinaryService(IOptions<CloudinarySettings> options)
        {
            _settings = options.Value;

            var account   = new Account(_settings.CloudName, _settings.ApiKey, _settings.ApiSecret);
            _cloudinary   = new Cloudinary(account) { Api = { Secure = true } };
        }

        public async Task<CloudinaryUploadResult> UploadAsync(
            Stream fileStream,
            string fileName,
            string? folder = null,
            CancellationToken cancellationToken = default)
        {
            var uploadParams = new ImageUploadParams
            {
                File           = new FileDescription(fileName, fileStream),
                Folder         = folder ?? _settings.Folder,
                UseFilename    = false,
                UniqueFilename = true,
                Overwrite      = false,
            };

            var result = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

            if (result.Error is not null)
            {
                return new CloudinaryUploadResult
                {
                    IsSuccess    = false,
                    ErrorMessage = result.Error.Message
                };
            }

            return new CloudinaryUploadResult
            {
                IsSuccess    = true,
                Url          = result.SecureUrl?.ToString() ?? string.Empty,
                PublicId     = result.PublicId,
                StoragePath  = result.PublicId,
                FileSize     = result.Bytes,
                ContentType  = $"image/{result.Format}"
            };
        }

        public async Task<bool> DeleteAsync(string publicId, CancellationToken cancellationToken = default)
        {
            var deleteParams = new DeletionParams(publicId);
            var result       = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok";
        }
    }
}
