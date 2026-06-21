using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Auth;

namespace Restaurant.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(request.Request, cancellationToken);
    }
}
