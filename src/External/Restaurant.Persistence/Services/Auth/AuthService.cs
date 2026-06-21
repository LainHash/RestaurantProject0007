using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Services.Auth;
using Restaurant.Contracts.DTOs.Auth;

namespace Restaurant.Persistence.Services.Auth
{
    public class AuthService : IAuthService
    {
        public string GenerateJwtToken()
        {
            throw new NotImplementedException();
        }

        public Task<Result> LoginAsync()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void RefeshToken()
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<RegisterResponse>> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
