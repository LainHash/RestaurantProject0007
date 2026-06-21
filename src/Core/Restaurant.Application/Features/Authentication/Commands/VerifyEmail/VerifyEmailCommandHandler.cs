using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Auth;

namespace Restaurant.Application.Features.Authentication.Commands.VerifyEmail;

public class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand, Result>
{
    private readonly IAuthService _authService;

    public VerifyEmailCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        return await _authService.VerifyEmailAsync(request.Request, cancellationToken);
    }
}
