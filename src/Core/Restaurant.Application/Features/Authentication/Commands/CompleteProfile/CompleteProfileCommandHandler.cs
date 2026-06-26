using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Auth;

namespace Restaurant.Application.Features.Authentication.Commands.CompleteProfile;

public class CompleteProfileCommandHandler : ICommandHandler<CompleteProfileCommand, Result>
{
    private readonly IAuthService _authService;

    public CompleteProfileCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result> Handle(CompleteProfileCommand request, CancellationToken cancellationToken)
    {
        return await _authService.CompleteProfileAsync(request.Request, cancellationToken);
    }
}
