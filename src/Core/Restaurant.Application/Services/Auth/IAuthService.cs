using Restaurant.Application.Common.Models.Result;
using Restaurant.Contracts.DTOs.Authentication;

namespace Restaurant.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
        Task<DataResult<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
        Task<Result> VerifyEmailAsync(VerifyEmailRequest request, CancellationToken cancellationToken = default);
    }
}
