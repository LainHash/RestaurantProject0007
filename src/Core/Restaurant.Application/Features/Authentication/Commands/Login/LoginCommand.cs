using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Authentication;

namespace Restaurant.Application.Features.Authentication.Commands.Login;

public class LoginCommand : ICommand<DataResult<AuthResponse>>
{
    public LoginRequest Request { get; set; }

    public LoginCommand(LoginRequest request)
    {
        Request = request;
    }
}
