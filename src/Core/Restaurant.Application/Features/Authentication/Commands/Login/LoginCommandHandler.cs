using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Auth;
using Restaurant.Contracts.DTOs.Authentication;

namespace Restaurant.Application.Features.Authentication.Commands.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, DataResult<AuthResponse>>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<DataResult<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LoginAsync(request.Request, cancellationToken);
    }
}
