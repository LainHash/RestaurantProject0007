using Restaurant.Application.Common.Models.Result;
using Restaurant.Contracts.DTOs.Auth;

namespace Restaurant.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<Result> LoginAsync();
        Task<DataResult<RegisterResponse>> RegisterAsync(RegisterRequest request);
        string GenerateJwtToken();
        void Logout();
        void RefeshToken();
    }
}
